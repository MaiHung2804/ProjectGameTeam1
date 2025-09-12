using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Quan ly di chuyen cua don vi.
/// </summary>
public class MoveComponent : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float moveSpeed = 5f;  // Bien dang input
    [SerializeField] private float stopDistance = 0.1f;

    protected Vector3? targetPosition; // Them ? de cho phep null
    protected bool isMoving;    // Bien noi bo nen de protected

    public bool IsMoving => isMoving;

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

    /// <summary>
    /// Dat vi tri dich va bat dau di chuyen den do.
    /// </summary>
    /// <param name="target"></param>
    public virtual void MoveTo(Vector3 target)
    {
        targetPosition = target;
        isMoving = true;
    }


    public virtual void Stop()
    {
        isMoving = false;
        targetPosition = null;
    }

    protected virtual void Awake()
    {
        isMoving = false;
        targetPosition = null;
    }

    

    //public virtual void HandleMoving()
    //{
      
    //}

    //public virtual void MoveTo(Vector3 target)
    //{ 
    //}

}