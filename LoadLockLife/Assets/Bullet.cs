using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour { 
    

    Vector3 PlayerDir;

    public GameObject player;

    public float moveSpeed = 12f;

    GameObject healParent;

    public GameObject heal;

    private void Start() {

        player = GameObject.Find("Player");
        MoveBullet();
        healParent = GameObject.Find("Heals");

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
            Instantiate(heal, other.gameObject.transform.position, other.gameObject.transform.rotation, healParent.transform);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
      
    }

    void MoveBullet() {
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
        PlayerDir = player.transform.forward;
        GetComponent<Rigidbody>().velocity = PlayerDir * moveSpeed;
        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet() {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}
