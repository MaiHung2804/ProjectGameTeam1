using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Quan ly di chuyen cua don vi.
/// </summary>
public abstract class MoveComponent : MonoBehaviour
{
    protected UnitData unitData;
    public enum MoveState
    {
        Idle,
        Moving,
        Falling,
        Jumping,
        Landing,
        Chasing,
        Patrol
    }

    protected float maxSpeed; // = 5f; // Luu gia tri MaxSpeed
    protected float currentSpeed; // = 0f; // Luu gia tri hien tai cua toc do
    protected Vector3 currentDir;
    protected Vector3 lastDir;
    protected Vector3? targetPosition = null; // Them ? de cho phep null
    protected MoveState moveState = MoveState.Idle;

    public virtual void InitComponent() 
    {
        UnitBase unitBase = GetComponent<UnitBase>();
        if (unitBase == null)
        {
            Debug.LogError("MoveComponent: UnitBase component is missing on " + gameObject.name);
            return;
        }
        unitData = unitBase.GetUnitData();
        maxSpeed = unitData.MaxSpeed;
        //Debug.Log("MaxSpeed: " + maxSpeed);
        currentSpeed = 0f;

    }

    public float MaxSpeed
    {
        get => maxSpeed;
        set => maxSpeed = Mathf.Max(0f, value);
    }

    public float CurrentSpeed
    {
        get => currentSpeed;
        set => currentSpeed = Mathf.Clamp(value, 0f, maxSpeed);
    }

    public Vector3 CurrentDir
    {
        get => currentDir;
        set
        {
            if (value != Vector3.zero)
            {
                currentDir = value.normalized;
                lastDir = currentDir;
            }
            else
            {
                currentDir = Vector3.zero;
            }
        }
    }

    public Vector3 LastDir => lastDir;

    //public float StopDistance
    //{
    //    get => stopDistance;
    //    set => stopDistance = Mathf.Max(0f, value);
    //}
  
    /// <summary>
    /// Dat vi tri dich va bat dau di chuyen den do.
    /// Dung abstract de bat buoc cac lop con phai override.
    /// Dung virtual thi lop con co the override hoac khong. Lop con co the goi base.MoveTo(target) de su dung logic mac dinh.
    /// </summary>
    /// <param name="target"></param>
    public abstract void MoveTo(Vector3 target);

    public abstract void MoveToDirection(Vector3 direction);

    public abstract void Stop();

    public virtual void HandleComponentActs() { }

    public virtual void HandleComponentActs(Vector2 moveInput, bool isJump) { }

    public virtual void HandleComponentActs(Vector3 targetPosition) {  }


    public abstract bool CanOutComponentState();
}