using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidbody;
    GameManager gameManager;
  
    [SerializeField] float rotationThrust = 250f;
    [SerializeField] float mainThrust = 50f;
    private bool isFlying;

    // Use this for initialization
    void Start()
    {
        gameManager = GameManager.gameManager;
        rigidbody = GetComponent<Rigidbody>();
        isFlying = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.gState == GameManager.gameState.playing)
        {
            RespondToThrustInput();
            Rotate();
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (GameManager.gState != GameManager.gameState.playing)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                gameManager.StartSuccessSequence();
                break;
            case "Obstacle":
                ReduceHealthForObstacle();
                break;
            case "Enemy":
                Destroy(collision.gameObject);
                ReduceHealthForEnemy();
                break;
        }
    }

    private void ReduceHealthForEnemy()
    {
        gameManager.reduceHealth(gameManager.getReduceEnemy());
    }


    private void ReduceHealthForObstacle()
    {
        gameManager.reduceHealth(gameManager.getReduceObstacle());
    }


    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isFlying = true;
            ApplyThrust();
        }
        else
        {
            isFlying = false;
        }
    }

    private void ApplyThrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * mainThrust);
    }

    private void Rotate()
    {
        float rotationThisFrame = rotationThrust * Time.deltaTime;

        rigidbody.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidbody.freezeRotation = false;
    }


    public bool GetIsFlying()
    {
        return isFlying;
    }
}
