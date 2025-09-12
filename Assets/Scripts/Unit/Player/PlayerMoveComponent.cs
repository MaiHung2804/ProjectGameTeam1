using System.Collections;
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


    private bool enterThisState = true;
    private KeyCode jumKey = KeyCode.Space;
    private Vector3 inputVector = Vector3.zero;
    private float inputVectorSqrMin = 0.05f; 

    private float gravity = -9.81f;
    private float jumpForce = 6f;
    private float verticalVelocity = 0f;
    private float verticalVelocityMax = -2f;
    private float currentSpeed = 0f;
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
        moveState = MoveState.Falling;
        
    }

    protected override void HandleActivites()
    {
        UpdateVerticalVelocity();

        switch (moveState)
        {
            case MoveState.Falling:
                HanldeFalling();
                //Debug.Log("Falling");
                break;
            case MoveState.Landing:
                HandleLanding();
                //Debug.Log("Landing");
                break;
            case MoveState.Moving:
                HandleMoving();
                //Debug.Log("Moving");
                break;
            case MoveState.Jumping:
                HandleJumping();
                //Debug.Log("Jumping");
                break;
            default: // Idle
                HandleIdle();
                //Debug.Log("Idle");
                break;
        }

    }
    private void HandleIdle()
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, MoveSpeed * 1.5f * Time.deltaTime);
        animationComponent.MoveSpeed(currentSpeed);

        if (Input.GetKey(KeyCode.Space))
        {
            moveState = MoveState.Jumping;
            enterThisState = true;
            lastMoveDirection = Vector3.zero;
            landingSpeed = 0f;
            return;
        }

        inputVector = MoveInputFromDevices();
        if (inputVector.sqrMagnitude < inputVectorSqrMin)
        {
            return;
        }
        else
        {
            moveState = MoveState.Moving;
            enterThisState = true;
        }
        // Mac dinh dang Idle thì khong tu dung Falling duoc
    }

    private void HandleMoving()
    {
        // Tinh huong di chuyen theo camera
        Vector3 moveDir = ConvertInputToDirectionByCamera(inputVector);

        // Tinh currentSpeed
        float inputMagnitude = Mathf.Clamp01(inputVector.magnitude);
        currentSpeed = inputMagnitude * MoveSpeed;

        MoveByDirection(moveDir);

        // Cap nhat animation
        animationComponent.MoveSpeed(currentSpeed);

        //Exit condition

        if (CheckRealFalling())
        {
            moveState = MoveState.Falling;
            landingSpeed = currentSpeed;
            lastMoveDirection = moveDir;
            enterThisState = true;
            return;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            moveState = MoveState.Jumping;
            lastMoveDirection = moveDir;     
            landingSpeed = currentSpeed;
            enterThisState = true;
            return;
        }

        inputVector = MoveInputFromDevices();
        if (inputVector.sqrMagnitude < inputVectorSqrMin)
        {
            moveState = MoveState.Idle;
            enterThisState = true;
        }

    }

    private void HanldeFalling()
    {
        if (enterThisState)
        {
            animationComponent.Falling(true);
            enterThisState = false;
        }
        // landingSpeed: Can phai duoc Cap nhat khi bat dau roi hoac nhay. Giu nguyen van toc khi roi
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


        if (characterController.isGrounded)
        {
            enterThisState = true;
            animationComponent.Falling(false);
            moveState = MoveState.Landing;
            return;
        }

    }

    private void HandleLanding()
    {
        if (enterThisState)
        {
            animationComponent.Landing(true, landingSpeed);
            enterThisState = false;
        }

        inputVector = MoveInputFromDevices();
        if (inputVector.sqrMagnitude > inputVectorSqrMin)
        {
            moveState = MoveState.Moving;
            enterThisState = false;
            return;
        }

         if (animationComponent.IsLandingEnd)
        {
            animationComponent.Landing(false, currentSpeed);
            moveState = MoveState.Idle;
            Debug.Log(" 1111 ");
            // KIEM TRA LAI PHAN NAY SAU: mot so truong hop khong thoat duoc Landing khi Falling.
        }

    }

    private void HandleJumping()
    {
        if (enterThisState)
        {
            verticalVelocity = jumpForce;
            animationComponent.Jumping(true);
            enterThisState = false;
        }

        // Di chuyen theo huong nhay truoc do
        Vector3 move = lastMoveDirection * landingSpeed * 1.5f;
        move.y = verticalVelocity;
        characterController.Move(move * Time.deltaTime);

        Debug.Log("Move " + move + ", verticalVelocity=" + verticalVelocity);

        // Ap dung trong luc
        verticalVelocity += gravity * Time.deltaTime;

        // Khi van toc am, thi bat dau roi
        if (verticalVelocity <= 0f)
        {
            moveState = MoveState.Falling;
            enterThisState = true;
            animationComponent.Jumping(false);
            animationComponent.Falling(true);
        }

    }

    public override void MoveByDirection(Vector3 direction)
    {
        moveState = MoveState.Moving;
        // Quay nhan vat theo huong di chuyen
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        Vector3 move = direction.normalized * currentSpeed;

        // Ap dung gravity
        move.y = verticalVelocity;
        characterController.Move(move * Time.deltaTime);
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

    private bool IsGroundedByRaycast()
    {
        RaycastHit hit;
        // 0.4f; Dieu chinh theo chieu cao va skin width cua CharacterController
        if ( Physics.Raycast(transform.position, Vector3.down, out hit, 0.4f) )
        {
            return true;
            // Tam thoi khong kiem tra slope
            //float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            //if (slopeAngle < characterController.slopeLimit) // thuong la 45 do
            //    return true;
            //else
            //    return false;
        }
        return false;
    }

    private bool CheckRealFalling()
    {
        if (verticalVelocity < verticalVelocityMax)
        {
            if ( IsGroundedByRaycast() )
                return false;
            return true;
        }
        return false;
    }


    private Vector3 MoveInputFromDevices()
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
        if (animationComponent == null)
            {
                Debug.LogError("AnimationComponent is missing.");
                return false;
        }

        return true;
    }

   

}