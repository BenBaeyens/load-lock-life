using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastPowerup : MonoBehaviour {
    GameObject player;
    GameManager gameManager;

    private void Start() {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == player && !player.GetComponent<PlayerController>().hasPowerup)
        {
            player.GetComponent<PlayerController>().BlastPowerup();
            
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other) {
        OnTriggerEnter(other);
    }
}


