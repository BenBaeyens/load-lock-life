using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    [Header("Game Settings")]
    public int maxHealItems;
    public int maxEnemies;


    [Header("UI elements")]
    public string finalScoreText = "YOUR SCORE: ";
    public string finalHighScoreText = "HIGHSCORE: ";

    public GameObject gameOverObject;
    public TextMeshProUGUI mainScore;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI finalhighscore;

    [Header("Booleans - DO NOT CHANGE")]
    public bool isGameOver = false;

    [Header("List (Auto allocation - DO NOT CHANGE)")]
    public GameObject player;
    public PlayerController playerController;

    public List<GameObject> enemySpawnPoints;
    public List<GameObject> projectiles;
    public List<GameObject> enemies;
    public List<GameObject> healObjects;
    public List<GameObject> effects;

    private void Start() {

        GameObject[] tempGameObjectsArray;
        
        enemySpawnPoints = new List<GameObject>();
        projectiles = new List<GameObject>();
        enemies = new List<GameObject>();
        effects = new List<GameObject>();
        healObjects = new List<GameObject>();

        tempGameObjectsArray = FindObjectsOfType<GameObject>();
        for (int i = 0; i < tempGameObjectsArray.Length; i++)
        {
            if (tempGameObjectsArray[i].GetComponent<EnemySpawner>() != null) // Find all objects that are enemy spawners
                enemySpawnPoints.Add(tempGameObjectsArray[i]);
            if (tempGameObjectsArray[i].GetComponent<EnemyController>() != null) // Find all objects that are enemies
                enemies.Add(tempGameObjectsArray[i]);
            if (tempGameObjectsArray[i].GetComponent<HealObject>() != null) // Find all objects that are heal items
                healObjects.Add(tempGameObjectsArray[i]);
        }

        player = GameObject.Find("Player"); // Locate player
        playerController = player.GetComponent<PlayerController>();
    }

    private void FixedUpdate() {
        KillHealables();
    }


    public void GameOver() {
        PlayerPrefs.SetInt("highscore", playerController.highscore);
        gameOverObject.SetActive(true);
        finalScore.text = finalScoreText + playerController.enemiesKilled.ToString();
        finalhighscore.text = finalHighScoreText + playerController.highscore.ToString();
        mainScore.gameObject.SetActive(false);
        Time.timeScale = 0.3f;
        isGameOver = true;
    }

    public void KillHealables() {
        if (healObjects.Count > maxHealItems)
        {
            Destroy(healObjects[0]);
            healObjects.RemoveAt(0);
        }
    }
}
