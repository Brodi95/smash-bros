using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : Ability {

	protected override void OnAbilityUsed ()
	{
		_Character.Animator.SetTrigger (_AbilityData.Event);
		_Character.Animator.SetBool ("Jump", false);
	}
}
