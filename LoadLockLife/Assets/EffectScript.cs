using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    
    void Start()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
    }

  
}
