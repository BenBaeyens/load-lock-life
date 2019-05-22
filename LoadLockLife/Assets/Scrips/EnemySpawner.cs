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
    public bool isSpawnable = true;

    void Start()
    {
        enemyParent = GameObject.Find("GameManager").transform.GetChild(1).gameObject;
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyParent = gameManager.transform.GetChild(1).gameObject;
        InvokeRepeating("randomTime", 1, 1);
    }



    void randomTime() {
        if (isSpawnable) {
            time = Random.Range(1f, 4f);
            StartCoroutine(spawnEnemy());
            isSpawnable = false;
        }
    }

        if (enemyParent.gameObject.transform.childCount < gameManager.maxEnemies && gameManager.isGameOver == false)
        {
 

   


    IEnumerator spawnEnemy() {
        yield return new WaitForSeconds(time);
            
            if (enemyParent.gameObject.transform.childCount < gameManager.maxEnemies && gameManager.isGameOver == false)
            {
                animator.Play("enemyspawner");
                yield return new WaitForSeconds(2);
                Instantiate(enemy, transform.position, transform.rotation, enemyParent.transform);
            }
            isSpawnable = true;

        
        
    }
  
}
