using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject obstacles;
    public Vector3 pos = new(5, 16.5f, 0);
    public int obstaclesCount = 10;
    public float spawnRate = .5f, blinkRate = 50;
    public int startRate = 1, waveRate = 4;

    public Text scoreText, gameOverText, restartText;
    private int score;

    private bool gameOver;

    void Start()
    {
        gameOver = false;
        gameOverText.enabled = false;
        restartText.enabled = false;
        score = 0;
        updateScore();
        StartCoroutine(SpawnWaves());

    }

    void Update()
    {
        if (gameOver)
        {
            Restart();
        }
    }

    IEnumerator SpawnWaves()
    {
        Vector3 spawnPos;
        //Wait for seconds before starting to spawn obstacle
        yield return new WaitForSeconds(startRate);

        while (true)
        {
            for (int i = 0; i < obstaclesCount; i++)
            {
                spawnPos = new Vector3(Random.Range(-pos.x, pos.x), pos.y, pos.z);
                Instantiate(obstacles, spawnPos, Quaternion.identity);
                //Wait for seconds before spawning another obstacle
                yield return new WaitForSeconds(spawnRate);
            }
            //wait for sencods before spawning a new wave
            yield return new WaitForSeconds(waveRate);

            if (obstaclesCount <= 10) obstaclesCount++;
            else if (obstaclesCount <= 30) obstaclesCount += 2;

            if (gameOver) break;
        }

    }

    public void addScore(int newScore)
    {
        score += newScore;
        updateScore();
    }

    void updateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.enabled = true;
        gameOverText.text = "GAMEOVER!";
        gameOver = true;
    }

    public void Restart()
    {
        restartText.enabled = true;
        restartText.text = "Press 'R' to Restart";
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
