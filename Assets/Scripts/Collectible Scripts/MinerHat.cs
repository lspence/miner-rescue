using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerHat : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == Tags.PLAYER)
        {
            GameManger.instance.IncreaseTime();
            Destroy(gameObject);
        }
    }
}
