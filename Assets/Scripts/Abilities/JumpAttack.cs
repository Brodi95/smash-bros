﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : Ability {

	void Start() {
		_AbilityName = Abilities.JumpAttack;
	}

	protected override void Update ()
	{
		if(!_Character._Grounded)
		base.Update ();
	}

	protected override void OnAbilityUsed ()
	{
		_Character.Animator.SetBool ("Jump", false);
		_Character.Animator.SetTrigger (_AbilityData.Event);
		Debug.Log ("Jump attack");
	}

	protected override void StopAbility ()
	{
		
	}
}
