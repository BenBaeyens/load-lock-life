using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] Vector3 offset;

    Vector3 oldPos;
    Vector3 newPos;

    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        oldPos = transform.position;
        newPos = player.transform.position - offset;

        gameObject.transform.position = Vector3.Lerp(oldPos, newPos, 0.125f);
    }
}
