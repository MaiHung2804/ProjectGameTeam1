using UnityEngine;

public class PlayerMoveComponent : MoveComponent
{
    [Header("Player Move Settings")]
    

    private IPlayerMoveState currentState;
    [SerializeField] private Joystick joystick;


    private void Start()
    {
        ChangeState(new PlayerIdle());
    }

    private void Update()
    {
        currentState?.Update(this);
        //if (currentState == null)
        //{ return; }
        //currentState.Update(this);

    }

    public void ChangeState(IPlayerMoveState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
    }

    public bool HasMoveInput()
    {
        // Ví dụ: kiểm tra input Joystick hoặc phím
        return Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0
                || joystick.Horizontal != 0 || joystick.Vertical != 0; ;
    }

    // Lấy hướng di chuyển từ input
    public Vector3 GetInputDirection()
    {
        //return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        return new Vector3(joystick.Horizontal, 0, joystick.Vertical);
    }


    public override void MoveDirection(Vector3 direction)
    {
        // Di chuyen bang input nguoi choi
        transform.position += direction.normalized * MoveSpeed * Time.deltaTime;
    }



}