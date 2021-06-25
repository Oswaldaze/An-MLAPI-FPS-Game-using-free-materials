using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Serialization;

public class BulletNet : NetworkBehaviour
{
    public Collider bulletcollider;
    public Rigidbody bulletrigidbody;
    public int TotalTime = 20;
    public int damage;
    [SerializeField]
    GameObject healthhit;
    public bool isCollider(Collider collider)
    {
        if(bulletcollider == collider)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool isRigidbody(Rigidbody rigidbody)
    {
        if (bulletrigidbody == rigidbody)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void Start()
    {
        StartCoroutine(Time());
    }
    private void Update()
    {
        if (TotalTime == 0)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Time()
    {
        while (TotalTime >= 0)
        {
            yield return new WaitForSeconds(1);
            TotalTime--;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        healthhit = other.gameObject; 
        var enemyHealth = healthhit.transform.parent.GetComponent<HealthNet>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
        if(!other.gameObject.transform.parent.GetComponent<BulletNet>())
            Destroy(gameObject);

    }

}
