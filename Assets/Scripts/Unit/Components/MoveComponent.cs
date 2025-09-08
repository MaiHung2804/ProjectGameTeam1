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




    public virtual void MoveDirection(Vector3 direction)
    {
        if (direction.sqrMagnitude < 0.01f)
        {
            isMoving = false;
            return;
        }
        transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        isMoving = true;
        targetPosition = null; // Huy vi tri dich khi di chuyen theo huong
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

    protected virtual void Update()
    {
        HandleMoving();

    }

    private void HandleMoving()
    {
        if (targetPosition.HasValue && isMoving)
        {
            Vector3 direction = targetPosition.Value - transform.position;
            float distance = direction.magnitude;
            if (distance <= stopDistance)
            {
                Stop();
            }
            else
            {
                MoveDirection(direction);
            }
        }
    }    

}