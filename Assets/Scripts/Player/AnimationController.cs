using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    // Player
    [Header("Animator")] [SerializeField] 
    private Transform _body;

    private PlayerController _playerController;
    
    private Animator _anim;
    private bool _hasAnimator;
    // Start is called before the first frame update
    void Start()
    {
        _hasAnimator = _body.TryGetComponent(out _anim);
        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (_playerController.turn)
        {
            _anim.SetBool("isTurn", _playerController.turn);
            _playerController.turn = false;
            Debug.Log("From Controller turn");
            Debug.Log(_playerController.turn); 
        }else if (_playerController.walking) 
        {
            _anim.SetBool("isTurn", _playerController.turn);
            _anim.SetFloat("AnimMoveX", Mathf.Abs(_playerController._velocity.x *_playerController.input.Move.x));
            Debug.Log("Walking...");
        }
    }
    
}
