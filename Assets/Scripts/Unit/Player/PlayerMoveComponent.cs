using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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


    private bool isEnteringState = true;
    private bool jumpRequested = false;
    private KeyCode jumpKey = KeyCode.Space;
    
    private float inputVectorSqrMin = 0.05f; 
    private float groundSpeedReductionFactor = 1.2f; // Cang lon thi CurrentSpeed giam ve 0 cang nhanh khi khong co input
    private float airSpeedReductionFactor = 0.5f; // Cang lon thi CurrentSpeed giam ve 0 cang nhanh khi khong co input
    private float horizontalJumpForceFactor = 1.2f; // Dung khi nhay nhan voi speed ngang
    private float verticalJumpForce = 6.5f;
    private bool isFallingFromJump = false;

    private float gravity = -9.81f;
    private float verticalVelocity = 0f;
    private float verticalVelocityMax = -2f;


    // Cac bien quan trong Quan ly trang thai di chuyen, nhap so lieu
    private float currentSpeed = 0f;
    private Vector3 currentDir = Vector3.zero;
    private Vector3 lastDir = Vector3.zero;




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
        // Dat nhan vat luc dau o tren cao
        moveState = MoveState.Falling;
        
    }

    public override void HandleActivites()
    {
        UpdateVerticalVelocity();

        switch (moveState)
        {
            case MoveState.Falling:
                HanldeFalling();
                break;
            case MoveState.Landing:
                HandleLanding();
                break;
            case MoveState.Moving:
                HandleMoving();
                break;
            case MoveState.Jumping:
                HandleJumping();
                break;
            default: // Idle
                HandleIdle();
                break;
        }

    }
    private void HandleIdle()
    {
        if (isEnteringState)
        {
            animationComponent.MoveSpeed(currentSpeed);
            isEnteringState = false;
            Debug.Log("Enter Idle" + currentSpeed);
        }

        if (Input.GetKey(jumpKey) || jumpRequested)
        {
            moveState = MoveState.Jumping;
            jumpRequested = false;
            isEnteringState = true;
            Debug.Log("Idle -> Jumping");
            return;
        }

        if (GetDirectionFromDevices(out currentDir, out float speedIntensity))
        {
            currentSpeed = speedIntensity * MaxSpeed;
            moveState = MoveState.Moving;
            isEnteringState = true;
            Debug.Log("Idle -> Moving");
        }
        
        // Mac dinh Idle thi khong co luc tac dong thi khong Falling duoc. Tru khi co skill Enemy day nhan vat.
    }

    private void HandleMoving()
    {
        if (isEnteringState)
        {
            animationComponent.MoveSpeed(currentSpeed);
            isEnteringState = false;
        }

        if (!GetDirectionFromDevices(out currentDir, out float speedIntensity))
        {
            // Luc nay khong co input, currentDirection luc nay van giu nguyen. CurrentSpeed giam dan ve 0
            currentDir = lastDir;
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, groundSpeedReductionFactor * MaxSpeed * Time.deltaTime);
        }
        else
        {
            currentSpeed = speedIntensity * MaxSpeed;
            lastDir = currentDir;       // Luu huong di chuyen moi nhat
        }
        
        MoveToDirection(currentDir);

        animationComponent.MoveSpeed(currentSpeed);

        //Exit condition
        if (IsTrueFalling())
        {
            moveState = MoveState.Falling;
            isEnteringState = true;
            Debug.Log("Moving -> Falling " + " speed " + currentSpeed + " direction " + currentDir);
            return;
        }

        if (Input.GetKey(jumpKey) || jumpRequested)  
        {
            moveState = MoveState.Jumping;
            isEnteringState = true;
            jumpRequested = false;
            Debug.Log("Moving -> Jumping " + " speed " + currentSpeed + " direction " + currentDir);
            return;
        }

        if (currentSpeed == 0 )
        {
            moveState = MoveState.Idle;
            isEnteringState = true;
            currentDir = Vector3.zero;
            Debug.Log("Moving -> Idle");
        }

    }

    private void HanldeFalling()
    {
        if (isEnteringState)
        {
            animationComponent.Falling(true);
            isEnteringState = false;
        }
        Vector3 horizontalMove;

        if (isFallingFromJump)
        {
            horizontalMove = currentDir * currentSpeed * horizontalJumpForceFactor;
        }
        else 
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, airSpeedReductionFactor * MaxSpeed * Time.deltaTime);
            horizontalMove = currentDir * currentSpeed;
        }
        
        // Ap dung gravity
        verticalVelocity += gravity * Time.deltaTime;

        Vector3 fallingMove = new Vector3(horizontalMove.x, verticalVelocity, horizontalMove.z);
        characterController.Move(fallingMove * Time.deltaTime);

        if (!IsTrueFalling())
        {
            isEnteringState = true;
            animationComponent.Falling(false);
            isFallingFromJump = false;
            moveState = MoveState.Landing;
            currentDir = Vector3.zero;
            Debug.Log("Falling -> Landing");
            return;
        }
    }
    private void HandleLanding()
    {
        if (isEnteringState)
        {
            animationComponent.Landing(true, currentSpeed);
            isEnteringState = false;
        }
        currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, groundSpeedReductionFactor * MaxSpeed * Time.deltaTime);

        if (animationComponent.IsLandingEnd)
        {
            animationComponent.Landing(false, currentSpeed);
            isEnteringState = true;
            if (GetDirectionFromDevices(out currentDir, out float speedIntensity))
            {
                currentSpeed = speedIntensity * MaxSpeed;
                moveState = MoveState.Moving;
                Debug.Log("Landing -> Moving");
                return;
            }
            moveState = MoveState.Idle;
            currentDir = Vector3.zero;
            currentSpeed = 0f;
            Debug.Log("Landing -> Idle");
        }

    }

    private void HandleJumping()
    {
        if (isEnteringState)
        {
            verticalVelocity = verticalJumpForce;
            animationComponent.Jumping(true);
            isEnteringState = false;
            Debug.Log("Enter Jumping " + " speed " + currentSpeed + " direction " + currentDir);
        }

        // Di chuyen theo huong nhay truoc do
        Vector3 jumpingMove = currentDir * currentSpeed * horizontalJumpForceFactor;
        jumpingMove.y = verticalVelocity;
        characterController.Move(jumpingMove * Time.deltaTime);

        // Ap dung trong luc cho lan sau
        verticalVelocity += gravity * Time.deltaTime;

        // Khi van toc am, thi bat dau roi
        if (verticalVelocity <= 0)
        {
            moveState = MoveState.Falling;
            isEnteringState = true;
            animationComponent.Jumping(false);

            isFallingFromJump = true;
            lastDir = currentDir;
            Debug.Log("Jumping -> Falling " + " speed " + currentSpeed + " direction " + currentDir);
        }

    }

    public override void MoveToDirection(Vector3 normalizeDirection)
    {
        // Quay nhan vat theo huong di chuyen
        Quaternion targetRotation = Quaternion.LookRotation(normalizeDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        Vector3 move = normalizeDirection * currentSpeed;

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

    private bool IsGroundedByCast()
    {
        // Day la cach check bang RAY CAST
        RaycastHit hit;
        float castDistance = 0.4f; // Dieu chinh theo chieu cao va skin width cua CharacterController
        if (Physics.Raycast(transform.position, Vector3.down, out hit, castDistance) )
        {
            return true;
            // Doan duoi day neu bat la kiem tra Slop. Tam thoi khong kiem tra slope
            //float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            //if (slopeAngle < characterController.slopeLimit) // thuong la 45 do
            //    return true;
            //else
            //    return false;
        }
        return false;

        // Day la cach check bang SPHERE CAST
        //RaycastHit hit;
        //float radius = characterController.radius * 0.85f; // Nho hon radius mot chut de tranh cham vao vat the
        //float castDistance = 0.3f;
        //Vector3 origin = transform.position; // Co the nang mot chut de tranh cham vao vat the Bang viec + Vector3.up * 0.1f
        //if ( Physics.SphereCast(origin, radius, Vector3.down, out hit, castDistance) )
        //{
        //    return true;
        //}
        //return false;

    }

    private bool IsTrueFalling()
    {
        if (moveState == MoveState.Falling)
        {
            if (characterController.isGrounded) // Lan dau tien cham dat
            {
                return false;
            }
            return true;

        }
        // Khi khong cham dat thi verticalVelocity se am,
        // Roi mot luc roi thì verticalVelocity se nho hon verticalVelocityMax luc do se tinh roi thuc su
        // Neu chi hoi khong cham dat do dia hinh khong phang thi khong chinh xac

        if (verticalVelocity < verticalVelocityMax)
        {
            if ( IsGroundedByCast() ) // Xu ly khi di xuong doc nhieu
                return false;
            return true;
        }
        return false;
    }

    #region CODE_OK_DO_NOT_MODIFY
    private bool GetDirectionFromDevices(out Vector3 direction, out float speedIntensity)
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) +
                              new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        if (input.sqrMagnitude < inputVectorSqrMin)
        {
            direction = Vector3.zero;
            speedIntensity = 0f;
            return false;
        }    
        speedIntensity = Mathf.Clamp01(input.magnitude);
        direction = ConvertInputToDirectionByCamera(input);
        return true;
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

        ////Lay theo toa do dia phuong cua nhan vat. Nay khong dung nua
        //Vector3 moveDir = input.sqrMagnitude > 0.01f ? input.normalized : Vector3.zero;
        //return moveDir;
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
    #endregion

    // Ham nay de goi tu UI Button - dang goi tam thoi
    public void OnJumpButtonPressed()
    {
        if ((moveState == MoveState.Idle) || (moveState == MoveState.Moving))
        { jumpRequested = true; }

    }
    public override bool HasMovementInput()
    {
        return GetDirectionFromDevices(out _, out _);
    }

    public override bool CanOutSate()
    {
        return false;
    }

    public override void MoveTo(Vector3 target)
    {
        // Khong su dung ham nay trong PlayerMoveComponent
        Debug.LogWarning("MoveTo is not implemented in PlayerMoveComponent. Use MoveToDirection instead.");
    }

    public override void Stop()
    {
        // Khong su dung ham nay trong PlayerMoveComponent
        Debug.LogWarning("Stop is not implemented in PlayerMoveComponent. Use input devices to stop movement.");
    }

}