using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    GameManager gameManager;
    public GameObject enemy;
    public GameObject enemyParent;
    public float time;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyParent = gameManager.transform.GetChild(1).gameObject;
        InvokeRepeating("spawnEnemy", 2, time);   
    }



    void spawnEnemy() {
        if (enemyParent.gameObject.transform.childCount < gameManager.maxEnemies && gameManager.isGameOver == false)
           Instantiate(enemy, transform.position, transform.rotation, enemyParent.transform);
    }

  
}
