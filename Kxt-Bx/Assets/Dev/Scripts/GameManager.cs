using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text LevelText;
    [SerializeField] private Text healthText;
    [SerializeField] private Text timeText;

    public enum gameState {playing, end, success};
    public static gameState gState;

    [SerializeField] private float time = 15f;
    [SerializeField] private int health = 100;

    [SerializeField] private int reduceHealthWhenEnemy = 30;
    [SerializeField] private int reduceHealthWhenObstacle = 20;


    private bool failed;
    private bool succeeded;
    int currentLevelIndex;


    // Start is called before the first frame update
    void Start()
    {
        gState = gameState.playing;

        failed = false;
        succeeded = false;

        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        LevelText.text = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if(failed)
        {
            StartEndSequence();
        }
        UpdateHealth();
        UpdateTime();
    }


    public void StartSuccessSequence()
    {
        succeeded = true;
        gState = gameState.success;
        Invoke("LoadNextLevel", 1);
    }


    public void StartEndSequence()
    {
        gState = gameState.end;
        Invoke("LoadCurrentLevel", 1);
    }

    private void LoadCurrentLevel()
    {
        SceneManager.LoadScene(currentLevelIndex);
    }



    private void LoadNextLevel()
    {
        if (currentLevelIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            currentLevelIndex = 0;
            SceneManager.LoadScene(currentLevelIndex);
        }
        else
        {
            SceneManager.LoadScene(currentLevelIndex + 1);
        }
    }


    private void UpdateHealth()
    {
        if(health <= 0)
        {
            failed = true;
            health = 0;
        }
        healthText.text = health.ToString();
    }

    private void UpdateTime()
    {
        if(time >= 0)
        {
            time = time - Time.deltaTime;
        }
        else
        {
            time = 0f;
            failed = true;
        }
        timeText.text = time.ToString("0.00");
    }


    public bool getFailed()
    {
        return failed;
    }

    public void setFailed(bool failed)
    {
        this.failed = failed;
    }


    public bool getsucceeded()
    {
        return succeeded;
    }

    public void setSucceeded(bool succeeded)
    {
        this.succeeded = succeeded;
    }

    public void reduceHealth(int reduce)
    {
        health -= reduce;
    }

    public void increaseHealth(int increase)
    {
        health += increase;
    }


    public int getReduceEnemy()
    {
        return reduceHealthWhenEnemy;
    }


    public int getReduceObstacle()
    {
        return reduceHealthWhenObstacle;
    }
}
