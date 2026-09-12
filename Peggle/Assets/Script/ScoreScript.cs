using System.IO;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    int score = 0;
    [SerializeField]
    TMP_Text ScoreText;
    [SerializeField]
    TMP_Text ScoreMake;
    [SerializeField]
    TMP_Text BallText;
    [SerializeField]
    TMP_Text FinishText;
    float TimeShow = 1.5f;
    float TimeShowMax = 1.5f;
    public bool Scored = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScoreText.text = "Score : 0";
        ScoreMake.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Scored)
        {
            TimeShow -= Time.deltaTime;
            if (TimeShow < 0)
            {
                ScoreMake.gameObject.SetActive(false);
                TimeShow = TimeShowMax;
                Scored = false;
            }
        }
    }

    public void ChangeMakeScore(int amountBlue, int amountOrange, int amountViolet)
    {
        int total = amountBlue + amountOrange + amountViolet;
        ScoreMake.text = $"({amountBlue}X{(int)PegsScript.PegsType.BLUE}+{amountOrange}X{(int)PegsScript.PegsType.ORANGE}" +
            $"+{amountViolet}X{(int)PegsScript.PegsType.VIOLET})X{total}";
        ScoreMake.gameObject.SetActive(true);
    }

    public void ChangeScore(int amountBlue, int amountOrange, int amountViolet)
    {
        int total = amountBlue + amountOrange + amountViolet;

        int BlueScore = amountBlue * (int)PegsScript.PegsType.BLUE;
        int OrangeScore = amountOrange * (int)PegsScript.PegsType.ORANGE;
        int VioletScore = amountViolet * (int)PegsScript.PegsType.VIOLET;

        int TotalScore = (BlueScore + OrangeScore + VioletScore) * total;
        score += TotalScore;
        ScoreText.text = "Score : " + score;
        Scored = true;
        GameManager.instance.CheckEndGame();
    }

    public void ChangeBall(int nbBall)
    {
        BallText.text = "NB Ball : " + nbBall.ToString("00");
    }

    public void SaveScore()
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

        if (score > Highscore)
        {
            Highscore = score;

            using (StreamWriter sw = File.CreateText(Application.persistentDataPath + "/SaveScore.txt"))
            {
                sw.WriteLine(Highscore);
                sw.Close();
            }
        }
    }

    public void SetTextFinish(bool win)
    {
        switch (win)
        {
            case true:
                FinishText.text = "WIN!";
                FinishText.color = Color.green;
                break;
            case false:
                FinishText.text = "LOSE!";
                FinishText.color = Color.red;
                break;
        }
    }
}
