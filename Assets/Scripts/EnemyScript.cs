using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;
    public float speed = 0.5f;
    private Rigidbody2D rb;
    public SpriteRenderer sprite;

    private Transform currentTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentTarget = waypoint1;
        sprite =  GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MoveToWaypoint();
        Flip(rb.linearVelocityX);
    }

    void MoveToWaypoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = (currentTarget == waypoint1) ? waypoint2 : waypoint1;
        }
    }
    void Flip(float velocity){
        print(velocity);
        if (velocity > 1)
        {
            sprite.flipX = true;
            
        }
        else if (velocity < -1)
        {
            sprite.flipX = false;
        }
    }
}
