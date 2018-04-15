using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Ability {


	protected override void OnEnable() {
		EventManager.StartListening (_AbilityData.Event, UseAbility);
		EventManager.StartListening (_AbilityData.Event + "Stop", StopAbility);
	}

	protected override void OnDisable() {
		EventManager.StopListening (_AbilityData.Event, UseAbility);
		EventManager.StopListening (_AbilityData.Event + "Stop", StopAbility);
	}

	protected override void OnAbilityUsed() {
		_Character.Animator.SetFloat ("AnimationSpeed", 0.5f);
		_Character.Animator.SetBool (_AbilityData.Event, true);
	}


}
