using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	#region Variables
	public string PlayerNum = "P1";			// The number of the controller
	public BaseCharacter _CharacterData;	// Selected Character: Data from Scriptable Object

	[HideInInspector] public bool Attacking {
		get { return !(CurrentAbility == Abilities.None); }
	}									// Player is currently attacking
	 public Abilities CurrentAbility;	// The ability currently animating

	[HideInInspector] public float MaxHealthpoints = 100f;
	[HideInInspector] public float CurrentHealthpoints; 

	#region Delegates
	public delegate void UpdatingUI();
	public UpdatingUI UpdateUI;
	public delegate void Flipping ();
	public Flipping Flip; 
	#endregion

	#region Movement
	[SerializeField] private float _MaxSpeed = 10f;					// Fastest Speed the player can move horizontally
	[SerializeField][Range(1, 10)] private float _JumpVelocity;		// Force which determines how high the player can jump
	private const float _GravityScale = 1f;							// The degree to which the player is affected by gravity
	private const float _FallMultiplier = 4.5f;						// Used for falling
	private const float _LowJumpMultiplier = 2f;					// Used for lower jumping
	[SerializeField] private bool _AirControl;						// Allows movement while not grounded
	public bool FacingRight = true;								// Determines which way the player is facing
	#endregion

	#region Check for objects
	[SerializeField] private LayerMask _GroundMask;		// Determines masks used for ground (check)
	[HideInInspector] public bool _Grounded;			// Does the player collide with a ground object
	[SerializeField] private Transform _GroundCheck;	// Child transform marking the position where to check if the player is grounded
	private const float _GroundCheckRadius = 0.2f;		// Radius of the overlap circle for ground check
	[SerializeField] private Transform _CeilingCheck;	// Child transform marking the position where to check if the player can stand up
	private const float _CeilingCheckRadius = 0.01f;	// Radius of the overlap circle for ceiling check
	#endregion

	private Rigidbody2D _Rigidbody;
	[HideInInspector] public Animator Animator;

	private string _JumpButton { get { return "Jump_" + PlayerNum; } }
	#endregion

	private void Awake() {
		// Setting up References (Cache)
		_Rigidbody = GetComponent<Rigidbody2D> ();
		Animator = GetComponent<Animator> ();
		Flip += FlipCharacter;

	}

	private void Start() {
		CurrentHealthpoints = MaxHealthpoints;
	}

	private void Update() {
	// Test purpose
		if (Input.GetKeyDown (KeyCode.F))
			UpdateHealthpoints (-20f);
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
			if (move < 0 && FacingRight) {
				Flip ();
			} 
			// ... or moving right but facing left
			else if (move > 0 && !FacingRight) {
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
	private void FlipCharacter() {
		// Inverting the bool labelling the players facing
		FacingRight = !FacingRight;

		// Inverting the players horizontal scale
		Vector3 localScale = transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}

	public void UpdateHealthpoints(float value) {		
		CurrentHealthpoints += value;
		// Make sure the current healthpoints can't be higher than the max value
		if (CurrentHealthpoints > MaxHealthpoints)
			CurrentHealthpoints = MaxHealthpoints;

		// Update Healthbar
		UpdateUI();

	}

	public void Block() {
		Animator.SetBool ("Blocked", true);
	}
}
