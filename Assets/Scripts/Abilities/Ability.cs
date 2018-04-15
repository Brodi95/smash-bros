using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

	[SerializeField] protected BaseAbility _AbilityData;	// The scriptable object containing relevant data

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
		if (Input.GetButton (_AbilityButton) && !_Character.Attacking && !_OnCooldown) {
			UseAbility ();
		}
	}

	protected void UseAbility() {
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

	protected virtual void OnAnimationFinished() {
		Debug.Log ("Animation finished");
		StopAbility ();
	}

	protected void StopAbility() {
		_Character.Animator.SetBool (_AbilityData.Event, false);
	}

	protected IEnumerator RunCooldown() {
		while (_OnCooldown) {
			_CurrentCooldown -= Time.deltaTime;
			yield return null;
		}

	}
}
