using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerController : MonoBehaviour {

	private Character _Character;
	private bool _Jump;
	private string _JumpButton { get { return "Jump_" + _Character.PlayerNum; } }
	private string _HorizontalAxis { get { return "Horizontal_" + _Character.PlayerNum; } }

	private void Awake() {
		// Setting up references
		_Character = GetComponent<Character> ();
	}

	// Read Input in Update so no keypresses are missed
	private void Update() {
		if (!_Jump) {
			_Jump = Input.GetButtonDown (_JumpButton);
//			if (!GameManager.Instance.Attacking) {
//				if(Input.GetButton ("Fire1")) {
//					EventManager.TriggerEvent ("Fire1");
//				} else if (Input.GetButton ("Fire2")) {
//					EventManager.TriggerEvent ("Fire2");
//
//				} else if (Input.GetButtonUp ("Fire1")) {
//					EventManager.TriggerEvent ("Fire1Stop");
//				} else if (Input.GetButtonUp ("Fire2")) {
//					EventManager.TriggerEvent ("Fire2Stop");
//				}
//			}
		}
//		if (Input.GetButton ("Jump") && Input.GetButtonDown ("Fire1")) {
//			EventManager.TriggerEvent ("JumpAttack");
//		}
	}

	// Use FixedUpdate for Physics
	private void FixedUpdate() {
		// Get horizontal movement
		float move = Input.GetAxis(_HorizontalAxis);
		_Character.Move (move, _Jump);
		_Jump = false;
	}		
}
