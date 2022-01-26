using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float sightRange;
    bool chase = false;
    public Rigidbody2D rig;
    public int dmg = 15;
    public float speed = .5f;
    public float pathFindRate = 0.5f;
    public float accuracy = 0.1f;
    bool bouncing = false;
    Vector2 bounceDir;
    public float bounceForce = 10f;
    public float bounceTime = 0.5f;
    public Vector2 target;

    public Pathfinding pathfinder;
    List<Node> path;
    int pathInd = 0;

    void Start()
    {
        InvokeRepeating("FindPath", 0f, pathFindRate);
        rig = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 playPos = new Vector2(player.transform.position.x, player.transform.position.y);
        if (Vector2.Distance(pos, playPos) < sightRange)
        {
            if (!chase)
            {
                RaycastHit2D hit = Physics2D.Raycast(pos, playPos - pos, sightRange);
                if (hit && hit.transform.gameObject == player)
                {
                    chase = true;
                }
            }
        }
        else
        {
            chase = false;
        }
    }

    private void FixedUpdate()
    {
        if (path != null && chase)
        {
            if (aboutEquals(transform.position, target, accuracy))
            {
                try
                {
                    pathInd++;
                    target = path[pathInd].pos;
                }
                catch (System.ArgumentOutOfRangeException ex)
                {
                    pathInd--;
                    target = path[pathInd].pos;
                }
            }
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            Vector2 movement;
            if (!bouncing)
            {
                movement = (target - pos).normalized * speed * Time.fixedDeltaTime;
                if (movement.x <= -0.01f)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else if (movement.x >= 0.01f)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
            }
            else
            {
                movement = bounceDir.normalized * bounceForce * Time.fixedDeltaTime;
            }
            rig.MovePosition(pos + movement);

        }
    }

    void FindPath()
    {
        if (chase)
        {
            path = pathfinder.FindPath(transform.position, player.transform.position);
            target = path[0].pos;
            pathInd = 0;
        }

    }

    bool aboutEquals(Vector2 a, Vector2 b, float accuracy)
    {
        if (Mathf.Abs(a.magnitude - b.magnitude) < accuracy)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            if (player.GetComponent<Movement>().dashing)
            {
                Destroy(this.gameObject);
            }
            else
            {
                player.GetComponent<PlayerHealth>().TakeDamage(dmg);
                Vector2 pos = new Vector2(transform.position.x, transform.position.y);
                bounceDir = (pos - target);
                bouncing = true;
                Invoke("StopBounce", bounceTime);

            }
        }
        else
        {
            CannonBall cb = collision.GetComponent<CannonBall>();
            if (cb)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void StopBounce()
    {
        bouncing = false;
    }
}
