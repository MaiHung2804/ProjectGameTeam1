using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.EventSystems;
using static MoveComponent;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    public static InputManager Instance { get; private set; }
    private float inputVectorSqrMin = 0.05f;

    private KeyCode jumpKey = KeyCode.Space;
    private KeyCode meleeAtackKey = KeyCode.J; // skill 1
    private KeyCode rangedAttackKey = KeyCode.K; // skill 2

    public event Action OnJumpPressed;
    public event Action OnMeleeAttackPressed;
    public event Action OnRangedAttackPressed;

    private bool isJumpPressed = false;
    private bool isMeleeAttackPressed = false;
    private bool isRangedAttackPressed = false;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject); // Giu doi tuong nay khong bi huy khi load scene moi
    }

    // Tra ve vector di chuyen tu ban phim va joystick
    public Vector2 GetMoveInput()
    {
        float horizontal = Input.GetAxis("Horizontal") + joystick.Horizontal;
        float vertical = Input.GetAxis("Vertical") + joystick.Vertical;
        Vector2 input = new Vector2(horizontal, vertical);
        if (input.sqrMagnitude < inputVectorSqrMin)
        {
            return Vector2.zero;
        }
        return input;
    }

    // Tra ve vector xoay tu Chuot
    public Vector2 GetRotateFromMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        return new Vector2(mouseX, mouseY);
    }

    public float GetZoomFromMouse()
    {
        return Input.GetAxis("Mouse ScrollWheel");
    }

    public bool IsControlPressed()
    {
        return Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
    }

    public bool IsJumpPressed()
    {
        return Input.GetKey(jumpKey) || isJumpPressed;
    }
    public bool IsMeleeAttackPressed()
    {
        return Input.GetKey(meleeAtackKey) || isMeleeAttackPressed;
    }
    public bool IsRangedAttackPressed()
    {
        return Input.GetKey(rangedAttackKey) || isRangedAttackPressed;
    }
    public bool IsAttackPressed()
    {
        return IsMeleeAttackPressed() || IsRangedAttackPressed();
    }

    // Can gan OnPointerDown va OnPointerUp de bat su kien UI trong Unity o cac nut
    public void JumpUIDown()
    {
        isJumpPressed = true;
        OnJumpPressed?.Invoke();
    }

    public void JumpUIUp()
    {
        isJumpPressed = false;
    }

    public void MeleeAttackUIDown()
    {
        isMeleeAttackPressed = true;
        OnMeleeAttackPressed?.Invoke();
    }
    public void MeleeAttackUIUp()
    {
        isMeleeAttackPressed = false;
    }

    public void RangedAttackUIDown()
    {
        isRangedAttackPressed = true;
        OnRangedAttackPressed?.Invoke();
    }
    public void RangedAttackUIUp()
    {
        isRangedAttackPressed = false;
    }
}
