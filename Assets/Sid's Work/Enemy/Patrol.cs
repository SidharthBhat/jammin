using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private FOV fieldOfView;
    [SerializeField] private Transform pfFov;
    public float fov;
    public float viewDist;
    public float waitTime = 1f;
    public LayerMask playerLayer;

    public Transform[] patrolPoints;
    private int currentPoint;
    public float speed;
    private float timer = 0;
    //basic gamestate, 0 idle, 1 moving, 2 detect, 3 stun, 4 boxdetect, 5 boxreveal (lifting the box)
    private int state = 0;
    private Vector3 direction;

    public GameObject player;
    public float stunTime;
    private float stunTimer;
    private bool playerCollide = false;

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
        switch (state)
        {
            case 0:
            case 1:
                DetectPlayer();
                break;
            case 2:
                //gameover
                break;
            case 3:
                Stunned();
                break;
            case 4:
                BoxApproach();
                break;
            case 5:
                BoxReveal();
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag.Equals("Player"))
        {
            playerCollide = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerCollide = false;
    }

    private void Stunned()
    {
        if(stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
            //stun animation

        }
        else
        {
            stunTimer = 0;
            state = 0;
        }
    }

    public void Stun()
    {
        state = 3;
        stunTimer = stunTime;
        return;
    }

    public void BoxApproach()
    {
        direction = (GetPosition() - player.transform.position).normalized;
        SetPosition(Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime));
        fieldOfView.SetOrigin(GetPosition());
        fieldOfView.SetDirection(direction);
        if(playerCollide)
        {
            state = 5;
            return;
        }
    }

    public void BoxReveal()
    {
        //TODO
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
                direction = (GetPosition() - patrolPoints[currentPoint].position).normalized;
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
            float angle = Vector3.Angle(direction, playerDirection) -180 + fov/2;
            if(angle < 0)
            {
                angle += 360;
            }
            if (angle < fov / 2f)
            {
                RaycastHit2D detect = Physics2D.Raycast(GetPosition(), playerDirection, viewDist, playerLayer);
                if(detect.collider != null)
                {
                    if (detect.collider.gameObject.tag.Equals("Player"))
                    {
                        /*
                        //state hasn't been added yet, so this can change
                        //if we use two bools, one for move and one for box, i'll change this to if-else
                        //currently running of assumed values, being:
                        //0 default, 1 box, 2 boxmove
                        int state = player.GetState();
                        switch(state)
                        {
                            case 0:
                                state=2;
                                return;
                            case 1:
                                //add that if box collides with enemy, set to state 5 
                                return;
                            case 2:
                                //box suspicion
                                state=4;
                                return;
                                //either they stop and look, then get back to it, or they just go to player and wait, and if player moves then they try to open the box, or they open the box anyways
                        }
                        */
                    }
                }
            }
        }
        PatrolMove();
    }
}
