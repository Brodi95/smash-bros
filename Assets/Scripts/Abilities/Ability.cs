using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

	[SerializeField] protected BaseAbility _AbilityData;	// The scriptable object containing relevant data

	protected Abilities _AbilityName;
	protected float _Damage;					// The amount of damage the players base attack does
	protected float _Cooldown;					// The time before the attack can be used again
	protected float _CurrentCooldown;
	protected bool _OnCooldown {				// If the attack can be used
		get { return _CurrentCooldown > 0f;}}	

	protected Character _Character;		// Reference to Character script
	protected string _AbilityButton {	// Button name in input
		get { return _AbilityData.Event + "_" + _Character.PlayerNum; }
	}

	protected void Awake() {
		_Character = GetComponent<Character> ();
	}
		
	public void Initialize() {
	}

	protected virtual void Update() {
		// If the the ability button is hold down
		// and the player isn't already attacking
		// and the ability is off cooldown
		if (Input.GetButton (_AbilityButton) /* && !_Character.Attacking */
			&& !_OnCooldown) {
			UseAbility ();
		}
	}

	protected void UseAbility() {
//		_Character.CurrentAbility = _AbilityName;
		// Use ability
		OnAbilityUsed();
		// Start cooldown timer
		_CurrentCooldown = _AbilityData.Cooldown;
		StartCoroutine (RunCooldown ());


	}

	protected virtual void OnAbilityUsed() {
		// Start ability animation
		_Character.Animator.SetTrigger (_AbilityData.Event);
		Debug.Log ("Ability used");
	}

	protected void OnAnimationFinished() {

		if (_Character.CurrentAbility != _AbilityName)
			return;
//		_Character.CurrentAbility = Abilities.None;
		
		Debug.Log (_AbilityName + " finnished");
		StopAbility ();
	}

	protected abstract void StopAbility ();

	protected IEnumerator RunCooldown() {
		while (_OnCooldown) {
			_CurrentCooldown -= Time.deltaTime;
			yield return null;
		}

	}

	protected void FrontAttack (float attackRadius) {
		Debug.Log ("FrontAttack");
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position , attackRadius);
		foreach (var collider in colliders) {
			if (collider.tag == "Player" && collider.gameObject != gameObject) {
				// Only attack objects infront of the player
				if(!Math.IsLookingAtObject(transform, collider.transform, 30f, _Character.FacingRight)) {
					continue; 
				}

				Character character = collider.gameObject.GetComponent<Character> ();
				// Only apply damage if opponent isn't blocking
				if (character.CurrentAbility == Abilities.Block) {
					character.Block ();
					continue;
				}

				character.UpdateHealthpoints (- _AbilityData.Damage);
			}
		}
	}
}

public enum Abilities {
	None,
	Attack,
	Block,
	JumpAttack,
	Dash
}
