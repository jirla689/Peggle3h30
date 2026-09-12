using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    TMP_Text HighscoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int Highscore = 0;
        if (File.Exists(Application.persistentDataPath + "/SaveScore.txt"))
        {
            using (StreamReader sr = File.OpenText(Application.persistentDataPath + "/SaveScore.txt"))
            {
                Highscore = int.Parse(sr.ReadLine());
                sr.Close();
            }
        }

        HighscoreText.text = "HighScore : "+Highscore;

    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
