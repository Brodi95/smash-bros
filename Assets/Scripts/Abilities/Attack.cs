using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Ability {

	void Start() {
		_AbilityName = Abilities.Attack;
	}

	protected override void Update ()
	{
		if(_Character._Grounded)
		base.Update ();
		if (Input.GetButtonUp (_AbilityButton)) {
			_Character.Animator.SetBool (_AbilityData.Event, false);
		}
	}

	protected override void StopAbility ()
	{
		FrontAttack (1.5f);
		_Character.Animator.SetBool (_AbilityData.Event, false);
	}

}
