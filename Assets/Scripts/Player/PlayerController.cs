using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour {
	// Player
	[Header("RigidBody2D")] [SerializeField]
	private Transform player;

	// Movement
	[Header("Player Walk")] [SerializeField]
	private float moveSpeedWalk = 1f;

	private const float WalkAcceleration = 1f;
	private const float ReleaseDecelerationX = 0.3f;
	private const float SkidTurnAroundSpeedX = 3f;
	private const float SkidDecelerationX = 0.5f;
	private const float AirAccelerationX = 0.2f;
	private const float AirDecelerationX = 0.15f;
	private const float JumpUpGravity = 2.7888f;
	private const float JumpDownGravity = 5f;

	[Header("Max Speed Walk")] [SerializeField]
	private float maxSpeedWalk = 2f;

	[Header("Player Run")] [SerializeField]
	private float moveSpeedRun = 1f;

	[Header("Max Speed Run")] [SerializeField]
	private float maxSpeedRun = 4f;

	public float currentSpeedX;
	private float _moveDirection;
	private bool _isChangingDirection;

	// Physics
	private const float NormalGravity = 1;

	// Flags
	public bool Running => Mathf.Abs(velocity.x) > maxSpeedWalk || Mathf.Abs(input.Move.x) > maxSpeedWalk;
	public bool turn;
	private bool IsGrounded { get; set; }
	public bool jumping;
	public bool falling;
	private bool _hasRb;
	public bool isHeadUp;
	public bool isDuck;


	private const float JumpVelocity = 15f;
	
	private float _turnTimer;
	public float directionX;
	public float directionHead;

	public PlayerInput input;
	public Vector2 velocity;
	public Rigidbody2D rb;

	//Ground Check
	public Transform feetPos;
	public float checkRadius;
	public LayerMask whatIsGround;
	private float _jumpTimeCounter;

	void Start() {
		input = GetComponent<PlayerInput>();
		_hasRb = player.TryGetComponent(out rb);
		rb.gravityScale = NormalGravity;
	}

	// Update is called once per frame
	void Update() {
		directionX = input.Move.x;
		directionHead = input.Move.y;
		_isChangingDirection = currentSpeedX > 0 && _moveDirection * directionX < 0;
		IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f);
	}


	private void OnEnable() {
		velocity = Vector2.zero;
	}

	void FixedUpdate() {
		GroundedMovement();
		AirMovement();
		VerticalMovement();
		rb.velocity = new Vector2(_moveDirection * currentSpeedX, rb.velocity.y);
	}

	private void VerticalMovement() {
		if (IsGrounded) {
			jumping = false;
			falling = false;
		}
		else if (rb.velocity.y < 0) {
			falling = true;
			jumping = false;
		}

		if (!jumping) {
			if (IsGrounded && input.JumpHold) {
				rb.velocity = new Vector2(rb.velocity.x, JumpVelocity);
				jumping = true;
			}
		}
		if (rb.velocity.y > 0 && input.JumpHold) {
			rb.gravityScale = NormalGravity * JumpUpGravity;
		}
		else if(!IsGrounded) {
			rb.gravityScale = NormalGravity * JumpDownGravity;
		}
	}

	private void AirMovement() {
		if (!IsGrounded) {
			//TODO: Set parameters air movement
			if (directionX != 0) {
				if (currentSpeedX == 0) {
					currentSpeedX = moveSpeedWalk;
				}
				else if (currentSpeedX < maxSpeedWalk) {
					currentSpeedX = IncreaseWithinBound(currentSpeedX, AirAccelerationX, maxSpeedWalk);
				}
				//TODO: Implement speed x if player is running
			}
			else if (currentSpeedX > 0) {
				currentSpeedX = DecreaseWithinBound(currentSpeedX, ReleaseDecelerationX, 0);
			}
			turn = false;
			if (_isChangingDirection) {
				directionX = _moveDirection;
				currentSpeedX = DecreaseWithinBound(currentSpeedX, AirDecelerationX, 0);
			}
		}
	}

	private void GroundedMovement() {
		if (IsGrounded) {
			rb.gravityScale = NormalGravity;
			if (directionX != 0) {
				if (currentSpeedX == 0) {
					currentSpeedX = moveSpeedWalk;
				}
				else if (currentSpeedX < maxSpeedWalk) {
					currentSpeedX = IncreaseWithinBound(currentSpeedX, WalkAcceleration, maxSpeedWalk);
				}
				//TODO: Dashing key (run)
				
			}
			else if (currentSpeedX > 0) {
				currentSpeedX = DecreaseWithinBound(currentSpeedX, ReleaseDecelerationX, 0);
			}

			if (_isChangingDirection) {
				if (currentSpeedX > SkidTurnAroundSpeedX) {
					_moveDirection = directionX;
					turn = true;
					currentSpeedX = DecreaseWithinBound(currentSpeedX, SkidDecelerationX, 0);
				}
				else {
					_moveDirection = directionX;
					turn = false;
				}
			}
			else {
				turn = false;
			}

			if (directionHead != 0) {
				isHeadUp = directionHead > 0 && currentSpeedX == 0;
				if (directionHead < 0) {
					currentSpeedX = 0;
					isDuck = true;
				}
				else {
					isDuck = false;
				}
			}
			else {
				isDuck = false;
				isHeadUp = false;
			}
		}

		if (directionX != 0 && !_isChangingDirection) {
			_moveDirection = directionX;
		}


		if (directionX > 0) {
			transform.localScale = new Vector2(1, 1);
		}
		else if (directionX < 0) {
			transform.localScale = new Vector2(-1, 1);
		}
	}
	private float DecreaseWithinBound(float actual, float delta, int target) {
		actual -= delta;
		if (actual < target) {
			actual = target;
		}
		return actual;
	}
	private float IncreaseWithinBound(float actual, float delta, float max) {
		actual += delta;
		if (actual > max) {
			actual = max;
		}
		return actual;
	}
}