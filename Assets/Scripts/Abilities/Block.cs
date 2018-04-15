using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Ability {

	protected override void Update ()
	{
		if (Input.GetButton (_AbilityButton)) {
			UseAbility ();
		}
		if (Input.GetButtonUp (_AbilityButton)) {
			StopAbility ();
		}
	}

}
