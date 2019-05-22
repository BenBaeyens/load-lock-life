using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    GameManager gameManager;
    Vector3 PlayerDir;

    public GameObject player;
    public PlayerController ps;

    GameObject deathEffect;
    public float moveSpeed = 12f;

    public GameObject healParent;

    GameObject heal;

    private void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject[] temp = Resources.FindObjectsOfTypeAll<GameObject>();
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].name == "DeathEffect")
            {
                deathEffect = temp[i];
                Debug.Log("found deatheffect");
                break;
            }
        }
        player = GameObject.Find("Player");
        MoveBullet();
        healParent = gameManager.transform.GetChild(0).gameObject;
        ps = player.GetComponent<PlayerController>();
    }


    private void OnTriggerEnter(Collider other) {
        if (other.name.Contains("Enemy"))
        {
            GameObject[] tempObj = Resources.FindObjectsOfTypeAll<GameObject>();
            for (int i = 0; i < tempObj.Length; i++)
            {
                if(tempObj[i].name == "Heal")
                {
                    Debug.Log("found");
                    heal = tempObj[i];
                    break;
                }
            }
            player.GetComponent<PlayerController>().KillEnemy();
            player.GetComponent<PlayerController>().enemiesKilled++;
            
            GameObject healtemp = Instantiate(heal, other.gameObject.transform.position, other.gameObject.transform.rotation, healParent.transform);
            gameManager.healObjects.Add(healtemp);
            if(deathEffect != null)
              Destroy(Instantiate(deathEffect, other.transform.position, new Quaternion(-transform.rotation.x, transform.rotation.y, -transform.rotation.z, 1)), 2f);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
      
    }

    void MoveBullet() {
      
        PlayerDir = player.transform.forward;
        GetComponent<Rigidbody>().velocity = PlayerDir * moveSpeed;
        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet() {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}
