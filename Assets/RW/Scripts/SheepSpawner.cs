using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class SheepSpawner : MonoBehaviour
{
    public bool canSpawn = true;
    public GameObject sheepPrefab;
    public List<Transform> sheepSpawnPositions = new List<Transform>();
    public float timeBetweenSpawns;
    public int startingLives = 3; // Number of lives to start with
    public Text livesText; // Reference to the UI Text element for lives
    public Text scoreText; // Reference to the UI Text element for score

    private List<GameObject> sheepList = new List<GameObject>();
    private int currentLives;
    private int currentScore;

    void Start()
    {
        currentLives = startingLives;
        currentScore = 0; // Initialize score
        UpdateLivesText();
        UpdateScoreText(); // Update score display at start
        StartCoroutine(SpawnRoutine());
    }

    private void SpawnSheep()
    {
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position;
        GameObject sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation);
        sheepList.Add(sheep);
        sheep.GetComponent<Sheep>().SetSpawner(this);
    }

    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            SpawnSheep();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }

    public void LoseLife()
    {
        currentLives--;
        UpdateLivesText();

        if (currentLives <= 0)
        {
            canSpawn = false;
            Debug.Log("Game Over!");
            // Game over logic - score stops increasing naturally as sheep stop spawning/being hit
        }
    }

    public void IncrementScore()
    {
        // Only increment score if the game is not over
        if (currentLives > 0)
        {
            currentScore++;
            Debug.Log("Score Incremented! New Score: " + currentScore); // DEBUG LOG
            UpdateScoreText();
        }
        else
        {
            Debug.Log("Score NOT Incremented. Game Over (Lives <= 0)."); // DEBUG LOG
        }
    }

    private void UpdateLivesText()
    {
        if (livesText != null)
        {
            if (currentLives <= 0)
            {
                livesText.text = "You lose!";
            }
            else
            {
                livesText.text = "Lives: " + currentLives;
            }
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
        else
        {
            Debug.LogError("Score Text UI element not assigned in SheepSpawner!"); // DEBUG LOG
        }
    }
}
