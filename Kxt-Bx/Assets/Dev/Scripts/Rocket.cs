using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;


public class Rocket : MonoBehaviour
{

    Rigidbody rigidbody;
    [SerializeField] GameManager gameManager;
  
    [SerializeField] float rotationThrust = 300f;
    [SerializeField] float mainThrust = 50f;
    private  bool isFlying;
    Vector3 gravity = 10 * Vector3.down;


    // Use this for initialization
    void Start()
    {
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
            rigidbody.AddForce(gravity, ForceMode.Acceleration);
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
        float x = CrossPlatformInputManager.GetAxis("Jump");
        if (x != 0)
        {
            isFlying = true;
            ApplyThrust(x);
        }
        else
        {
            isFlying = false;
        }
    }

    private void ApplyThrust(float x)
    {
        rigidbody.AddRelativeForce(Vector3.up * mainThrust * x);
    }

    private void Rotate()
    {
        float rotationThisFrame = rotationThrust * Time.deltaTime;

        rigidbody.freezeRotation = true;

        float x = CrossPlatformInputManager.GetAxis("Horizontal") * rotationThisFrame * -1;
        transform.Rotate(Vector3.forward * x);


        /*if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * x);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * x);
        }*/

        rigidbody.freezeRotation = false;
    }


    public bool GetIsFlying()
    {
        return isFlying;
    }



    void OnCollisionEnter(Collision collision)
    {
        if (GameManager.gState != GameManager.gameState.playing)
        {
            return;
        }
        else
        {
            Collider myCollider = collision.contacts[0].thisCollider;

            if (collision.gameObject.tag == "Friendly")
            {
                return;
            }
            else if (collision.gameObject.tag == "Finish")
            {
                if (myCollider.gameObject.tag == "Leg")
                {
                    gameManager.StartSuccessSequence();
                }
                
            }
            else if (collision.gameObject.tag == "Obstacle")
            {
                ReduceHealthForObstacle();
            }
            else if (collision.gameObject.tag == "Enemy")
            {
                Destroy(collision.gameObject);
                ReduceHealthForEnemy();
            }
        }
    }

}
