using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;
    public GameObject titleScreen;
    public GameObject hud;
    public Text scoreText, bestScoreText, gameOptionsText;
    public int score, bestScore;

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("HighScore", 0);
        bestScoreText.text = "Best: " + bestScore;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main_Menu");
        }
    }

    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    // CheckForBestScore
    // if current score greater then bestScore
    // bestScore = currentScore

    public void CheckForBestScore()
    {
        Debug.Log(score);
        Debug.Log(bestScore);
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("HighScore", bestScore);
            bestScoreText.text = "Best: " + bestScore;
        }
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
        hud.SetActive(false);
        gameOptionsText.enabled = true;
        //StartCoroutine(RealoadScene());
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        hud.SetActive(true);
        scoreText.text = "Score: 0";
        gameOptionsText.enabled = false;
    }
    
    public IEnumerator RealoadScene()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Main_Menu");
    }

    // Reumse play
    public void ResumePlay()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.ResumeGame();
    }

    // Back to main menu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
