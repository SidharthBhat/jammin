using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovementScript PMS;
    private Animator anim;
    private SpriteRenderer sprite;

    private static readonly int Idle = Animator.StringToHash("kid_idle");
    private static readonly int Walk = Animator.StringToHash("kid_walk");
    private static readonly int Throw = Animator.StringToHash("kid_throw");


    // Start is called before the first frame update
    void Start()
    {
        PMS = GetComponentInParent<PlayerMovementScript>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerData pdata = PMS.GetData();

        if (pdata.HorizontalFacing)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }

        if (pdata.Moving)
        {
            anim.CrossFade(Walk, 0f);
        }
        else
        {
            anim.CrossFade(Idle, 0f);
        }
    }
}
