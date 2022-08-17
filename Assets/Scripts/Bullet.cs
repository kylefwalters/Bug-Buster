using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDmg = 0;
    public float bulletSpeed = 0.0f;
    float bulletRange = 100.0f;

    private void Update()
    {
        Vector3 movement = transform.forward * bulletSpeed * Time.deltaTime;
        transform.position += movement;
        bulletRange -= movement.magnitude;
        if (bulletRange <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject gObject = collision.gameObject;
        if (gObject.tag == "Enemy")
        {
            gObject.GetComponent<Health>().TakeDamage(bulletDmg);
            Destroy(this);
        }
    }
}
