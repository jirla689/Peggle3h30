using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class BasketScript : MonoBehaviour
{
    enum DirectionType
    {
        LEFT,
        RIGHT
    }
    Rigidbody2D rb;

    DirectionType direction;

    [SerializeField]
    float BasketSpeed=5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        direction = (DirectionType)Random.Range(0,2);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector3(BasketSpeed * ((int)direction * 2 - 1), 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Walls")
        {
            switch (direction)
            {
                case DirectionType.LEFT:
                    direction = DirectionType.RIGHT;
                    break;
                case DirectionType.RIGHT:
                    direction = DirectionType.LEFT;
                    break;
                default:
                    break;
            }
        }
    }
}
