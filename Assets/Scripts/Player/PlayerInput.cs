using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 Move { get; private set; }
    public bool RunOn { get; private set; }
    public bool JumpRelease { get; private set; }
    
    public bool JumpHold { get; private set;}
    public bool JumpDown { get; private set;}
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DesktopInputs();
    }
    private void DesktopInputs()
    {
        Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        RunOn = Input.GetKey(KeyCode.Z);
        JumpRelease = Input.GetButtonUp("Jump");
        JumpHold = Input.GetButton("Jump");
        JumpDown = Input.GetButtonDown("Jump");
    }
}