using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIJoystick : MonoBehaviour
{
    [SerializeField] private Joystick joyStick;
    [SerializeField] private Button attackBtn;
    public Vector2 moveDirection { get; private set; }
    public bool isAttack { get; private set; }
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector2(joyStick.Horizontal, joyStick.Vertical);
    }

}
