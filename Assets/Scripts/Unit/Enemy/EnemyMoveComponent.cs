using UnityEngine;

public class EnemyMoveComponent : MoveComponent
{
    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public override void MoveTo(Vector3 targetPos)
    {
        CurrentDir = (targetPos - transform.position).normalized;
        CurrentSpeed = MaxSpeed;
        MoveToDirection(CurrentDir);
    }

    public override void MoveToDirection(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        transform.position += direction * CurrentSpeed * Time.deltaTime;

        // Xoay mặt về phía di chuyển
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }

    public override void Stop()
    {
        CurrentSpeed = 0f;
        CurrentDir = Vector3.zero;
    }

    public override bool CanOutComponentState()
    {
        // Có thể ra khỏi state nếu không còn target hoặc đã dừng lại
        return target == null || CurrentSpeed <= 0.01f;
    }
}
