using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemySpawner : MonoBehaviour
{

    GameManager gameManager;
    Animator animator;
    public GameObject enemy;
    public GameObject bigenemy;
    public GameObject enemyParent;
    public float time;
    public bool isSpawnable = true;

    void Start()
    {
        time /= PlayerPrefs.GetInt("difficulty");
        enemyParent = GameObject.Find("GameManager").transform.GetChild(1).gameObject;
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        InvokeRepeating("randomTime", 1, 1);
    }



    void randomTime() {
        if (isSpawnable) {
            time = Random.Range(1f, 4f);
            StartCoroutine(spawnEnemy());
            isSpawnable = false;
        }
    }

 

   


    IEnumerator spawnEnemy() {
        yield return new WaitForSeconds(time);
            
            if (enemyParent.gameObject.transform.childCount < gameManager.maxEnemies && gameManager.isGameOver == false)
            {
                animator.Play("enemyspawner");
                yield return new WaitForSeconds(2);
            float random = Random.value;
            if (random > gameManager.DefaultEnemySpawnChance && !isSpawnable)
            {
                Instantiate(bigenemy, transform.position, transform.rotation, enemyParent.transform);
                isSpawnable = true;
            }
            if(random <= gameManager.DefaultEnemySpawnChance && !isSpawnable)
            {
                Instantiate(enemy, transform.position, transform.rotation, enemyParent.transform);
                
            }
            }
            isSpawnable = true;

        
        
    }
  
}
