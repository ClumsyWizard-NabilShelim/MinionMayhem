using UnityEngine;

public class CowardCharacterModule : CharacterModule
{
    private bool move;
    private bool runningAway;
    private Vector2 moveToPosition;

    [Header("Runaway")]
    [SerializeField] private float visionArea;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Patrol")]
    [SerializeField] private float patrolIdleDelay;
    private float currentTime;

    private void Update()
    {
        if (move)
        {
            character.Move(moveToPosition);
            if (!runningAway)
                CheckForEnemy();
        }
        else
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                return;
            }

            move = true;
            moveToPosition = GameManager.Instance.GetRandomPoint();
        }
    }

    public override void OnMoveDone()
    {
        if (runningAway)
        {
            GameManager.Instance.RemoveHuman((Human)character);
            gameObject.SetActive(false);
        }
        else
        {
            move = false;
            currentTime = patrolIdleDelay;
        }
    }

    //Helper functions
    private void CheckForEnemy()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, visionArea, enemyLayer);

        if (col != null)
        {
            move = true;
            runningAway = true;
            character.IncreaseMoveSpeed(4.0f);
            moveToPosition = GameManager.Instance.GetEscapeToPoint();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionArea);
    }
}