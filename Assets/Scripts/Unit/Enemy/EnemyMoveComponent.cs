using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveComponent : MoveComponent
{
    private Vector3? target, lastTarget;
    private bool isEnteringState = true;

    private NavMeshAgent agent;
    private float sqrUpdateThreshold = 0.5f * 0.5f;
    private float sqrDetectionRange;
    private float sqrAttackRange;
    private Vector3 patrolA, patrolB, currentPatrol;
    private const float PATROL_SPEED_FACTOR = 0.3f;
    private const float CHASE_SPEED_FACTOR = 0.7f;
    private const float ANIMATION_SPEED_FACTOR = 6f;

    private bool thinking = false;
    private const float thinkDuration = 4f;
    private float thinkTimer = 0f;


    public override void InitComponent()
    {
        base.InitComponent();
        target = null;
        lastTarget = null;
        sqrDetectionRange = ((EnemyData)unitData).DetectionRange * ((EnemyData)unitData).DetectionRange;
        sqrAttackRange = ((EnemyData)unitData).AttackRange * ((EnemyData)unitData).AttackRange;
        patrolA = ((EnemyData)unitData).PatrolPointA;
        patrolB = ((EnemyData)unitData).PatrolPointB;
        currentPatrol = patrolA;
        InitNavMeshAgent();

    }


    public override void HandleComponentActs(Vector3? targetPosition)
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
            animationComponent.MoveSpeed(0);
            //Debug.Log(" current state entering " + moveState);
        }
        //Debug.Log(" current state " + moveState);
        
        if (target == null) return;

        if (IsTargetInDetectionRange((Vector3)target))
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
            currentSpeed = MaxSpeed * CHASE_SPEED_FACTOR;
            animationComponent.MoveSpeed(currentSpeed * ANIMATION_SPEED_FACTOR);
            agent.speed = MaxSpeed/3;
            //Debug.Log(" current state entering " + moveState);
            UIHealthBarManager.Instance.CreateHealthBarEnemy(((EnemyData)unitData).RunTimeId);
        }

        if (target == null)
        {
            moveState = MoveState.Idle;
            isEnteringState = true;
            return;
        }
        //Debug.Log(" current state " + moveState);
        if (IsTargetChanged(lastTarget,target))
        {
            lastTarget = target;
            MoveTo(target.Value);
        }
    }

    private void HandlePatrol()
    {
        if (isEnteringState)
        {
            isEnteringState = false;
            currentSpeed = MaxSpeed * PATROL_SPEED_FACTOR;
            animationComponent.MoveSpeed(currentSpeed * ANIMATION_SPEED_FACTOR);
            agent.speed = currentSpeed;
            MoveTo(currentPatrol);
            //Debug.Log(" current state entering " + moveState);
        }
        //Debug.Log(" current state " + moveState);
        if (target == null)
        {
            moveState = MoveState.Idle;
            isEnteringState = true;
            return;
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!thinking)
            {
                thinking = true;
                thinkTimer = 0f;
            }
            else
            {
                if (currentSpeed > 0f)
                {
                    currentSpeed -= Time.deltaTime * (MaxSpeed * PATROL_SPEED_FACTOR) / thinkDuration;
                    if (currentSpeed < 0f) currentSpeed = 0f;
                    animationComponent.MoveSpeed(currentSpeed * ANIMATION_SPEED_FACTOR);
                    agent.speed = currentSpeed;
                }
                else
                {
                    currentSpeed = 0f;
                    animationComponent.MoveSpeed(0);
                    agent.speed = 0f;
                }

                thinkTimer += Time.deltaTime;
                if (thinkTimer >= thinkDuration)
                {
                    currentPatrol = SelectPatrol(currentPatrol);
                    isEnteringState = true;
                    thinking = false;
                }
            }
        }

        if (target != null && IsTargetInDetectionRange((Vector3)target))
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

    private bool IsTargetChanged(Vector3? oldTarget, Vector3? newTarget)
    {
        if (newTarget == null) return false;
        if (oldTarget == null) return true;
        return (newTarget.Value - oldTarget.Value).sqrMagnitude > sqrUpdateThreshold;
    }

    private bool IsTargetInDetectionRange(Vector3 targetPos)
    {
        if (targetPos == null) return false;
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
