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

//using UnityEngine;

//public class EnemyMoveComponent : MoveComponent
//{
//    public override bool CanOutComponentState()  //todo
//    {
//        return false;
//    }

//    public override void MoveTo(Vector3 target)
//    {
//        targetPosition = target;
//        moveState = MoveState.Moving;

//        // Di chuyển đơn giản bằng Lerp
//        Vector3 direction = (target - transform.position).normalized;
//        transform.position += direction * MaxSpeed * Time.deltaTime;

//        // Nếu gần đến mục tiêu thì dừng
//        if (Vector3.Distance(transform.position, target) <= StopDistance)
//        {
//            Stop();
//        }
//    }

//    public override void MoveToDirection(Vector3 direction)
//    {
//        moveState = MoveState.Moving;
//        transform.position += direction.normalized * MaxSpeed * Time.deltaTime;
//    }

//    public override void Stop()
//    {
//        targetPosition = null;
//        moveState = MoveState.Idle;
//    }

//    //public override void HandleActivities() //todo
//    //{
//    //    if (targetPosition.HasValue)
//    //    {
//    //        MoveTo(targetPosition.Value);
//    //    }
//    //}

//    //public override bool HasMovementInput() //todo
//    //{
//    //    // Enemy không cần input từ người chơi
//    //    return false;
//    //}

//    //public override bool CanOutSate() //todo
//    //{
//    //    return true;
//    //}
//}
