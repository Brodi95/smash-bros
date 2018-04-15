using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public string PlayerNum = "P1";			// The number of the controller
	public BaseCharacter _CharacterData;	// Selected Character: Data from Scriptable Object

	[SerializeField] private float _MaxSpeed = 10f;					// Fastest Speed the player can move horizontally
	[SerializeField][Range(1, 10)] private float _JumpVelocity;		// Force which determines how high the player can jump
	private const float _GravityScale = 1f;							// The degree to which the player is affected by gravity
	private const float _FallMultiplier = 4.5f;						// Used for falling
	private const float _LowJumpMultiplier = 2f;					// Used for lower jumping
	[SerializeField] private bool _AirControl;						// Allows movement while not grounded
	[SerializeField] private LayerMask _GroundMask;					// Determines masks used for ground (check)

	private bool _Grounded;								// Does the player collide with a ground object
	[SerializeField] private Transform _GroundCheck;	// Child transform marking the position where to check if the player is grounded
	private const float _GroundCheckRadius = 0.2f;		// Radius of the overlap circle for ground check
	[SerializeField] private Transform _CeilingCheck;	// Child transform marking the position where to check if the player can stand up
	private const float _CeilingCheckRadius = 0.01f;	// Radius of the overlap circle for ceiling check
	private bool _FacingRight = true;					// Determines which way the player is facing

	private Rigidbody2D _Rigidbody;
	[HideInInspector] public Animator Animator;

	private string _JumpButton { get { return "Jump_" + PlayerNum; } }


	private void Awake() {
		// Setting up References (Cache)
//		_GroundCheck = transform.GetChild (0);
//		_CeilingCheck = transform.GetChild (1);
		_Rigidbody = GetComponent<Rigidbody2D> ();
		Animator = GetComponent<Animator> ();

//		// Initialize abilities
//		var abilities = GetComponents<Ability> ();
//		foreach (var ability in abilities) {
//			ability.Initialize ();
//		}
	}

	private void FixedUpdate() {
		_Grounded = false;
		// CircleCast to check if the player is grounded
		Collider2D[] colliders = Physics2D.OverlapCircleAll(_GroundCheck.position, _GroundCheckRadius, _GroundMask);
		foreach (var collider in colliders) {
			// Make sure to exclude the player character 
			if (collider.gameObject != gameObject) {
				_Grounded = true;
			}
		}
		Animator.SetBool ("Ground", _Grounded);

		// Adding vertical velocity if player is jumping
		if (_Rigidbody.velocity.y < 0) {
			_Rigidbody.gravityScale = _FallMultiplier;
		} else if (_Rigidbody.velocity.y > 0 && !Input.GetButton (_JumpButton)) {
			_Rigidbody.gravityScale = _LowJumpMultiplier;
		} else {
			_Rigidbody.gravityScale = _GravityScale;
		}
	}

	public void Move(float move, bool jump) {
		// Only control player if grounded or air control enabled
		if (_Grounded || _AirControl) {
			_Rigidbody.velocity = new Vector2 (move * _MaxSpeed, _Rigidbody.velocity.y);

			Animator.SetFloat ("Speed", Mathf.Abs (move));

			// Flip the character if the player is moving left but facing right...
			if (move < 0 && _FacingRight) {
				Flip ();
			} 
			// ... or moving right but facing left
			else if (move > 0 && !_FacingRight) {
				Flip ();
			}
		}

		// If player should jump
		if (_Grounded && jump) {
			_Grounded = false;
			Animator.SetBool ("Ground", false);
			// Moving the player vertically by adding an impulse force to the y axis
			_Rigidbody.AddForce (Vector2.up * _JumpVelocity, ForceMode2D.Impulse);
		}
	}

	// Flipping the player horizontally
	private void Flip() {
		// Inverting the bool labelling the players facing
		_FacingRight = !_FacingRight;

		// Inverting the players horizontal scale
		Vector3 localScale = transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}
}
