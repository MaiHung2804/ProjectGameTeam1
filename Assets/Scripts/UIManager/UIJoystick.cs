using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIJoystick : MonoBehaviour
{
    //[SerializeField] private Joystick joyStick;
    [SerializeField] private Button meleeAttackBtn;
    [SerializeField] private Button rangeAttackBtn;
    public InputManager inputManager;


    // Update is called once per frame
    void Update()
    {

    }

    public void OnMeleeAttackButtonDown()
    {
        inputManager.MeleeAttackUIDown();
    }
    public void OnMeleeAttackButtonUp()
    {
        inputManager.MeleeAttackUIUp();
    }
    public void OnMoveJoyStick()
    {
        inputManager.GetMoveInput();
    }
   
}
