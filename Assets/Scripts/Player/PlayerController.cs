using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour {
	// Player
	[Header("RigidBody2D")] [SerializeField]
	private Transform player;

	// Movement
	[Header("Player Walk")] [SerializeField]
	private float moveSpeedWalk = 1f;

	private float _walkAcceleration = 1f;
	private float _releaseDecelerationX = 0.3f;
	private float _skidTurnAroundSpeedX = 3f;
	private float _skidDecelerationX = 0.5f;
	private float _airAccelerationX = 0.2f;
	private float _airDecelerationX = 0.15f;
	private float _jumpUpGravity = 0.8f;
	private float _jumpDownGravity = 1.3f;

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
	private float _normalGravity;

	// Flags
	public bool Running => Mathf.Abs(velocity.x) > maxSpeedWalk || Mathf.Abs(input.Move.x) > maxSpeedWalk;
	public bool turn;
	private bool IsGrounded { get; set; }
	public bool jumping;
	private bool _hasRb;
	public bool isHeadUp;
	public bool isDuck;


	private float _jumpVelocity = 6f;

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
		_normalGravity = rb.gravityScale;

	}

	// Update is called once per frame
	void Update() {
		directionX = input.Move.x;
		directionHead = input.Move.y;
		_isChangingDirection = currentSpeedX > 0 && _moveDirection * directionX < 0;
		IsGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
	}


	private void OnEnable() {
		velocity = Vector2.zero;
	}

	void FixedUpdate() {
		GroundedMovement();
		AirMovement();
		VerticalMovement();
	}

	private void VerticalMovement() {
		if (IsGrounded) {
			jumping = false;
			rb.gravityScale = _normalGravity;
		}

		if (!jumping) {
			if (IsGrounded && input.JumpHold) {
				rb.velocity = new Vector2(rb.velocity.x, _jumpVelocity);
				jumping = true;
			}
		}

		if (rb.velocity.y > 0 && input.JumpHold) {
			rb.gravityScale = _normalGravity * _jumpUpGravity;
		}
		else {
			rb.gravityScale = _normalGravity * _jumpDownGravity;
		}
			Debug.Log("Jump Hold " + input.JumpHold);
		Debug.Log("Jump Release" + input.JumpRelease);
		
	}

	private void AirMovement() {
		if (!IsGrounded) {
			//TODO: Set parameters air movement
			if (directionX != 0) {
				if (currentSpeedX == 0) {
					currentSpeedX = moveSpeedWalk;
				}
				else if (currentSpeedX < maxSpeedWalk) {
					currentSpeedX = IncreaseWithinBound(currentSpeedX, _airAccelerationX, maxSpeedWalk);
				}
				//TODO: Implement speed x if player is running
			}
			else if (currentSpeedX > 0) {
				currentSpeedX = DecreaseWithinBound(currentSpeedX, _releaseDecelerationX, 0);
			}

			if (_isChangingDirection) {
				directionX = _moveDirection;
				turn = false;
				currentSpeedX = DecreaseWithinBound(currentSpeedX, _airDecelerationX, 0);
			}
			
		}
		
	}

	private void GroundedMovement() {
		if (IsGrounded) {
			if (directionX != 0) {
				if (currentSpeedX == 0) {
					currentSpeedX = moveSpeedWalk;
				}
				else if (currentSpeedX < maxSpeedWalk) {
					currentSpeedX = IncreaseWithinBound(currentSpeedX, _walkAcceleration, maxSpeedWalk);
				}
				//TODO: Dashing key (run)
				
			}
			else if (currentSpeedX > 0) {
				currentSpeedX = DecreaseWithinBound(currentSpeedX, _releaseDecelerationX, 0);
			}

			if (_isChangingDirection) {
				if (currentSpeedX > _skidTurnAroundSpeedX) {
					_moveDirection = directionX;
					turn = true;
					currentSpeedX = DecreaseWithinBound(currentSpeedX, _skidDecelerationX, 0);
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
			rb.velocity = new Vector2(_moveDirection * currentSpeedX, rb.velocity.y);
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