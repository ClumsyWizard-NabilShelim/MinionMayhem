using System;
using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public CharacterSO Data { get; private set; }
    public CharacterStats Stats { get; private set; }
    private Rigidbody2D rb;
    private Transform gfx;
    private CharacterModule module;
    private Animator animator;

    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float safeDistance;

    [Header("Personal Space Repulsion")]
    [SerializeField] private float personalArea;
    [SerializeField] private float repulsionForce;
    [SerializeField] private LayerMask repulsiveLayer;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gfx = transform.GetChild(0);
        module = GetComponent<CharacterModule>();
        module.Initialize(this);
        animator = GetComponent<Animator>();
    }

    public virtual void Initialize(CharacterSO data)
    {
        Data = data;
        Stats = GetComponent<CharacterStats>();
        Stats.Initialize(this);
    }

    public void Move(Vector2 target)
    {
        animator.SetBool("Move", true);
        if (Vector2.Distance(transform.position, target) > safeDistance)
        {
            rb.AddForce((target - (Vector2)transform.position).normalized * walkSpeed * Time.deltaTime);
            FaceMoveDirection(target);
            Repulsion();
        }
        else
        {
            rb.velocity = Vector2.zero;
            module.OnMoveDone();
            animator.SetBool("Move", false);
            return;
        }
    }
    public void IncreaseMoveSpeed(float multiplier)
    {
        walkSpeed *= multiplier;
    }

    private void Repulsion()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, personalArea, repulsiveLayer);

        if (col == null || col.Length == 0)
            return;

        for (int i = 0; i < col.Length; i++)
        {
            if (col[i].gameObject != gameObject)
            {
                Vector2 repulsionDirection = (transform.position - col[i].transform.position).normalized + new Vector3(UnityEngine.Random.Range(-1.1f, 1.1f), UnityEngine.Random.Range(-1.1f, 1.1f), 0.0f);
                rb.AddForce(repulsionDirection.normalized * repulsionForce * Time.deltaTime);
            }
        }
    }

    public void FaceMoveDirection(Vector3 target)
    {
        if ((target.x - transform.position.x) < 0.0f)
            gfx.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        else
            gfx.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
    }

    //Debug
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, personalArea);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, safeDistance);
    }
}