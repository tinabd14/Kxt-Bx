using UnityEngine;
using UnityEngine.UI;

public class Instruction : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Text text;

    // Start is called before the first frame update
    void Update()
    {
        if(!GameManager.instructionsFinished)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }

    public void StartButtonPressed()
    {
        GameManager.instructionsFinished = true;
    }
}
