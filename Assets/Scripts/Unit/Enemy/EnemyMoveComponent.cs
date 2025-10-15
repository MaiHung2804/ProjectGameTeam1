using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveComponent : MoveComponent
{
    private EnemyData enemyData;
    private NavMeshAgent agent;
    private Transform target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }
    }

    public override void InitComponent()
    {
        base.InitComponent();
        enemyData = (EnemyData)unitData;
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        agent.speed = MaxSpeed;
        agent.stoppingDistance = enemyData.StopDistance;
        agent.updateRotation = true;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public override void MoveTo(Vector3 targetPos)
    {
        if (agent == null) return;
        agent.isStopped = false;
        agent.SetDestination(targetPos);
    }

    public override void MoveToDirection(Vector3 direction)
    {
        // Dung NavMesh nen khong can
    }

    public override void Stop()
    {
        if (agent != null)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }
        CurrentSpeed = 0f;
        CurrentDir = Vector3.zero;
    }

    public override bool CanOutComponentState()
    {
        return true;
    }

}
