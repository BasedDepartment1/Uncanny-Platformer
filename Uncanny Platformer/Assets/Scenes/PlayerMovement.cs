using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MaxSpeed = 10f;
    private bool isOrientationRight = true;
    
    private void Start()
    {
        
    }
    
    private void Update()
    {
        var moveDistance = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveDistance * MaxSpeed,
            GetComponent<Rigidbody2D>().velocity.y);
        if (moveDistance > 0 && !isOrientationRight
            || moveDistance < 0 && isOrientationRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        isOrientationRight = !isOrientationRight;
    }
}

