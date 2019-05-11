using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;

    [SerializeField] bool obstacleCanMove = false;

    [SerializeField] Vector3 movementVector = new Vector3(0f, 10f, 0f);
    [Range(0, 1)] [SerializeField] float movementFactor;
    [SerializeField] float period = 2f;

    Vector3 startingPos;

    // Use this for initialization
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(obstacleCanMove)
        {
            MoveObstacle();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }


    private void MoveObstacle()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        }

        float cycles = Time.time / period;
        const float tau = 2f * Mathf.PI;
        float rawSinWave = Mathf.Sin(tau * cycles);

        movementFactor = rawSinWave / 2f + 0.5f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}