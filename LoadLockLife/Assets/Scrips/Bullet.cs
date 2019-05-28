using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    GameManager gameManager;
    Vector3 Dir;

    public GameObject player;
    public PlayerController ps;

    GameObject deathEffect;
    GameObject bigdeathEffect;
    public float moveSpeed = 12f;

    GameObject healParent;
    GameObject godModePrefab;
    GameObject infShotPrefab;
    GameObject blastPrefab;

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
            if (g.name == "Heal")
                heal = g;
            if (g.name == "BigDeathEffect")
                bigdeathEffect = g;
            player = GameObject.Find("Player");
            MoveBullet();
            healParent = gameManager.transform.GetChild(0).gameObject;
            ps = player.GetComponent<PlayerController>();
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.name.Contains( "DefaultEnemy"))
        {
            KillEnemy(other);
        }
        if (other.name.Contains("BigEnemy"))
        {
            other.GetComponent<BigEnemyController>().enemyHealth -= 1;
            if(other.GetComponent<BigEnemyController>().enemyHealth <= 0)
            {
                KillEnemy(other);
            } else
            {
                other.transform.localScale /= 2;
                player.GetComponent<PlayerController>().KillEnemy();
                if(deathEffect != null)
                    Destroy(Instantiate(bigdeathEffect, other.transform.position, new Quaternion(-transform.rotation.x, transform.rotation.y, -transform.rotation.z, 1)), 2f)
            }
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

    void KillEnemy(Collider other) {
        player.GetComponent<PlayerController>().KillEnemy();
        player.GetComponent<PlayerController>().enemiesKilled++;
        PowerupDrop();

        GameObject healtemp = Instantiate(heal, other.gameObject.transform.position, other.gameObject.transform.rotation, healParent.transform);
        gameManager.healObjects.Add(healtemp);
        if (deathEffect != null)
            Destroy(Instantiate(deathEffect, other.transform.position, new Quaternion(-transform.rotation.x, transform.rotation.y, -transform.rotation.z, 1)), 2f);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
