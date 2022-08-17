using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    int health = 1;

    virtual public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Death();
        }
    }

    virtual public void Death()
    {
        Destroy(this);
    }
}
