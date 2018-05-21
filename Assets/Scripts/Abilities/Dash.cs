using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Ability {

	void Start() {
		_AbilityName = Abilities.Dash;
	}

	protected override void OnAbilityUsed ()
	{
		_Character.Animator.SetTrigger ("Dash");
	}

	protected override void StopAbility ()
	{
		
	}
}
