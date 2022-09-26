using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemyMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 500f;
    public float nextWaypointDistance = 3f;
    public Transform enemyGFX;
    public Transform playerPos;
    public float startY;

    Path path;
    int currentWaypoint = 0;
    //bool reachedEndOfPath = false;
    bool start;

    Seeker seeker;
    Rigidbody2D rb;

    private bool inBackground = false;
    public Animator enemy_animator;
    private SpriteRenderer e_SpriteRenderer;
    private CircleCollider2D e_CircleCollider;
    public bool startBG;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        start = false;
        e_SpriteRenderer = GetComponent<SpriteRenderer>();
        e_CircleCollider = GetComponent<CircleCollider2D>();

        enemy_animator.SetBool("Background", inBackground);
        if (startBG == true)
        {
            FlipFlyingEnemyBackground();
        }

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
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


    void Update()
    {
        if (Input.GetButtonDown("Flip Background"))
        {
            FlipFlyingEnemyBackground();
        }
    }


    void FixedUpdate()
    {
        if (playerPos.position.y > startY)
        {
            start = true;
        }

        

        if (start == true && inBackground == false)
        {
            AstarPath.active.Scan();
            if (path == null)
            {
                return;
            }

            if (currentWaypoint >= path.vectorPath.Count)
            {
                //reachedEndOfPath = true;
                return;
            }
            else
            {
                //reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;
            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            if (force.x >= 0.01f)
            {
                enemyGFX.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (force.x <= -0.01f)
            {
                enemyGFX.localScale = new Vector3 (-1f, 1f, 1f);
            }
        }
        
    }


    private void FlipFlyingEnemyBackground()
    {
        if (inBackground == false)
        {
            e_SpriteRenderer.sortingLayerName = "Background";
            e_CircleCollider.enabled = false;
            enemy_animator.SetBool("Background", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            inBackground = true;
        }
        else
        {
            e_SpriteRenderer.sortingLayerName = "Foreground";
            e_CircleCollider.enabled = true;
            enemy_animator.SetBool("Background", false);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            inBackground = false;
        }
    }
}
