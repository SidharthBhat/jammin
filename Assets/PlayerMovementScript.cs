using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public float SPEED = 20.0f;
    public bool moving = false;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        float xDist = horizontalAxis * SPEED * Time.deltaTime;
        float yDist = verticalAxis * SPEED * Time.deltaTime;

        Vector2 deltaPosition = new Vector2(xDist, yDist);

        moving = deltaPosition.magnitude != 0;

        rb.MovePosition(rb.position + deltaPosition);
    }
}
