using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMoveComponent : MoveComponent
{
    [Header("Player Move Settings")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private float gravity = -9.81f;
    private Animator animator;
    
    private float verticalVelocity = 0f;
    private CharacterController characterController;

    [SerializeField] private float moveSmoothTime = 0.1f;
    private float currentMoveAmount = 0f;
    private float moveAmountVelocity = 0f; // Toc do thay doi cua moveAmount

    private float lastAnimatorMove = 0f;
    private const float animatorMoveThreshold = 0.1f;

    protected override void Awake()
    {
        base.Awake();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        if (!CheckNessessaryComponent())
        {
            enabled = false; // Vo hieu hoa component PlayerMoveComponent luc nay
        }
    }



    protected void Update()
    {
        // Khong goi base.Update() de tranh viec di chuyen den targetPosition
        // Player se di chuyen theo joystick
        //base.Update();
        HandleGravity();
        HandleMoving();
    }

    private void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = 0f; // Reset van toc khi o tren mat dat
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime; // Ap dung gia toc do
        }
        Vector3 gravityMove = new Vector3(0, verticalVelocity, 0);
        characterController.Move(gravityMove * Time.deltaTime);

    }

    private void HandleMoving()
    {
        if (!characterController.isGrounded)
        {
            Stop();
            return;
        }
        Vector3 keyboardInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 joystickInput = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        Vector3 input = keyboardInput + joystickInput;

        float inputMagnitude = Mathf.Clamp01(input.magnitude);
        float targetSpeed = inputMagnitude * MoveSpeed;

        Vector3 moveDir = input.sqrMagnitude > 0.01f ? input.normalized : Vector3.zero;
        Vector3 move = moveDir * targetSpeed;
        move.y = 0;
        characterController.Move(move * Time.deltaTime);

        //currentMoveAmount = inputMagnitude;

        // Muot gia tri speed
        currentMoveAmount = Mathf.Lerp(currentMoveAmount, inputMagnitude, Time.deltaTime * 10f);

        // Xoay ra hien tuong nhay giat khi di chuyen cham
        if (currentMoveAmount > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }

        UpdateAnimatorMove(currentMoveAmount);


    }

    private bool CheckNessessaryComponent()
    {
        if (joystick == null)
        {
            Debug.LogError("Joystick is not assigned in PlayerMoveComponent.");
            return false;
        }
        if (characterController == null)
        {
            Debug.LogError("CharacterController component is missing.");
            return false;
        }
        if (animator == null)
        {
            Debug.LogError("Animator component is missing.");
            return false;
        }
        return true;
    }

    public override void Stop()
    {
        base.Stop();
        UpdateAnimatorMove(0f);
    }

    private void UpdateAnimatorMove(float moveAmount)
    {
        // Cap nhat Animation Speed: 0 Idle , 0.2 Walk, 1 Run
        animator.SetFloat("Speed", moveAmount);
    }

}