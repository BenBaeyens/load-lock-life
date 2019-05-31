using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigEnemyController : MonoBehaviour {
    GameManager gameManager;
    public GameObject bigdeathEffect;
    Transform player;
    public NavMeshAgent agent;

    public float enemyHealth;

    // Start is called before the first frame update
    void Start() {
        GameObject[] temp = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject g in temp)
        {
            if (g.name == "BigDeathEffect")
                bigdeathEffect = g;
            
        }

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        float distance = Vector3.Distance(player.position, transform.position);

        agent.SetDestination(player.position);

        agent.stoppingDistance = 0;


        if (distance <= agent.stoppingDistance)
        {
            FaceTarget();
        }

    }

    void FaceTarget() {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player")
        {


            player.GetComponent<PlayerController>().Hurt();
            if (bigdeathEffect != null)
                Instantiate(bigdeathEffect, transform);
            if (!player.GetComponent<PlayerController>().canBeHurt)
            {
                player.GetComponent<PlayerController>().enemiesKilled++;
                player.GetComponent<PlayerController>().KillEnemy();
                player.GetComponent<PlayerController>().audioSource.PlayOneShot(player.GetComponent<PlayerController>().godmodeSound);
                Destroy(Instantiate(bigdeathEffect, transform.position, new Quaternion(-transform.rotation.x, transform.rotation.y, -transform.rotation.z, 1)), 2f);
            }
            Destroy(gameObject);
        }
    }



}
