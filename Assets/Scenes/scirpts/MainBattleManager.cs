using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class MainBattleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Modeloading").GetComponent<ModeStorage>().ARMode)
        {
            print("AR");
        }
        if (GameObject.Find("Modeloading").GetComponent<ModeStorage>().OnlineMode)
        {

            GetComponent<BattleManager>().setIP(GameObject.Find("Modeloading").GetComponent<ModeStorage>().HostIpAddress);
            if (GameObject.Find("Modeloading").GetComponent<ModeStorage>().Host) 
                GetComponent<BattleManager>().StartServer();
            else
                GetComponent<BattleManager>().StartClient();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
