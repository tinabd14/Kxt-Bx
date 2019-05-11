using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] ParticleSystem thrustParticle;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem deathParticle;

    [SerializeField] private Rocket rocket;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ExplodeAppropriatePartice();
    }

    private void ExplodeAppropriatePartice()
    {
        if (GameManager.gState == GameManager.gameState.playing)
        {
            if (rocket.GetIsFlying())
            {
                thrustParticle.Play();
            }
            else
            {
                thrustParticle.Stop();
            }
        }
        else if (GameManager.gState == GameManager.gameState.success)
        {
            successParticle.Play();
        }
        else if (GameManager.gState == GameManager.gameState.end)
        {
            deathParticle.Play();
        }
    }
}