using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySwap : MonoBehaviour
{

    public GameObject Claus;
    public GameObject Handgun;
    public GameObject Gun;
    public Camera cameragun;
    public Camera camerabody;
    public bool isActive;

    public void SetActive(bool isActive)
    {
        if (!isActive) 
        {
            foreach (Transform claus in Claus.GetComponentsInChildren<Transform>())
            {      
                claus.gameObject.layer = 0;
                (GetComponent("FPSControl") as MonoBehaviour).enabled=false;
                (Gun.GetComponent("HandgunScriptLPFP") as MonoBehaviour).enabled = false;
                cameragun.enabled = false;
                camerabody.enabled = false;
            }
        }
        else
        {
            foreach (Transform claus in Claus.GetComponentsInChildren<Transform>())
            {
                claus.gameObject.layer = 31;
                (GetComponent("FPSControl") as MonoBehaviour).enabled = true;
                (Gun.GetComponent("HandgunScriptLPFP") as MonoBehaviour).enabled = true;
                cameragun.enabled = true;
                camerabody.enabled = true;
            }
        }
    }
    void Update()
    {
        SetActive(isActive);
    }
}
