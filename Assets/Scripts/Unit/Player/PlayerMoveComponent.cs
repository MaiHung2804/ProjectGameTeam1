using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMoveComponent : MoveComponent
{
    [Header("Player Move Settings")]
    
    private CharacterController characterController;
    private AnimationComponent animationComponent;

    
    
    private const float INPUT_VECTOR_SQR_MIN = 0.05f; 
    private const float GROUND_SPEED_REDUCTION = 1.2f; // Cang lon thi CurrentSpeed giam ve 0 cang nhanh khi khong co input
    private const float AIR_SPEED_REDUCTION = 0.5f; // Cang lon thi CurrentSpeed giam ve 0 cang nhanh khi khong co input
    private const float HOR_JUMP_FORCE_FACTOR = 1.2f; // Dung khi nhay nhan voi speed ngang
    private const float VER_JUMP_FORCE = 5f;
    private const float GRAVITY = -9.81f;
    private const float VERTICAL_VELOCITY_MAX = -2f;

    private float verticalVelocity = 0f;
    private bool isFallingFromJump = false;
    private bool canOutComponentState = true;   // Tong the
    private bool isEnteringState = true; // Chi tiet trong moi state

    private Vector2 moveInput = Vector2.zero;
    private bool jumpInput = false;

    public override void InitComponent()
    {
        characterController = GetComponent<CharacterController>();
        UnitBase unit = GetComponent<UnitBase>();
        animationComponent = unit.GetAnimationComponent();

        // Dat nhan vat luc dau o tren cao
        moveState = MoveState.Falling;
        canOutComponentState = false;
    }

    public override void HandleComponentActs(Vector2 moveInput, bool isJump)
    {
        this.moveInput = moveInput;
        this.jumpInput = isJump;
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
            //Debug.Log("Enter Idle" + currentSpeed);
            canOutComponentState = true;
        }

        if (jumpInput)
        {
            moveState = MoveState.Jumping;
            isEnteringState = true;
            //Debug.Log("Idle -> Jumping");
            return;
        }

        if (GetDirectionFromDevices(moveInput, out currentDir, out float speedIntensity))
        {
            currentSpeed = speedIntensity * MaxSpeed;
            moveState = MoveState.Moving;
            isEnteringState = true;
            //Debug.Log("Idle -> Moving");
        }
        
        // Mac dinh Idle thi khong co luc tac dong thi khong Falling duoc. Tru khi co skill Enemy day nhan vat.
    }

    private void HandleMoving()
    {
        if (isEnteringState)
        {
            animationComponent.MoveSpeed(currentSpeed);
            isEnteringState = false;
            canOutComponentState = true;
        }

        if (!GetDirectionFromDevices(moveInput, out currentDir, out float speedIntensity))
        {
            // Luc nay khong co input, currentDirection luc nay van giu nguyen. CurrentSpeed giam dan ve 0
            Debug.Log(" No input moving ");
            currentDir = lastDir;
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, GROUND_SPEED_REDUCTION * MaxSpeed * Time.deltaTime);
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
            //Debug.Log("Moving -> Falling " + " speed " + currentSpeed + " direction " + currentDir);
            return;
        }

        if (jumpInput)  
        {
            moveState = MoveState.Jumping;
            isEnteringState = true;
            //Debug.Log("Moving -> Jumping " + " speed " + currentSpeed + " direction " + currentDir);
            return;
        }

        if (currentSpeed == 0 )
        {
            moveState = MoveState.Idle;
            isEnteringState = true;
            currentDir = Vector3.zero;
            //Debug.Log("Moving -> Idle");
        }

    }

    private void HanldeFalling()
    {
        if (isEnteringState)
        {
            animationComponent.Falling(true);
            isEnteringState = false;
            canOutComponentState = false;
        }
        Vector3 horizontalMove;

        if (isFallingFromJump)
        {
            horizontalMove = currentDir * (currentSpeed * HOR_JUMP_FORCE_FACTOR);
        }
        else 
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, AIR_SPEED_REDUCTION * MaxSpeed * Time.deltaTime);
            horizontalMove = currentDir * currentSpeed;
        }

        // Bo vi da co UpdateVerticalVelocity
        //// Ap dung gravity
        //verticalVelocity += gravity * Time.deltaTime;

        Vector3 fallingMove = new Vector3(horizontalMove.x, verticalVelocity, horizontalMove.z);
        characterController.Move(fallingMove * Time.deltaTime);

        if (!IsTrueFalling())
        {
            isEnteringState = true;
            animationComponent.Falling(false);
            isFallingFromJump = false;
            moveState = MoveState.Landing;
            currentDir = Vector3.zero;
            //Debug.Log("Falling -> Landing");
            return;
        }
    }
    private void HandleLanding()
    {
        if (isEnteringState)
        {
            animationComponent.Landing(true, currentSpeed);
            isEnteringState = false;
            canOutComponentState = false;
        }
        currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, GROUND_SPEED_REDUCTION * MaxSpeed * Time.deltaTime);

        if (animationComponent.IsLandingEnd)
        {
            animationComponent.Landing(false, currentSpeed);
            isEnteringState = true;
            if (GetDirectionFromDevices(moveInput, out currentDir, out float speedIntensity))
            {
                currentSpeed = speedIntensity * MaxSpeed;
                moveState = MoveState.Moving;
                //Debug.Log("Landing -> Moving");
                return;
            }
            moveState = MoveState.Idle;
            currentDir = Vector3.zero;
            currentSpeed = 0f;
            //Debug.Log("Landing -> Idle");
         
        }

    }

    private void HandleJumping()
    {
        if (isEnteringState)
        {
            verticalVelocity = VER_JUMP_FORCE;
            animationComponent.Jumping(true);
            isEnteringState = false;
            //Debug.Log("Enter Jumping " + " speed " + currentSpeed + " direction " + currentDir);
        }

        // Di chuyen theo huong nhay truoc do
        Vector3 jumpingMove = currentDir * (currentSpeed * HOR_JUMP_FORCE_FACTOR);
        jumpingMove.y = verticalVelocity;
        characterController.Move(jumpingMove * Time.deltaTime);

        //// Ap dung trong luc cho lan sau
        //verticalVelocity += gravity * Time.deltaTime;

        // Khi van toc am, thi bat dau roi
        if (verticalVelocity <= 0)
        {
            moveState = MoveState.Falling;
            isEnteringState = true;
            animationComponent.Jumping(false);

            isFallingFromJump = true;
            lastDir = currentDir;
            //Debug.Log("Jumping -> Falling " + " speed " + currentSpeed + " direction " + currentDir);
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

    public void UpdateVerticalVelocity()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = 0f;
        }
        else
        {
            verticalVelocity += GRAVITY * Time.deltaTime;
        }

    }

    private bool IsGroundedByCast()
    {
        // Day la cach check bang RAY CAST
        float castDistance = 0.4f; // Dieu chinh theo chieu cao va skin width cua CharacterController
        if (Physics.Raycast(transform.position, Vector3.down, castDistance) )
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

        if (verticalVelocity < VERTICAL_VELOCITY_MAX)
        {
            if ( IsGroundedByCast() ) // Xu ly khi di xuong doc nhieu
                return false;
            return true;
        }
        return false;
    }

    #region CODE_OK_DO_NOT_MODIFY
    private bool GetDirectionFromDevices(Vector2 input2D, out Vector3 direction, out float speedIntensity)
    {
        Vector3 input = new Vector3(input2D.x, 0, input2D.y);
        if (input.sqrMagnitude < INPUT_VECTOR_SQR_MIN)
        {
            direction = Vector3.zero;
            speedIntensity = 0f;
            return false;
        }    
        speedIntensity = Mathf.Clamp01(input.magnitude);
        direction = FollowingCamera.Instance.ConvertVectorAsCameraCordination(input);
        return true;
    }

   
    #endregion

    public override bool CanOutComponentState()
    {
        return canOutComponentState;
    }

    public override void MoveTo(Vector3 target)
    {
        // Khong su dung ham nay trong PlayerMoveComponent
        Debug.LogWarning("MoveTo is not implemented in PlayerMoveComponent. Use MoveToDirection instead.");
    }
    
    public override void Stop()
    {
        currentDir = Vector3.zero;
        currentSpeed = 0f;
    }

   

}