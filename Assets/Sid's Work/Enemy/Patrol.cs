using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{

    [SerializeField] private FOV fov;
    public Transform[] patrolPoints;
    private int currentPoint;
    public float speed;
    private float waitTime = 1f;
    //basic gamestate, 0 idle, 1 moving, 2 detect, 3 distraction, 4 boxdetect, 5 boxreveal (lifting the box)
    private int state;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //add detect code here, if nothing patrol, otherwise activate game over. if distraction, do that
        patrol();
    }

    private void patrol()
    {
        if (waitTime >= 0)
        {
            waitTime -= Time.deltaTime;
        }
        else
        {
            if (transform.position != patrolPoints[currentPoint].position)
            {
                direction = transform.position - patrolPoints[currentPoint].position;
                transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, speed * Time.deltaTime);
                state = 1;
            }
            else
            {
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
                waitTime = 1f;
                state = 0;
            }
        }
        if (state == 1)
        {
            fov.setOrigin(transform.position);
            fov.setDirection(direction);
        }
    }
}
