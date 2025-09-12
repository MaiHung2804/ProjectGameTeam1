using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Quan ly di chuyen cua don vi.
/// </summary>
public abstract class MoveComponent : MonoBehaviour
{
    public enum MoveState
    {
        Idle,
        Moving,
        Falling,
        Jumping,
        Landing
    }


    [Header("Move Settings")]
    [SerializeField] private float moveSpeed = 5f;  // Bien dang input
    [SerializeField] private float stopDistance = 0.1f;

    protected Vector3? targetPosition; // Them ? de cho phep null
    protected MoveState moveState; 

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = Mathf.Max(0f, value);
    }
    public float StopDistance
    {
        get => stopDistance;
        set => stopDistance = Mathf.Max(0f, value);
    }

    

    protected virtual void Awake()
    {
        targetPosition = null;
        moveState = MoveState.Idle;
    }
    
    protected virtual void Update()
    {
        HandleMoving();
    }

    /// <summary>
    /// Dat vi tri dich va bat dau di chuyen den do.
    /// Dung abstract de bat buoc cac lop con phai override.
    /// Dung virtual thi lop con co the override hoac khong. Lop con co the goi base.MoveTo(target) de su dung logic mac dinh.
    /// </summary>
    /// <param name="target"></param>
    public abstract void MoveTo(Vector3 target);
    public abstract void MoveByDirection(Vector3 direction);
    public abstract void Stop();

    protected abstract void HandleMoving();

}