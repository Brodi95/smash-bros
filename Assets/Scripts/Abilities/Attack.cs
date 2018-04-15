using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Ability {

	protected override void OnEnable() {
		EventManager.StartListening (_AbilityData.Event, UseAbility);
		EventManager.StartListening (_AbilityData.Event + "Stop", StopAbility);
	}

	protected override void OnDisable() {
		EventManager.StopListening (_AbilityData.Event, UseAbility);
		EventManager.StopListening (_AbilityData.Event + "Stop", StopAbility);
	}

	protected override void OnAbilityUsed() {
		_Character.Animator.SetTrigger (_AbilityData.Event);
	}


}
