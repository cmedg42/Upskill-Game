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

    [SerializeField] private Transform player;
    private float enemyWakeupRange = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidbody = GetComponent<Rigidbody2D>();
        Enemy = GetComponent<Transform>();

        // 3rd param is how often the path is updated
        InvokeRepeating("UpdatePath", 0f, 0.2f);
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

    // figure out player on enemy collisions

    

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector2.Distance(player.position, transform.position);

        if (distance > enemyWakeupRange)
        {
            return;
        }

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

        float distanceToPlayer = Vector2.Distance(rigidbody.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // flips enemy
        if (rigidbody.linearVelocity.x >= 0.1f)
        {
            Enemy.localScale = new Vector3(3.7613f, 3.7613f, 3.7613f);
        }
        else if (rigidbody.linearVelocity.x <= -0.1f)
        {
            Enemy.localScale = new Vector3(-3.7613f, 3.7613f, 3.7613f);
        }
    }
}
