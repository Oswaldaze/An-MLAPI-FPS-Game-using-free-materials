using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeStorage : MonoBehaviour
{
    public bool ARMode = false;
    public bool OnlineMode = false;
    public bool Host = false;
    public string HostIpAddress= "100.66.132.215";
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        
    }
}
