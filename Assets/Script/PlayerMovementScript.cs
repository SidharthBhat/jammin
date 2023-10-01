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
        if (Input.GetButtonDown("Fire1"))
        {
            switch (playerData.InBox)
            {
                case true:
                    playerData.InBox = false;
                    break;

                case false:
                    playerData.InBox = true;
                    break;
            }
        }
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
        float boxmulti = 1;
        if (playerData.InBox)
        {
            boxmulti = 0.5f;
        }

        float xDist = horizontalAxis * SPEED * Time.deltaTime * boxmulti;
        float yDist = verticalAxis * SPEED * Time.deltaTime * boxmulti;

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
    public bool InBox;
}
