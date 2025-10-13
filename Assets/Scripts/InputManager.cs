using System;
using UnityEngine;

using static MoveComponent;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    public static InputManager Instance { get; private set; }
    private float inputVectorSqrMin = 0.05f;

    private KeyCode keyJump = KeyCode.Space;
    private KeyCode keyMeleeAttack = KeyCode.J; // skill 1
    private KeyCode keyRangedAttack = KeyCode.K; // skill 2
    private KeyCode keyMagicAttack = KeyCode.L; // skill 3

    public event Action EventOnJump;
    public event Action EventOnMeleeAttack;
    public event Action EventOnRangedAttack;
    public event Action EventOnMagicAttack;

    private bool isJumpOnUI = false;
    private bool isMeleeAttackOnUI = false;
    private bool isRangedAttackOnUI = false;
    private bool isMagicAttackOnUI = false;
   

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject); // Giu doi tuong nay khong bi huy khi load scene moi
    }

    public void Init()
    {
        if (joystick == null)
        {
            Debug.LogWarning("Joystick not assigned in InputManager. Add Joystick cavas");
        }
    }

    // Tra ve vector di chuyen tu ban phim va joystick
    public Vector2 GetMoveInput()
    {
        float horizontal = Input.GetAxis("Horizontal") + joystick.Horizontal;
        float vertical = Input.GetAxis("Vertical") + joystick.Vertical;
        Vector2 input = new Vector2(horizontal, vertical);
        if (input.sqrMagnitude < inputVectorSqrMin)
        {
            return Vector2.zero;
        }
        return input;
    }

    // Tra ve vector di chuyen tu Chuot
    public Vector2 GetRotateFromMouseInput()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        return new Vector2(mouseX, mouseY);
    }

    public float GetZoomFromMouseInput()
    {
        return Input.GetAxis("Mouse ScrollWheel");
    }

    public bool GetControlInput()
    {
        return Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
    }

    public bool GetJumpInput()
    {
        return Input.GetKey(keyJump) || isJumpOnUI;
    }
    public Skill GetAttackInput()
    {
        if ( GetMeleeAttackInput())
        {
            return Skill.MeleeAttack;
        }
        if ( GetRangedAttackInput())
        {
            return Skill.RangedAttack;
        }

        if (GetMagicAttackInput())
        {
            return Skill.MagicAttack;
        }

        return Skill.None;
    }

    public bool GetMeleeAttackInput()
    {
        return Input.GetKey(keyMeleeAttack) || isMeleeAttackOnUI;

        // Neu khong dung MeleeAttackUIUp thi co the dung cach nhu sau
        // Chi goi 1 lan trong 1 Frame khi bam nut UI
        //bool result = Input.GetKey(keyMeleeAttack) || isMeleeAttackOnUI;
        //isMeleeAttackOnUI = false; // chi kich hoat 1 lan: RAT QUAN TRONG, GOI TRONG 1 FRAME
        //return result;
    }
    public bool GetRangedAttackInput()
    {
        return Input.GetKey(keyRangedAttack) || isRangedAttackOnUI;
    }
    public bool GetMagicAttackInput()
    {
        return Input.GetKey(keyMagicAttack) || isMagicAttackOnUI;
    }

    #region KHU VUC CHO CAC NUT UI
    // Can gan OnPointerDown va OnPointerUp de bat su kien UI trong Unity o cac nut
    public void JumpUIDown()
    {
        isJumpOnUI = true;
        EventOnJump?.Invoke();
    }

    public void JumpUIUp()
    {
        isJumpOnUI = false;
    }

    public void MeleeAttackUIDown()
    {
        isMeleeAttackOnUI = true;
        EventOnMeleeAttack?.Invoke();
        Debug.Log("MeleeAttackUIDown");
    }
    public void MeleeAttackUIUp()
    {
        Debug.Log("MeleeAttackUIUp");
        isMeleeAttackOnUI = false;
    }

    public void RangedAttackUIDown()
    {
        isRangedAttackOnUI = true;
        EventOnRangedAttack?.Invoke();
    }
    public void RangedAttackUIUp()
    {
        isRangedAttackOnUI = false;
    }

    public void MagicAttackUIDown()
    {
        isMagicAttackOnUI = true;
        EventOnMagicAttack?.Invoke();
    }
    public void MagicAttackUIUp()
    {
        isMagicAttackOnUI = false;
    }
    #endregion
}
