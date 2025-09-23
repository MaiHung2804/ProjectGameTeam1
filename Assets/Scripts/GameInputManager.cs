using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.XR;
using UnityEngine;
using static MoveComponent;

public class GameInputManager : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    public static GameInputManager Instance { get; private set; }
    private float inputVectorSqrMin = 0.05f;

    private KeyCode jumpKey = KeyCode.Space;
    private KeyCode melleAttackKey = KeyCode.J;
    private KeyCode rangedAttackKey = KeyCode.K;

    public event Action OnJumpPressed;
    public event Action OnMelleAttackPressed;
    public event Action OnRangedAttackPressed;


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject); // Giu doi tuong nay khong bi huy khi load scene moi
    }

    // Tra ve vector di chuyen tu ban phim va joystick
    public bool GetMoveInput(out Vector2 MoveInput)
    {
        float horizontal = Input.GetAxis("Horizontal") + joystick.Horizontal;
        float vertical = Input.GetAxis("Vertical") + joystick.Vertical;
        Vector2 input = new Vector2(horizontal, vertical);
        if (input.sqrMagnitude < inputVectorSqrMin)
        {
            MoveInput = Vector2.zero;
            return false; 
        }
        MoveInput = input;
        return true;
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

    public void JumpUIButtonPressed()
    {
       OnJumpPressed?.Invoke();
    }

    public void MelleAttackUIButtonPressed()
    {
        OnMelleAttackPressed?.Invoke();
    }

    public void RangedAttackUIButtonPressed()
    {
        OnRangedAttackPressed?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            OnJumpPressed?.Invoke();
        }
        if (Input.GetKeyDown(melleAttackKey))
        {
            OnMelleAttackPressed?.Invoke();
        }
        if (Input.GetKeyDown(rangedAttackKey))
        {
            OnRangedAttackPressed?.Invoke();
        }
    }

}
