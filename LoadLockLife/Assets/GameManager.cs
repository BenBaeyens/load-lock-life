using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemySpawnPoints;
    public List<GameObject> projectiles;
    public List<GameObject> enemies;
    public List<GameObject> healObjects;
    public List<GameObject> effects;

    public GameObject player;

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



    }
}
