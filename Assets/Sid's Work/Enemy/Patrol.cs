using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    //NOTE: can't seem to get the angle to line up properly, possible points of failure are Vector2Angle in FOV, setDirection in FOV, and vector inputted in this class
    private FOV fieldOfView;
    [SerializeField] private Transform pfFov;
    public float fov;
    public float viewDist;

    public Transform[] patrolPoints;
    private int currentPoint;
    public float speed;
    private float waitTime = 1f;
    //basic gamestate, 0 idle, 1 moving, 2 detect, 3 distraction, 4 boxdetect, 5 boxreveal (lifting the box)
    private int state;
    private Vector3 direction;

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = 0;
        fieldOfView=Instantiate(pfFov, null).GetComponent<FOV>();
        fieldOfView.SetFOV(fov);
        fieldOfView.SetViewDist(viewDist);
    }

    // Update is called once per frame
    void Update()
    {
        //add detect code here, if nothing patrol, otherwise activate game over. if distraction, do that
        PatrolMove();
        DetectPlayer();
    }
    
    private void PatrolMove()
    {
        if (waitTime >= 0)
        {
            waitTime -= Time.deltaTime;
        }
        else
        {
            if (GetPosition() != patrolPoints[currentPoint].position)
            {
                direction = GetPosition() - patrolPoints[currentPoint].position;
                SetPosition(Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, speed * Time.deltaTime));
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
            fieldOfView.SetOrigin(GetPosition());
            fieldOfView.SetDirection(direction);
        }
    }

    private void DetectPlayer()
    {
        //swap out zero for player position
        if (Vector3.Distance(GetPosition(), Vector3.zero) < viewDist)
        {
            //swap zero for player position
            Vector3 playerDirection = (Vector3.zero - GetPosition()).normalized;
            if(Vector3.Angle(direction, playerDirection) < fov / 2f)
            {
                RaycastHit2D detect = Physics2D.Raycast(GetPosition(), playerDirection, viewDist);
                if(detect != null)
                {
                    //uncomment when player is added
                    //if(detect.collider.gameObject.GetComponent<Player>() != null)
                    //{
                        //check detect states here
                    //}
                }
            }
        }
    }
}
