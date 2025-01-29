using UnityEngine;
using Pathfinding;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public Transform Enemy;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rigidbody;

    [SerializeField] private Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidbody = GetComponent<Rigidbody2D>();
        Enemy = GetComponent<Transform>();

        // 3rd param is how often the path is updated
        InvokeRepeating("UpdatePath", 0f, 1f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rigidbody.position, target.position, OnPathComplete);
        }
    }


    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void CheckAttack()
    {
        // checks if player is in range
        if (Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer) != null)
        {
     
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }

        Vector2 dir = ((Vector2) path.vectorPath[currentWaypoint] - rigidbody.position).normalized;
        Vector2 force = dir * speed * Time.deltaTime;

        rigidbody.AddForce(force);

        float distance = Vector2.Distance(rigidbody.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // flips enemy
        if (rigidbody.linearVelocity.x >= 0.01f)
        {
            Enemy.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rigidbody.linearVelocity.x <= -0.01f)
        {
            Enemy.localScale = new Vector3(-1f, 1f, 1f);
        }

        CheckAttack();
    }

}
