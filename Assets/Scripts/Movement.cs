using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController2D controller;
    public PlayerHealth health;
    public Rigidbody2D rig;

    public float speed = 50f;
    public float airSpd = 25f;
    bool jump = false;
    public bool dashing = false;
    public float dashTime = .5f;
    float dashEnd;
    public float dashMod = 2f;
    public int dashDmg = 10;
    float move = 0f;
    float gravityScale = 3f;

    public Animator anim;
    public string blastAnim;

    void Start()
    {
        gravityScale = rig.gravityScale;
    }

    void Update()
    {
        //Physics2D.Raycast(transform.position, -transform.up, 1);
        if (dashing && dashEnd < Time.time)
        {
            dashing = false;
            rig.gravityScale = gravityScale;
        }
        if (!dashing)
        {
            move = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            move = dashMod;
            if (!controller.m_FacingRight)
            {
                move *= -1;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            if (controller.m_Grounded)
            {
                anim.Play(blastAnim);
            }
        }
        if (Input.GetButtonDown("Dash"))
        {
            dashing = true;
            anim.Play(blastAnim);
            health.TakeDamage(dashDmg);
            rig.velocity = new Vector2(0f, 0f);
            rig.gravityScale = 0f;
            dashEnd = Time.time + dashTime;
        }
    }

    void FixedUpdate()
    {
        if (controller.m_Grounded)
        {
            controller.Move(move * speed * Time.deltaTime, jump);
        }
        else
        {
            controller.Move(move * airSpd * Time.deltaTime, jump);
        }

        jump = false;
    }
}
