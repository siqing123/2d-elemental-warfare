using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBullet : MonoBehaviour
{
    private float lifeSpan = 5.0f;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == ("wall"))
        {
            Destroy(gameObject);
        }
        //
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (lifeSpan > 0.0f)
        {
            lifeSpan -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
