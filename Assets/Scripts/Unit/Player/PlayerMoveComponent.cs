using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMoveComponent : MoveComponent
{
    [Header("Player Move Settings")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private Camera mainCamera;

    private Animator animator;
    private AnimationComponent animationComponent;
    private CharacterController characterController;

    private float gravity = -9.81f;
    private float verticalVelocity = 0f;
    private float currentSpeed = 0f;
    private float airborneThreshold = 0.4f; // Thoi gian cho phep nhan vat nhay sau khi roi khoi mat dat
    private float lastGroundedTime = 0f;
    private Vector3 lastMoveDirection = Vector3.zero;



    protected override void Awake()
    {
        base.Awake();
        currentSpeed = 0f;

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animationComponent = GetComponent<AnimationComponent>();
        
        if (!CheckNessessaryComponent())
        {
            enabled = false; // Vo hieu hoa component PlayerMoveComponent luc nay
        }
    }

    private void Start()
    {
        // Cap nhat thoi diem cuoi cung nhan vat tiep dat
        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }
        else
        {
            lastGroundedTime = -Mathf.Infinity;
        }
            
    }

    public override void MoveTo(Vector3 target)
    {
        targetPosition = target;
        moveState = MoveState.Moving;
    }




    public override void MoveByDirection(Vector3 direction)
    {
        if (direction.sqrMagnitude < 0.01f)
        {   Stop();
            return;
        }
        moveState = MoveState.Moving;
        // Quay nhan vat theo huong di chuyen
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        Vector3 move = direction.normalized * currentSpeed;

        // Ap dung gravity
        move.y = verticalVelocity;
        characterController.Move(move * Time.deltaTime);
    }

    public override void Stop()
    {
        moveState = MoveState.Idle;
        targetPosition = null;
        animationComponent.MoveSpeed(0f);
    }

    protected override void HandleMoving()
    {
        UpdateVerticalVelocity();

        if (  CheckRealFalling() )
        {
            HanldeFalling();
            return;
        }

        if ( (moveState == MoveState.Falling) && characterController.isGrounded)
        {
            HandleLanding();
            return;
        }

        if ( ( moveState == MoveState.Landing) && animationComponent.IsLandingEnd )
        {
            // Debug.Log("Landing End");
            moveState = MoveState.Idle;
            animationComponent.Landing(false, currentSpeed);
        }


        Vector3 input = GetInputFromDevices();
        if (input.sqrMagnitude < 0.05f) // Khong de qua nho de tranh rung
        {
            Stop();
            return;
        }
        else
        {
            HandleMovingByInput(input);
        }

    }

    private void HanldeFalling()
    {
        // Ap dung van toc ngang
        Vector3 horizontalMove = Vector3.zero;
        if (currentSpeed > 0.1f)
        {
            horizontalMove = lastMoveDirection * currentSpeed;
        }
        // Ap dung gravity
        Vector3 gravityMove = new Vector3(horizontalMove.x, verticalVelocity, horizontalMove.y);

        characterController.Move(gravityMove * Time.deltaTime);

        // Bat dau roi
        if ( (moveState == MoveState.Idle) || (moveState == MoveState.Moving) )
        {
            // Debug.Log("Start Falling");  
            moveState = MoveState.Falling;
            animationComponent.Falling(true);
        }

    }

    private void HandleLanding()
    {
        animationComponent.Falling(false);
        animationComponent.Landing(true, currentSpeed);
        moveState = MoveState.Landing;
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

    private bool CheckRealFalling()
    {
        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
            return false;
        }
        else
        {
            // Them verticalVelocity < -0.1f de tranh truong hop nhan vat vua roi tu tren xuong, chua kip rơi da bi xet la falling
            if ( (Time.time - lastGroundedTime > airborneThreshold) && (verticalVelocity < -0.2f) )
            {
                return true; 
            }
            else
            {
                return false; 
            }
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

    private void HandleMovingByInput(Vector3 input)
    {
        // Tinh huong di chuyen theo camera
        Vector3 moveDir = ConvertInputToDirectionByCamera(input);

        // Tinh currentSpeed
        float inputMagnitude = Mathf.Clamp01(input.magnitude);
        currentSpeed = inputMagnitude * MoveSpeed;
        if (currentSpeed > 0.1f)
        {
            lastMoveDirection = moveDir;
        }

        MoveByDirection(moveDir);

        // Cap nhat animation
        animationComponent.MoveSpeed(currentSpeed);

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
        if (animationComponent == null)
            {
                Debug.LogError("AnimationComponent is missing.");
                return false;
        }

        return true;
    }

   

}