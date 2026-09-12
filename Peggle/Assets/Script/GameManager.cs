using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    LevelCreationScript levelCreationScript;
    [SerializeField]
    ScoreScript scoreScript;
    [SerializeField]
    AimScript aimScript;

    public static GameManager instance;

    public bool EndGame = false;
    float timeStay=1.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(EndGame)
        {
            timeStay-=Time.deltaTime;
            if (timeStay < 0)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    public void CheckEndGame()
    {
        if (levelCreationScript.FinishGame())
        {
            scoreScript.SaveScore();
            EndGame = true;
            scoreScript.SetTextFinish(true);
        }
        else if(!aimScript.HadBall())
        {
            EndGame = true;
            scoreScript.SetTextFinish(false);
        }
        else
        {
            levelCreationScript.NewRound();
        }
    }
}
