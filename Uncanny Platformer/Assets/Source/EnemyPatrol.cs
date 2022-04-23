using UnityEngine;


public enum Directions
{
    Left = -1,
    Right = 1
}

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Objects")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform enemy;
    
    [Header("Patrol Timings")]
    [SerializeField] private float standDuration;
    [SerializeField] private float maxSpeed;
    
    
    private bool movingRight;
    private float standTimer;
    

    private Vector3 initialScale;
    private Rigidbody2D enemyBody;

    void Awake()
    {
        initialScale = enemy.localScale;
        enemyBody = enemy.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (movingRight)
        {
            if (enemy.position.x < rightEdge.position.x)
            {
                MoveToDirection(Directions.Right);
            }
            else
            {
                TurnAround();
            }
        }
        else
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveToDirection(Directions.Left);
            }
            else
            {
                TurnAround();
            }
        }
    }

    private void MoveToDirection(Directions direction)
    {
        standTimer = 0;
        enemy.localScale = new Vector3(-Mathf.Abs(initialScale.x) * (int)direction, 
            initialScale.y, initialScale.z);
        enemyBody.velocity = new Vector2((int)direction * maxSpeed, enemyBody.velocity.y);
    }

    private void TurnAround()
    {
        standTimer += Time.deltaTime;
        if (standTimer > standDuration)
        {
            movingRight = !movingRight;
        }
    }
}
