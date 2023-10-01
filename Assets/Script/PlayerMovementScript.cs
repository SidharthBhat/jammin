using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    private PlayerData playerData;

    public float SPEED = 20.0f;
    public bool moving = false;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerData.HorizontalFacing = true ;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        if(horizontalAxis > 0)
        {
            playerData.HorizontalFacing = true;
        }
        else if (horizontalAxis < 0)
        {
            playerData.HorizontalFacing = false;
        }

        float xDist = horizontalAxis * SPEED * Time.deltaTime;
        float yDist = verticalAxis * SPEED * Time.deltaTime;

        Vector2 deltaPosition = new Vector2(xDist, yDist);

        moving = deltaPosition.magnitude != 0;
        playerData.Moving = moving;

        rb.MovePosition(rb.position + deltaPosition);
    }

    public PlayerData GetData()
    {
        return playerData;
    }
}

public struct PlayerData
{
    public bool HorizontalFacing;
    public bool VerticalFacing;
    public bool Moving;
}
