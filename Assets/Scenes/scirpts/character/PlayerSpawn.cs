using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class PlayerSpawn : NetworkBehaviour
{
    HealthNet hn;
    CharacterController cc;
    public GameObject[] renderers;
    public Behaviour[] scripts;
    public ParticleSystem deathParticles;
    void Start()
    {
        hn= GetComponent<HealthNet>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsLocalPlayer && Input.GetKeyDown(KeyCode.Y))
        {
            Respawn();
        }

    }
    public void Respawn()
    {
        RespawnServerRPC();
    }
    [ServerRpc]
    void RespawnServerRPC()
    {
        var r = Random.Range(0, 3);
        if (r==0)
            RespawnClientRPC(GameObject.Find("RevivePos").transform.position);
        else if (r == 1)
            RespawnClientRPC(GameObject.Find("RevivePos1").transform.position);
        else if (r == 2)
            RespawnClientRPC(GameObject.Find("RevivePos2").transform.position);
        else if (r == 3)
            RespawnClientRPC(GameObject.Find("RevivePos3").transform.position);
    }
    [ClientRpc]
    void RespawnClientRPC(Vector3 spawnPos)
    {
        StartCoroutine(RespawnCorountine(spawnPos));
    }
    IEnumerator RespawnCorountine(Vector3 spawnPos)
    {
        hn.immune = true;
        Instantiate(deathParticles, transform.position, transform.rotation);
        cc.enabled = false;
        SetPlayerState(false);
        transform.position = spawnPos;
        yield return new WaitForSeconds(3f);
        cc.enabled = true;
        SetPlayerState(true);
        hn.immune = false;
    }
    void SetPlayerState(bool state)
    {
        foreach(var script in scripts)
        {
            script.enabled = state;
        }
        foreach(var renderer in renderers)
        {
            renderer.SetActive(state);
        }
    }
}
