using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveComponent : MoveComponent
{
    private Vector3? target, lastTarget;
    private AnimationComponent animationComponent;
    private bool isEnteringState = true;

    private NavMeshAgent agent;
    private float sqrUpdateThreshold = 0.5f * 0.5f;
    private float sqrDetectionRange;
    private float sqrAttackRange;
    private Vector3 patrolA, patrolB, currentPatrol;
    private bool thinking = false;
    private float thinkDuration = 2f;
    private float thinkTimer = 0f;


    public override void InitComponent()
    {
        base.InitComponent();
        UnitBase unit = GetComponent<UnitBase>();
        animationComponent = unit.GetAnimationComponent();
        target = null;
        sqrDetectionRange = ((EnemyData)unitData).DetectionRange * ((EnemyData)unitData).DetectionRange;
        sqrAttackRange = ((EnemyData)unitData).AttackRange * ((EnemyData)unitData).AttackRange;
        patrolA = ((EnemyData)unitData).PatrolPointA;
        patrolB = ((EnemyData)unitData).PatrolPointB;
        currentPatrol = patrolA;
        InitNavMeshAgent();
    }


    public override void HandleComponentActs(Vector3 targetPosition)
    {
        target = targetPosition;
        switch (moveState)
        {
            case MoveState.Chasing:
                HandleChasing();
                break;
            case MoveState.Patrol:
                HandlePatrol();
                break;
            default: // MoveState.Idle
                HandleIdle();
                break;

        }

    }

    private void HandleIdle()
    {
        if (isEnteringState)
        {
            isEnteringState = false;
            Stop();
            animationComponent.MoveSpeed(0);
        }
        if (thinking)
        {
            thinkTimer += Time.deltaTime;
            if (thinkTimer >= thinkDuration)
            {
                thinking = false;
                thinkTimer = 0f;
            }
            return;
        }

        if (target == null) return;
        if (IsTargetInRange((Vector3)target))
        {
            moveState = MoveState.Chasing;
            isEnteringState = true;
            return;
        }
        else
        {
            moveState = MoveState.Patrol;
            isEnteringState = true;
            return;
        }
    }

    private void HandleChasing()
    {
        if (isEnteringState)
        {
            isEnteringState = false;
            animationComponent.MoveSpeed(MaxSpeed);
            agent.speed = MaxSpeed;
        }
        MoveTo((Vector3)target);
    }

    private void HandlePatrol()
    {
        if (isEnteringState)
        {
            isEnteringState = false;
            animationComponent.MoveSpeed(MaxSpeed / 2);
            agent.speed = MaxSpeed / 2;
            MoveTo(currentPatrol);
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            currentPatrol = SelectPatrol(currentPatrol);
            thinking = true;
            moveState = MoveState.Idle;
            isEnteringState = true;
            return;
        }

        if (target != null && IsTargetInRange((Vector3)target))
        {
            moveState = MoveState.Chasing;
            isEnteringState = true;
            thinking = false;
            thinkTimer = 0f;
            return;
        }
    }

    private Vector3 SelectPatrol(Vector3 currentPatrol)
    {
        if (currentPatrol == patrolA) return patrolB;
        else return patrolA;
    }    

    private bool IsTargetChanged(Vector3 oldTarget, Vector3 newTarget)
    {
        return (oldTarget - newTarget).sqrMagnitude > sqrUpdateThreshold;
    }

    private bool IsTargetInRange(Vector3 targetPos)
    {
        float sqrDistance = (transform.position - targetPos).sqrMagnitude;
        return sqrDistance < sqrDetectionRange;
    }

    private void InitNavMeshAgent()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing on " + gameObject.name);
            return;
        }
        agent.stoppingDistance = ((EnemyData)unitData).StopDistance;
        agent.updateRotation = true;
    }

    public override void MoveTo(Vector3 targetPos)
    {
        agent.isStopped = false;
        agent.SetDestination(targetPos);
    }

    public override void MoveToDirection(Vector3 direction)
    {
        // Dung NavMesh nen khong can
    }

    public override void Stop()
    {
        target = null;
        agent.isStopped = true;
        agent.ResetPath();
    }

    public override bool CanOutComponentState()
    {
        return true;
    }

}
