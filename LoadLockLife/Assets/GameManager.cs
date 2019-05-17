using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

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
        GameObject[] tempGameObjectList;

        enemySpawnPoints = new List<GameObject>();
        projectiles = new List<GameObject>();
        enemies = new List<GameObject>();
        effects = new List<GameObject>();
        healObjects = new List<GameObject>();

        tempGameObjectList = FindObjectsOfType<GameObject>();
        for (int i = 0; i < tempGameObjectList.Length; i++)
        {
            if (tempGameObjectList[i].GetComponent<EnemySpawner>() != null) // Find all objects that are enemy spawners
                enemySpawnPoints.Add(tempGameObjectList[i]);
            if (tempGameObjectList[i].GetComponent<EnemyController>() != null) // Find all objects that are enemies
                enemies.Add(tempGameObjectList[i]);
            if (tempGameObjectList[i].GetComponent<HealObject>() != null) // Find all objects that are heal items
                healObjects.Add(tempGameObjectList[i]);
        }

        player = GameObject.Find("Player"); // Locate player
        playerController = player.GetComponent<PlayerController>();



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
}
