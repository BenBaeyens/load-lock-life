using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemySpawner : MonoBehaviour
{

    GameManager gameManager;
    Animator animator;
    public GameObject enemy;
    public GameObject enemyParent;
    public float time;

    void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyParent = gameManager.transform.GetChild(1).gameObject;
        InvokeRepeating("spawnEnemy", 2, time);
        InvokeRepeating("spawnEnemyAnimation", 0, time);
    }



    void spawnEnemy() {
        if (enemyParent.gameObject.transform.childCount < gameManager.maxEnemies && gameManager.isGameOver == false)
        {
           
            Instantiate(enemy, transform.position, transform.rotation, enemyParent.transform);

        }
    }

    void spawnEnemyAnimation() {
        if (enemyParent.gameObject.transform.childCount < gameManager.maxEnemies && gameManager.isGameOver == false)
        {
            animator.Play("enemyspawner");
        }
    }

  
}
