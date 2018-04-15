using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

	[SerializeField] protected BaseAbility _AbilityData;	// The scriptable object containing relevant data

	protected float _AttackDamage;					// The amount of damage the players base attack does
	protected float _AttackCooldown;					// The time before the attack can be used again
	protected float _AttackCurrentCooldown;
	protected bool _AttackOnCooldown {				// If the attack can be used
		get { return _AttackCurrentCooldown > 0f;}}	

	protected Character _Character;		// Reference to Character script

	protected void Awake() {
		_Character = GetComponent<Character> ();
	}

	protected virtual void OnEnable() {
		EventManager.StartListening (_AbilityData.Event, UseAbility);
	}

	protected virtual void OnDisable() {
		EventManager.StopListening (_AbilityData.Event, UseAbility);
	}

	public void Initialize() {
	}

	protected void UseAbility() {
		// Only execute if not on cooldown
		if (_AttackOnCooldown)
			return;
		
//		_AbilityData.OnAbilityUsed ();
		OnAbilityUsed();

		// Start cooldown timer
		_AttackCurrentCooldown = _AbilityData.Cooldown;
		StartCoroutine (RunCooldown ());

	}

	protected abstract void OnAbilityUsed();

	protected void StopAbility() {
		_Character.Animator.SetBool (_AbilityData.Event, false);
	}

	protected IEnumerator RunCooldown() {
		while (_AttackOnCooldown) {
			_AttackCurrentCooldown -= Time.deltaTime;
			yield return null;
		}

	}
}
