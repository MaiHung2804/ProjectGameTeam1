using UnityEngine;

/// <summary>
/// Quan ly di chuyen cua don vi.
/// </summary>
public class MoveComponent : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float moveSpeed = 5f;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = Mathf.Max(0f, value);
    }

    public virtual void MoveTo(Vector3 targetPosition)
    {
    }

    public virtual void MoveDirection(Vector3 direction)
    {
    }
}