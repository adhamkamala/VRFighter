using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{

    public int score = 0;
    public int lives = 6;
    public int maxScore;
    public Text LifeText;
    public Text ScoreText;
    public Text LifeTextVr;
    public Text ScoreTextVr;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateLife();
    }

     public void AddScore(int newScore)
    {
        score += newScore;
    }

    public void UpdateScore()
    {
        ScoreText.text = "Score: "+score.ToString();
        ScoreTextVr.text = "Score: " + score.ToString();
    }

    public void LessLife() //2
    {
        lives--;
        UpdateLife();
    }

    private void UpdateLife()
    {
        LifeText.text = "Life: " + lives;
        LifeTextVr.text = "Life: " + lives;

    }

    public bool NoLifeChecker() { //1
        if (lives == 0) {
            return true;
        } else { return false;}
    }
    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }
}
