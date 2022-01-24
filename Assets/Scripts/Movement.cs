using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController2D controller;
    public PlayerHealth health;
    public Rigidbody2D rig;

    public float speed = 50f;
    bool jump = false;
    bool dashing = false;
    public float dashTime = .5f;
    float dashEnd;
    public float dashMod = 2f;
    public int dashDmg = 10;
    float move = 0f;
    float gravityScale = 3f;

    void Start()
    {
        gravityScale = rig.gravityScale;
    }

    void Update()
    {
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
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            dashing = true;
            health.TakeDamage(dashDmg);
            rig.velocity = new Vector2(0f, 0f);
            rig.gravityScale = 0f;
            dashEnd = Time.time + dashTime;
        }
    }

    void FixedUpdate()
    {
        controller.Move(move * speed * Time.deltaTime, jump);
        jump = false;
    }
}
