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
    
    private KeyCode jumpKey = KeyCode.Space;
    private float gravity = -9.81f;
    private float jumpForce = 6f;
    private float verticalVelocity = 0f;
    private float currentSpeed = 0f;
    private float airborneThreshold = 0.4f; // Thoi gian cho phep nhan vat nhay sau khi roi khoi mat dat
    private float lastGroundedTime = 0f;
    private Vector3 lastMoveDirection = Vector3.zero;
    private float landingSpeed = 0f; 



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
        
        // Dat nhan vat luc dau o tren cao

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

        // Lam the nay animation tut ve 0 qua nhanh
        //animationComponent.MoveSpeed(0f);
    }

    protected override void HandleMoving()
    {
        
        UpdateVerticalVelocity();

        if (moveState == MoveState.Jumping)
        {
            HandleMovingInJumping();
            return;
        }

        if (moveState == MoveState.Idle)         
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, MoveSpeed * 1.5f * Time.deltaTime);
            animationComponent.MoveSpeed(currentSpeed);
        }

        if ( CheckRealFalling() )
        {
            HanldeFalling();
            return;
        }

        if ( (moveState == MoveState.Falling) && characterController.isGrounded)
        {
            HandleLanding();
            return;
        }

        // Chua chay chuan lam. DANG CO VAN DE CAN XEM LAI
        if ((moveState == MoveState.Landing) && animationComponent.IsLandingEnd)
        {
            moveState = MoveState.Idle;
            Debug.Log("Landing End");
            animationComponent.Landing(false, currentSpeed);
            animationComponent.IsLandingEnd = false;
        }

        if ( JumpInputFromDevices() )
        {
            StartJumping();
            return;
        }

        Vector3 input = MoveInputFromDevices();



        if (!((moveState == MoveState.Moving) || (moveState == MoveState.Idle)))
        {
            Debug.Log ("Move State: " + moveState.ToString()); 
        } // Chi nhan input khi dang o trang thai Moving hoac Idle



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
        if (moveState == MoveState.Moving)
        {
            landingSpeed = currentSpeed;
        }

        // Giu nguyen van toc khi roi
        //landingSpeed = Mathf.MoveTowards(landingSpeed, 0f, MoveSpeed * 0.5f * Time.deltaTime); // 0.5f la he so giam toc khi roi, cang nho cang cham

        // Ap dung van toc ngang
        Vector3 horizontalMove = Vector3.zero;
        if (currentSpeed > 0.1f)
        {
            horizontalMove = lastMoveDirection * landingSpeed;
        }
        // Ap dung gravity
        Vector3 gravityMove = new Vector3(horizontalMove.x, verticalVelocity, horizontalMove.y);

        characterController.Move(gravityMove * Time.deltaTime);

        // Bat dau roi
        if ( (moveState == MoveState.Idle) || (moveState == MoveState.Moving)  )
        {
            // Debug.Log("Start Falling");  
            moveState = MoveState.Falling;
            animationComponent.Falling(true);
        }

    }

    private void HandleLanding()
    {
        animationComponent.Falling(false);
        animationComponent.Landing(true, landingSpeed);
        moveState = MoveState.Landing;
    }

    private void StartJumping()
    {
        float jumpHorizontalSpeed = 0;
        Vector3 jumpDirection = Vector3.zero;
        if (currentSpeed > 0.1f)
        {
            jumpHorizontalSpeed = currentSpeed * 1.5f;
            jumpDirection = lastMoveDirection;
        }
        verticalVelocity = jumpForce;
        moveState = MoveState.Jumping;
        animationComponent.Jumping(true);

        landingSpeed = jumpHorizontalSpeed;
    }

    private void HandleMovingInJumping()
    {

        Vector3 move = lastMoveDirection * landingSpeed;
        move.y = verticalVelocity;
        characterController.Move(move * Time.deltaTime);

        // Ap dung giam toc khi bay
        verticalVelocity += gravity * Time.deltaTime;

        Debug.Log("Jumping Moving");

        // Nhay cuc diem, bat dau roi
        if (verticalVelocity <= 0f)
        {
            Debug.Log("Start Falling from Jumping");

            moveState = MoveState.Falling;

            animationComponent.Jumping(false);
            animationComponent.Falling(true);
        }

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
        if (moveState == MoveState.Idle)
        {
            lastGroundedTime = Time.time;
            return false;
        }
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


    private Vector3 MoveInputFromDevices()
    {
        Vector3 keyboardInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 joystickInput = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        return keyboardInput + joystickInput;
    }    

    private bool JumpInputFromDevices()
    {
        bool keyboardJump = Input.GetKeyDown(jumpKey);
        return keyboardJump;
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