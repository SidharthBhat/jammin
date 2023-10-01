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
    public float waitTime = 1f;

    public Transform[] patrolPoints;
    private int currentPoint;
    public float speed;
    private float timer = 0;
    //basic gamestate, 0 idle, 1 moving, 2 detect, 3 distraction, 4 boxdetect, 5 boxreveal (lifting the box)
    private int state;
    private Vector3 direction;

    public GameObject player;


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
        PatrolMove();
    }

    // Update is called once per frame
    void Update()
    {

        //swap false with stun condition
        if (false)
        {
            Stunned();
        }
        else
        {
            DetectPlayer();
        }
    }

    private void Stunned()
    {

    }
    
    private void PatrolMove()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
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
                timer = waitTime;
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
        //PatrolMove();
        //swap out out for player position function
        if (Vector3.Distance(GetPosition(), player.transform.position) < viewDist)
        {
            //swap out for player position function
            Vector3 playerDirection = (player.transform.position - GetPosition()).normalized;
            if(Vector3.Angle(direction, playerDirection) < fov / 2f)
            {
                RaycastHit2D detect = Physics2D.Raycast(GetPosition(), playerDirection, viewDist);
                if(detect != null)
                {
                    /*
                    //uncomment when player is added
                    if(detect.collider.gameObject.GetComponent<Player>() != null)
                    {
                        //state hasn't been added yet, so this can change
                        //if we use two bools, one for move and one for box, i'll change this to if-else
                        //currently running of assumed values, being:
                        //0 default, 1 box, 2 boxmove
                        int state = player.GetState();
                        switch(state)
                        {
                            case 0:
                                //game over
                                break
                            case 1:
                                break
                            case 2:
                                //box suspicion
                                //either they stop and look, then get back to it, or they just go to player and wait, and if player moves then they try to open the box, or they open the box anyways
                                break
                        }
                    }
                    */
                    //remove this once detect code is implemented
                    return;
                }
            }
        }
        PatrolMove();
    }
}
