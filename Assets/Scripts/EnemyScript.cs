using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;
    public float speed = 0.5f;
    private Rigidbody2D rb;
    public SpriteRenderer sprite;

    private Transform currentTarget;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentTarget = waypoint1;
        sprite =  GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        MoveToWaypoint();
        Flip();
    }

    void MoveToWaypoint()
    {
        animator.SetFloat("Speed", speed);
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = (currentTarget == waypoint1) ? waypoint2 : waypoint1;
        }
    }
    void Flip(){
       
        if (currentTarget == waypoint1)
        {
            sprite.flipX = true;
            
        }
        else if (currentTarget == waypoint2)
        {
            sprite.flipX = false;
        }
    }
}
