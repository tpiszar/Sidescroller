using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public int dmg = 15;

    public float startY;

    void Start()
    {
        startY = this.transform.position.y;
    }

    void Update()
    {
        if (this.transform.position.y < startY)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth hp = collision.GetComponent<PlayerHealth>();
        if (hp)
        {
            hp.TakeDamage(dmg);
            Destroy(this.gameObject);
        }
    }
}
