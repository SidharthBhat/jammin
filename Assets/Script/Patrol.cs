using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private bool StunSetting = false;

    [SerializeField] private GameObject GOUI;

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetState(int state)
    {
        Debug.Log("Eef Freef");
        this.state = state;

    }

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = 0;
        fieldOfView=Instantiate(pfFov, null).GetComponent<FOV>();
        fieldOfView.SetFOV(fov);
        fieldOfView.SetViewDist(viewDist);
        PatrolMove();
        GOUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (StunSetting== false) {
        //swap false with stun condition
        switch (state)
        {
            case 0:
            case 1:
                DetectPlayer();
                break;
            case 2:
                GOUI.SetActive(true);
                Time.timeScale = 0.0f;
                return;
            case 3:
                Stunned();
                break;
            case 4:
                BoxApproach();
                break;
            case 6:
                Stun();
                break;
            default:
                break;
        }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag.Equals("Player"))
        {
            playerCollide = true;
        }
        else if (collision.collider.gameObject.tag.Equals("Monkey"))
        {
            Stun();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerCollide = false;
    }

    private void Stunned()
    {
        Debug.Log("bingus");
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
        //state = 3;
        //stunTimer = stunTime;
        StartCoroutine(StunLock()); 
        return;
    }

    IEnumerator StunLock()
    {
        Debug.Log("its not a tumor");
        StunSetting = true;
        yield return new WaitForSeconds(stunTime);
        Debug.Log("ok its a tumor");
        StunSetting = false;
    }


    public void BoxApproach()
    {
        //excalamation on top of head???
        direction = (GetPosition() - player.transform.position).normalized;
        SetPosition(Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime));
        fieldOfView.SetOrigin(GetPosition());
        fieldOfView.SetDirection(direction);
        if(Vector2.Distance(player.transform.position, transform.position) < 0.1)
        {
            state = 5;
            return;
        }
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
        if (Vector3.Distance(GetPosition(), player.transform.position) < viewDist)
        {
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
                        PlayerData playerState = player.GetComponent<PlayerMovementScript>().GetData();
                        if(!playerState.InBox)
                        {
                            state = 2;
                            return;
                        }
                        else
                        {
                            if(playerState.Moving)
                            {
                                state = 2;
                                return;
                            }
                        }
                    }
                }
            }
        }
        PatrolMove();
    }
}
