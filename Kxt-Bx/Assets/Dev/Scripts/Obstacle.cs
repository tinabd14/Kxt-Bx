using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    public GameObject explosionPrefab;

    void OnCollisionEnter(Collision collision)                      // this is used for things that explode on impact and are NOT triggers
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject, 0);      // destroy the object whenever it hits something

            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, transform.rotation);
            }
        }
    }
}