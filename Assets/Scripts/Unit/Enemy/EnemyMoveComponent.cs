using UnityEngine;

public class EnemyMoveComponent : MoveComponent
{
    public override bool CanOutComponentState()  //todo
    {
        return false;
    }

    public override void MoveTo(Vector3 target)
    {
        targetPosition = target;
        moveState = MoveState.Moving;

        // Di chuyển đơn giản bằng Lerp
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * MaxSpeed * Time.deltaTime;

        // Nếu gần đến mục tiêu thì dừng
        if (Vector3.Distance(transform.position, target) <= StopDistance)
        {
            Stop();
        }
    }

    public override void MoveToDirection(Vector3 direction)
    {
        moveState = MoveState.Moving;
        transform.position += direction.normalized * MaxSpeed * Time.deltaTime;
    }

    public override void Stop()
    {
        targetPosition = null;
        moveState = MoveState.Idle;
    }

    //public override void HandleActivities() //todo
    //{
    //    if (targetPosition.HasValue)
    //    {
    //        MoveTo(targetPosition.Value);
    //    }
    //}

    //public override bool HasMovementInput() //todo
    //{
    //    // Enemy không cần input từ người chơi
    //    return false;
    //}

    //public override bool CanOutSate() //todo
    //{
    //    return true;
    //}
}
