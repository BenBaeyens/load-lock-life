using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;
    public GameObject enemyParent;
    public float time;

    void Start()
    {
        InvokeRepeating("spawnEnemy", time, time);   
    }



    void spawnEnemy() {
        if (enemyParent.gameObject.transform.childCount < 20)
           Instantiate(enemy, transform.position, transform.rotation, enemyParent.transform);
    }

  
}
