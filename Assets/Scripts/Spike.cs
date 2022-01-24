using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public int baseDmg = 15;
    public float baseVel = 15;
    public int extraDmg = 2;
    float objVel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objVel = Mathf.Abs(collision.GetComponent<Rigidbody2D>().velocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
        if (health && !health.invulnerable)
        {
            int dmg = baseDmg;
            //Rigidbody2D rig = collision.gameObject.GetComponent<Rigidbody2D>();
            if (objVel > baseVel)
            {
                dmg += (int)(objVel - baseVel) / extraDmg;
                Debug.Log(objVel);
                Debug.Log((objVel - baseVel) + " " + (int)(objVel - baseVel) + " " +(int)(objVel - baseVel) / extraDmg);
                Debug.Log(dmg);
            }
            health.TakeDamage(dmg);
            health.invulnerable = true;
            health.invTime = Time.time + health.invInt;
        }
    }
}
