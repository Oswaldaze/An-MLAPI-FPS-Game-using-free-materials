using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;

public class HealthNet : NetworkBehaviour
{
    PlayerSpawn playerspawaner;
    public Image healthimage;
    public Image healthbar;
    public static int maxhealth = 100;
    public bool immune;

    [SerializeField]
    NetworkVariableInt health = new NetworkVariableInt(new NetworkVariableSettings { WritePermission = NetworkVariablePermission.Everyone }, maxhealth);

    // Start is called before the first frame update
    void Start()
    {
        playerspawaner = GetComponent<PlayerSpawn>();
        immune = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.Value <= 0 && IsLocalPlayer)
        {
            GetComponent<WinManager>().lives -= 1;
            health.Value = maxhealth;
            playerspawaner.Respawn();
        }
        if (IsLocalPlayer)
        {
            HealthserverRPC();
        }
    }
    public void TakeDamage(int damage)
    {
        if (!immune)
        {
            health.Value -= damage;
            //HealthserverRPC();
        }
    }
    [ServerRpc]
    void HealthserverRPC()
    {
        HealthclientRPC();
    }
    [ClientRpc]
    void HealthclientRPC()
    {
        healthimage.transform.localScale = new Vector3(Convert.ToSingle(Convert.ToDouble(health.Value) / Convert.ToDouble(maxhealth)), 1, 1);
        healthbar.transform.localScale = new Vector3(Convert.ToSingle(Convert.ToDouble(health.Value) / Convert.ToDouble(maxhealth)), 1, 1);
    }
}
