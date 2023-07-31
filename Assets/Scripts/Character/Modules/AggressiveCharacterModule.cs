using System.Collections;
using UnityEngine;

public abstract class AggressiveCharacterModule : CharacterModule
{
    [Header("Detection")]
    [SerializeField] private float detectionRange;
    [SerializeField] private LayerMask targetLayer;
    private Transform detectedTarget;

    [Header("Attack")]
    [SerializeField] private float attackRange;
    [SerializeField] private float attackDelay;
    private float currentTime;

    private void Update()
    {
        //Detecting target
        if (detectedTarget == null || !detectedTarget.gameObject.activeSelf)
            DetectTarget();
        else
            MoveToTarget();

        //Attacking target
        if (currentTime <= 0)
            AttackTargetInRange();
        else
            currentTime -= Time.deltaTime;
    }

    private void DetectTarget()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, detectionRange, targetLayer);

        if (cols == null || cols.Length == 0)
            return;

        float minDistance = float.MaxValue;
        for (int i = 0; i < cols.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, cols[i].transform.position);
            if (distance <= minDistance)
            {
                minDistance = distance;
                detectedTarget = cols[i].transform;
            }
        }
    }

    protected virtual void MoveToTarget()
    {
        character.Move(detectedTarget.position);
    }

    //Attacking 
    private void AttackTargetInRange()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, attackRange, targetLayer);

        if (col == null)
            return;

        currentTime = attackDelay;
        AttackTarget(col.GetComponent<Damageable>());
    }

    protected abstract void AttackTarget(Damageable damageable);

    //Debug
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public override void OnMoveDone()
    {
    }
}