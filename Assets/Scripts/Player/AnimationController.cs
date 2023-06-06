using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
	// Player
	[Header("Animator")] [SerializeField] private Transform _body;

	private PlayerController _playerController;

	private Animator _anim;
	private bool _hasAnimator;

	private PlayerInput _input;

	// Start is called before the first frame update
	void Start() {
		_hasAnimator = _body.TryGetComponent(out _anim);
		_playerController = GetComponent<PlayerController>();
		_input = GetComponent<PlayerInput>();
	}

	// Update is called once per frame
	void Update() {

		if (_hasAnimator) {
			// _anim.speed = 2f;
			_anim.SetBool("isTurn", _playerController.turn);
			_anim.SetFloat("AnimMoveX", _playerController.currentSpeedX);
			
			if (_playerController.isHeadUp) {
				_anim.SetFloat("AnimLookY", _playerController.directionHead);
			}
			_anim.SetBool("isDuck", _playerController.isDuck);
			_anim.SetBool("isJump", _playerController.jumping);
			_anim.SetBool("isFall", _playerController.falling);
			_anim.SetBool("isRunNormal", _playerController.runningNormalSpeed);
			// _anim.SetBool("isRunMax", _playerController.runningMaxSpeed);
			// _anim.speed = 
		}
		else {
			Debug.Log("Cargue el body pedazo de basura");
		}
		
		
	}
}