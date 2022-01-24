using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    float nextFire;
    public float fireRate = 3.5f;
    public float shotForce = 35f;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        nextFire = Time.time + fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextFire <= Time.time)
        {
            Vector3 pos = this.transform.position;
            pos.z = 1;
            Rigidbody2D shot = Instantiate(prefab, pos, Quaternion.identity).GetComponent<Rigidbody2D>();
            Vector2 force = new Vector2(0, shotForce);
            shot.AddForce(force, ForceMode2D.Impulse);
            nextFire = Time.time + fireRate;
        }
    }
}
