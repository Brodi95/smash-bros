using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Ability {

	void Start() {
		_AbilityName = Abilities.Block;
	}

	protected override void Update ()
	{
		if (Input.GetButtonDown (_AbilityButton) ) {
			UseAbility ();
		}
		if (Input.GetButtonUp (_AbilityButton)) {
			StopAbility ();
		}
	}

	protected override void StopAbility ()
	{
		_Character.Animator.SetBool (_AbilityData.Event, false);
	}

}
