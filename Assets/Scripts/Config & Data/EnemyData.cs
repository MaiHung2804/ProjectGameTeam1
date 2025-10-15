using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : UnitData
{
    protected float detectionRange;
    protected float stopDistance;
    protected Vector3 patrolPointA;
    protected Vector3 patrolPointB;

    public EnemyData(UnitConfig config) : base(config)
    {
        this.detectionRange = config.BaseAttackRange * 5;
        this.stopDistance = config.BaseAttackRange * 0.9f;
    }

    public EnemyData(UnitConfig config, Vector3 pointA, Vector3 pointB) : base(config)
    {
        this.detectionRange = config.BaseAttackRange * 5;
        this.stopDistance = config.BaseAttackRange * 0.9f;
        this.patrolPointA = pointA;
        this.patrolPointB = pointB;
    }

    public float DetectionRange { get => detectionRange; set { detectionRange = value; } }
    public float StopDistance { get => stopDistance; set { stopDistance = value; } }
    public Vector3 PatrolPointA { get => patrolPointA; set { patrolPointA = value; } }
    public Vector3 PatrolPointB { get => patrolPointB; set { patrolPointB = value; } }

}
