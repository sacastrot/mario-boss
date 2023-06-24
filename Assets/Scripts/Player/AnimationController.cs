using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimationController : MonoBehaviour {
	// Player
	[Header("Animator")] [SerializeField] private Transform _body;

	private PlayerController _playerController;

	private Animator _anim;
	private bool _hasAnimator;

	private PlayerInput _input;
	private float _growingUpDuration = 1f;
	private float _timerAnim;
	public bool isBigMario;

	// Start is called before the first frame update
	void Start() {
		_hasAnimator = _body.TryGetComponent(out _anim);
		_playerController = GetComponent<PlayerController>();
		_input = GetComponent<PlayerInput>();
		_timerAnim = _growingUpDuration;
	}

	// Update is called once per frame
	void Update() {
		
		if (_hasAnimator) {
			// _anim.speed = 2f;
			
			Debug.Log("Growing UP: " + _playerController.grownUp);
			if (_playerController.grownUp) {
				if (_timerAnim > 0) {
					_anim.SetBool("isGrowingUp", _playerController.grownUp);
					_anim.SetBool("isRunNormal", false);
					_anim.SetFloat("AnimMoveX", 0);
					_anim.SetFloat("AnimLookY", 0);

					_timerAnim -= Time.deltaTime;
				} 
				else if (!isBigMario) {
					_anim.SetBool("isGrowingUp", false);
					_anim.SetBool("isMarioBig", true);
					_anim.SetBool("isTurn", false);
					_anim.SetFloat("AnimMoveX", 0);
					_anim.SetFloat("AnimLookY", 0);
					_anim.SetBool("isDuck", false);
					_anim.SetBool("isJump", false);
					_anim.SetBool("isFall", false);
					_anim.SetBool("isRunNormal", false);
					_anim.SetBool("invensiblePowerUp", false);
					isBigMario = true;
				}
				_anim.SetFloat("AnimMoveXBig", _playerController.currentSpeedX);
				_anim.SetFloat("AnimMoveYBig", _playerController.directionHead);
				_anim.SetBool("isJumpBig", _playerController.jumping);
				_anim.SetBool("isFallBig", _playerController.falling);
				_anim.SetBool("isRunNormalBig", _playerController.runningNormalSpeed);
				_anim.SetBool("invensiblePowerUp", _playerController.invensible);


			} else {
				if (!_playerController._currentGrowUp) {
					_timerAnim = _growingUpDuration;
					isBigMario = false;
				}
				_anim.SetBool("isMarioBig", false);
				_anim.SetBool("isTurn", _playerController.turn);
				_anim.SetFloat("AnimMoveX", _playerController.currentSpeedX);
				_anim.SetFloat("AnimLookY", _playerController.directionHead);
				_anim.SetBool("isDuck", _playerController.isDuck);
				_anim.SetBool("isJump", _playerController.jumping);
				_anim.SetBool("isFall", _playerController.falling);
				_anim.SetBool("isRunNormal", _playerController.runningNormalSpeed);
				_anim.SetBool("invensiblePowerUp", _playerController.invensible);
			}
			// _anim.SetBool("isRunMax", _playerController.runningMaxSpeed);
			// _anim.speed = 
		}
		else {
			Debug.Log("Cargue el body pedazo de basura");
		}
		
		
	}
}