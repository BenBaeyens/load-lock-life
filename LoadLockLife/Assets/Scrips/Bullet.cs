using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    GameManager gameManager;
    Vector3 Dir;

    public GameObject player;
    public PlayerController ps;

    GameObject deathEffect;
    public float moveSpeed = 12f;

    public GameObject healParent;
    public GameObject godModePrefab;
    public GameObject infShotPrefab;
    public GameObject blastPrefab;

    GameObject heal;

    private void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject[] temp = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject g in temp)
        {
            if (g.name == "GodMode")
                godModePrefab = g;
            if (g.name == "Infinite Shooting")
                infShotPrefab = g;
            if (g.name == "Blast")
                blastPrefab = g;
            if (g.name == "DeathEffect")
                deathEffect = g;
            player = GameObject.Find("Player");
            MoveBullet();
            healParent = gameManager.transform.GetChild(0).gameObject;
            ps = player.GetComponent<PlayerController>();
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.name.Contains("Enemy"))
        {
            GameObject[] tempObj = Resources.FindObjectsOfTypeAll<GameObject>();
            for (int i = 0; i < tempObj.Length; i++)
            {
                if(tempObj[i].name == "Heal")
                {
                    heal = tempObj[i];
                    break;
                }
            }
            player.GetComponent<PlayerController>().KillEnemy();
            player.GetComponent<PlayerController>().enemiesKilled++;
            PowerupDrop();
            
            GameObject healtemp = Instantiate(heal, other.gameObject.transform.position, other.gameObject.transform.rotation, healParent.transform);
            gameManager.healObjects.Add(healtemp);
            if(deathEffect != null)
              Destroy(Instantiate(deathEffect, other.transform.position, new Quaternion(-transform.rotation.x, transform.rotation.y, -transform.rotation.z, 1)), 2f);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
      
    }

    void MoveBullet() {
        
        Dir = transform.forward;
        GetComponent<Rigidbody>().velocity = Dir * moveSpeed;
        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet() {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }

    void PowerupDrop() {
        int rand = Random.Range(0, gameManager.godModePowerupDropRate);
        if (rand == 1)
            Instantiate(godModePrefab, transform.position, transform.rotation);
        rand = Random.Range(0, gameManager.infShotPowerupDropRate);
        if (rand == 1)
            Instantiate(infShotPrefab, transform.position, transform.rotation);
        rand = Random.Range(0, gameManager.blastPowerupDropRate);
        if (rand == 1)
            Instantiate(blastPrefab, transform.position, transform.rotation);
    }
}
