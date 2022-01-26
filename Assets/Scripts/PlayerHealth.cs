using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public bool invulnerable = false;
    public float invInt = 3f;
    public float invTime;

    public Slider healthBar;

    void Update()
    {
        if (invTime < Time.time)
        {
            invulnerable = false;
        }
        //if (health <= 0)
        //{
        //    SceneManager.LoadScene("Level1");
        //}
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        healthBar.value = health;
    }
}
