using UnityEngine;

public class PlayerMoveComponent : MoveComponent
{
    [Header("Player Move Settings")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Animator animator;
    private float verticalVelocity = 0f;
    private CharacterController characterController;



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



    protected override void Update()
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

        float moveAmount = Mathf.Clamp01(input.magnitude);

        if (input.sqrMagnitude < 0.01f)
        {
            Stop();
        }
        else
        {
            MoveDirection(input);
            UpdateAnimatorMove(moveAmount);

        }
    }    

    public override void MoveDirection(Vector3 direction)
    {
        if (direction.sqrMagnitude < 0.01f)
        {
            isMoving = false;
            return;
        }
        Vector3 move = direction.normalized * MoveSpeed * Time.deltaTime;
        move.y = 0; // Khong di chuyen theo truc y, tranh anh huong boi yeu to khac
        characterController.Move(move);
        
        isMoving = true;
        //targetPosition = null; // TAM THOI chua co di chuyen toi target nao

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
        Debug.Log("UpdateAnimatorMove called with moveAmount: " + moveAmount);
        animator.SetFloat("Move", moveAmount);
    }

}