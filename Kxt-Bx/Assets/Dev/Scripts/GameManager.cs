using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text LevelText;
    [SerializeField] private Text healthText;
    [SerializeField] private Text timeText;

    public enum gameState {stop, playing, end, success};
    public static gameState gState;

    [SerializeField] private float time = 15f;
    [SerializeField] private int health = 100;

    [SerializeField] private int reduceHealthWhenEnemy = 30;
    [SerializeField] private int reduceHealthWhenObstacle = 20;


    private bool failed;
    private bool succeeded;
    int currentLevelIndex;

    public static bool isPaused;
    public static float start;
    public static bool instructionsFinished;
    // Start is called before the first frame update
    void Start()
    {
        gState = gameState.stop;
        failed = false;
        succeeded = false;

        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        LevelText.text = SceneManager.GetActiveScene().name;

        isPaused = false;
        start = 0;

        if (SceneManager.GetActiveScene().name == "Instruction 1" || SceneManager.GetActiveScene().name == "Instruction 2")
        {
            instructionsFinished = false;
        }
        else
        {
            instructionsFinished = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetAxis("Start") != 0)
        {
            start = start + 0.0001f;
        }
        if (start == 0.0001f)
        {
            gState = gameState.playing;
        }

        if(gState == gameState.playing)
        {
            if (CrossPlatformInputManager.GetButtonDown("Pause"))
            {
                isPaused = !isPaused;
            }
            if (isPaused)
            {
                Time.timeScale = 0;
            }
            else if (!isPaused)
            {
                Time.timeScale = 1;
                if (failed)
                {
                    StartEndSequence();
                }
                UpdateHealth();
                UpdateTime();
            }
        }
        
    }

    private void PlayGame()
    {
        if(!isPaused)
        {
            if (failed)
            {
                StartEndSequence();
            }
            UpdateHealth();
            UpdateTime();
        }
    }


    public void StartSuccessSequence()
    {
        succeeded = true;
        start = 0;
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
