using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Tooltip("Where to display gameTime")]
    public TMP_Text timeText;

    public TMP_Text scoreText;
    public TMP_Text gameOverScoreText;

    [Tooltip("Where to display gameTime when GameOver")]
    public TMP_Text gameOverTimeText;

    public WaveSpawner waveSpawner;

    [Header("Info")]
    public float gameTime;
    public bool gameStarted = false;
    public bool gamePaused;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        if (timeText)
        {
            timeText.text = FormatTime(gameTime);
        }
        /*if (gameStarted)
        {
            gameTime += Time.deltaTime;
            timeText.text = FormatTime(gameTime);
        }*/
    }

    // Method for formatting time
    public string FormatTime(float time)
    {
        // Get minutes from time
        int minutes = Mathf.FloorToInt(time / 60);
        // Get seconds from time
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateScore(int scoreValue)
    {
        score += scoreValue;
        if (scoreText)
        {
            scoreText.text = string.Format("{0:0000}", score);
        }
    }

    public void SetPauseGame(bool value)
    {
        Time.timeScale = value ? 0 : 1;
        gamePaused = value;
    }

    public void StartGame()
    {
        gameStarted = true;
    }

    /*public void RestartGame()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject projectile in projectiles)
        {
            Destroy(projectile);
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //player.GetComponent<PlayerCharacter>().Restart();

        HealthController playerHealth = player.GetComponent<HealthController>();
        playerHealth.ChangeHealth(playerHealth.maxHealth);

        gameTime = 0;
        score = 0;
        UpdateScore(score);
        waveSpawner.ReInitialize();
        SetPauseGame(false); 
    }*/

    public void RestartGame()
    {
        SetPauseGame(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToTitle()
    {
        SetPauseGame(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        SetPauseGame(true);
        if (gameOverTimeText)
        {
            gameOverTimeText.text = FormatTime(gameTime);
        }
        if (gameOverScoreText)
        {
            gameOverScoreText.text = string.Format("{0:0000}", score);
        }
    }
}
