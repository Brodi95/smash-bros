using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Ability {


	protected override void Update ()
	{
		if(_Character._Grounded)
		base.Update ();
		if (Input.GetButtonUp (_AbilityButton)) {
			_Character.Animator.SetBool (_AbilityData.Event, false);
		}
	}

}
