using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMoveComponent : MoveComponent
{
    [Header("Player Move Settings")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private Camera mainCamera;

    private Animator animator;
    private CharacterController characterController;

    private float gravity = -9.81f;
    private float verticalVelocity = 0f;
    private float currentSpeed = 0f;


    protected override void Awake()
    {
        base.Awake();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        currentSpeed = 0f;
        if (!CheckNessessaryComponent())
        {
            enabled = false; // Vo hieu hoa component PlayerMoveComponent luc nay
        }
    }


    public override void MoveTo(Vector3 target)
    {
        targetPosition = target;
        isMoving = true;
    }




    public override void MoveByDirection(Vector3 direction)
    {
        if (direction.sqrMagnitude < 0.01f)
        {   Stop();
            return;
        }
        isMoving = true;
        // Quay nhan vat theo huong di chuyen
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        Vector3 move = direction.normalized * currentSpeed;

        // Ap dung gravity
        move.y = verticalVelocity;
        characterController.Move(move * Time.deltaTime);
        UpdateAnimatorMove(currentSpeed);

    }

    public override void Stop()
    {
        isMoving = false;
        targetPosition = null;
        UpdateAnimatorMove(0f);
    }

    protected override void HandleMoving()
    {
        UpdateVerticalVelocity();

        Vector3 input = GetInputFromDevices();
        if (input.sqrMagnitude < 0.01f) 
        {
            Vector3 gravityMove = new Vector3(0, verticalVelocity, 0);
            characterController.Move(gravityMove * Time.deltaTime);
            Stop();
            return;
        }
        
        


        // Tinh huong di chuyen theo camera
        Vector3 moveDir = ConvertInputToDirectionByCamera(input);

        // Tinh currentSpeed
        float inputMagnitude = Mathf.Clamp01(input.magnitude);
        currentSpeed = inputMagnitude * MoveSpeed;

        MoveByDirection(moveDir);


    }

    private void UpdateVerticalVelocity()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = 0f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

    }

    private Vector3 GetInputFromDevices()
    {
        Vector3 keyboardInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 joystickInput = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        return keyboardInput + joystickInput;
    }    

    private Vector3 ConvertInputToDirectionByCamera(Vector3 input)
    {

        // Chuyen doi input theo huong nhin trai phai tu Camera. Rat quan trong
        Vector3 camForward = mainCamera.transform.forward;
        camForward.y = 0;
        camForward.Normalize();
        Vector3 camRight = mainCamera.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        // Tinh huong di chuyen theo camera
        return (camForward * input.z + camRight * input.x).normalized;


        // Lay theo toa do dia phuong cua nhan vat. Nay khong dung nua
        // Vector3 moveDir = input.sqrMagnitude > 0.01f ? input.normalized : Vector3.zero;
        // Return moveDir;
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
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned and no Camera tagged as MainCamera found.");
            return false;
        }

        return true;
    }

    private void UpdateAnimatorMove(float moveAmount)
    {
        // Cap nhat Animation Speed: 0 Idle , 2 Walk, 5 Run
        animator.SetFloat("Speed", moveAmount);
    }

}