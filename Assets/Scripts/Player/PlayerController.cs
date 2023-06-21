using System;
using System.Text.RegularExpressions;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

public enum CollisionSide { None, Top, Bottom, Right, Left }
public class PlayerController : MonoBehaviour
{

	public LayerMask Ground;
	
	// Player
	[Header("RigidBody2D")] [SerializeField]
	private Transform player;
	
	// Movement
	[Header("Player Walk")] [SerializeField]
	private float moveSpeedWalk = 1f;
	[Header("Max Speed Walk")] [SerializeField]
	private float maxSpeedWalk = 5f;
	[Header("Player Run")] [SerializeField]
	private float moveSpeedRun = 7f;
	[Header("Max Speed Run")] [SerializeField]
	private float maxSpeedRun = 10f;

	[Header("Line Ray")]
	[SerializeField]
	public LineRenderer lineRend;
	
	
	private const float WalkAcceleration = 0.2f;
	private const float SpeedAcceleration = 0.04f;
	private const float ReleaseDecelerationX = 0.3f;
	private const float SkidTurnAroundSpeedX = 3f;
	private const float SkidDecelerationX = 0.5f;
	private const float AirAccelerationX = 0.2f;
	private const float AirDecelerationX = 0.15f;
	private const float JumpUpGravity = 2.7888f;
	private const float JumpDownGravity = 5f;
	
	public float currentSpeedX;
	private float _moveDirection;
	private bool _isChangingDirection;

	// Physics
	private const float NormalGravity = 1;

	// Flags
	// public bool Running => Mathf.Abs(velocity.x) > maxSpeedWalk || Mathf.Abs(input.Move.x) > maxSpeedWalk;
	[FormerlySerializedAs("runnig")] public bool running;
	public bool runningNormalSpeed;
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
	public bool jumpDown;
	

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
		jumpDown = input.JumpDown;
		running = input.RunOn && directionX != 0;
		_isChangingDirection = currentSpeedX > 0 && _moveDirection * directionX < 0;
		IsGrounded = checkGrounded();
		//This lines was in FixedUpdate
		VerticalMovement();
		if(IsGrounded) GroundedMovement();
		if(!IsGrounded) AirMovement();
		Debug.Log("Grounded" + IsGrounded);
	}


	private void OnEnable() {
		velocity = Vector2.zero;
	}

	void FixedUpdate() {
		setDirection(); //New Function 
		rb.velocity = new Vector2(_moveDirection * currentSpeedX, rb.velocity.y);
	}

	private void VerticalMovement() {
		if (IsGrounded) {
			jumping = false;
			falling = false;
		}
		else if (rb.velocity.y < 0) {
			falling = true;
			// jumping = false;
		}
		
		if (IsGrounded && jumpDown) {
			jumping = true;
			rb.velocity = new Vector2(rb.velocity.x, JumpVelocity);
		}

		if (rb.velocity.y > 0 && input.JumpHold && !IsGrounded) {
			jumping = true;
			rb.gravityScale = NormalGravity * JumpUpGravity;
		}
		else if(!IsGrounded) {
			rb.gravityScale = NormalGravity * JumpDownGravity;
		}
	}

	private void AirMovement() {
	//TODO: Set parameters air movement
	runningNormalSpeed = false;
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
			currentSpeedX = DecreaseWithinBound(currentSpeedX, currentSpeedX, 0);
		}
	}

	private void GroundedMovement() {
		rb.gravityScale = NormalGravity;
		if (directionX != 0) {
			//TODO: Dashing key (run)
			if (running)
			{
				if (currentSpeedX < moveSpeedRun)
				{
					currentSpeedX = IncreaseWithinBound(currentSpeedX, 0.2f, moveSpeedRun);
				}
				else if (currentSpeedX >= moveSpeedRun)
				{
					runningNormalSpeed = true;
				}
			}
			else
			{
				if (currentSpeedX == 0) {
					currentSpeedX = moveSpeedWalk;
				}
				else if (currentSpeedX <= maxSpeedWalk) {
					currentSpeedX = IncreaseWithinBound(currentSpeedX, WalkAcceleration, maxSpeedWalk);
				}
				else if (currentSpeedX > maxSpeedWalk)
				{
					currentSpeedX = DecreaseWithinBound(currentSpeedX, ReleaseDecelerationX, 0);
				}
			}
			
		}
		else if (currentSpeedX > 0) {
			currentSpeedX = DecreaseWithinBound(currentSpeedX, ReleaseDecelerationX, 0);
		}

		if (!running)
		{
			// runningMaxSpeed = false;
			runningNormalSpeed = false;
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

	private void setDirection()
	{
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
	
	private CollisionSide collisionType;
	private int currentStayCollision;
	
	private bool checkGrounded() {
		RaycastHit2D hit;
		hit = Physics2D.Raycast(player.position, Vector2.down, 1f, Ground);
		return rb.IsTouchingLayers(Ground) && hit;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Vector2 collisionNormal = collision.contacts[0].normal;

		float angle = Vector2.Angle(collisionNormal, Vector2.up);

		if (angle < 45f)
		{
			// Top collision
			Debug.Log("Bottom collision");
			collisionType =  CollisionSide.Bottom;
		}
		else if (angle > 135f)
		{
			// Bottom collision
			Debug.Log("Top collision");
			collisionType = CollisionSide.Top;
		}
		else if (collisionNormal.x > 0f)
		{
			// Right collision
			Debug.Log("Left collision");
			collisionType = CollisionSide.Left;

		}
		else
		{
			// Left collision
			Debug.Log("Right collision");
			collisionType = CollisionSide.Right;
		}

		currentStayCollision = collision.gameObject.layer;
	}

	private void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.layer == 6 && currentStayCollision != 6) {
			if (collisionType != CollisionSide.Bottom) {
				collisionType = CollisionSide.Bottom;
			}
		}
	}
}