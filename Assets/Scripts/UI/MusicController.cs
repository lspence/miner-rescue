using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance = null;

    private void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        
    }

    
    private void Update()
    {
        
    }

    private void MakeInstance()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
