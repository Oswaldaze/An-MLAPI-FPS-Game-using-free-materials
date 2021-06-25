using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;

public class Shoot : NetworkBehaviour
{
    public GameObject bullet;
    public int speed;
    public Rigidbody sbullet;
    public Transform gunpos;
    public Image bulletbar;
    public static int maxbullet=10000;
    public int shootspeed;
    public AudioSource shoot;
    public AudioSource hover;
    [SerializeField]
    NetworkVariableInt bulletnum = new NetworkVariableInt(new NetworkVariableSettings { WritePermission = NetworkVariablePermission.Everyone }, maxbullet);

    void Start()
    {
        bulletnum.Value = 5000;
        hover.loop = true;
        hover.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            if (Input.GetButtonDown("Fire1")&& bulletnum.Value>=100)
            {
                ShootserverRPC();
                shoot.Play();
            }
            if (Input.GetAxis("Mouse ScrollWheel")!=0f && bulletnum.Value >= 100)
            {
                ShootserverRPC();
                shoot.Play();

            }
        }
        if(bulletnum.Value<= maxbullet)
            bulletnum.Value= bulletnum.Value+2;
        bulletbar.transform.localScale = new Vector3(Convert.ToSingle(Convert.ToDouble(bulletnum.Value) / Convert.ToDouble(maxbullet)),1, 1);
    }

    [ServerRpc]
    void ShootserverRPC()
    {
        if(Physics.Raycast(gunpos.position, gunpos.forward, out RaycastHit hit, 10f))
        {
            var enemyHealth = hit.transform.parent.GetComponent<HealthNet>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(10);
            }
        }

        ShootclientRPC();
    }
    [ClientRpc]
    void ShootclientRPC()
    {
        //var bullets = Instantiate(bullet,gunpos.position, Quaternion.identity);
        //GameObject.Find("Weapon_EyeLazers").GetComponent<Unity.FPS.Game.WeaponController>().HandleShootInputs(false, true, false);
       /* var bullets = Instantiate(bullet, gunpos.position, Quaternion.LookRotation(gunpos.forward));

        if (Physics.Raycast(gunpos.position,gunpos.forward,out RaycastHit hit,10f))
        {
            bullets.transform.position=hit.point;
        }
        else
        {
            bullets.transform.position = gunpos.position + (gunpos.forward * 20f);
        }
       */
        Rigidbody bulletCopy = (Rigidbody)Instantiate(sbullet, gunpos.position+ gunpos.forward* Convert.ToSingle(2.9), Quaternion.LookRotation(gunpos.forward));
        bulletCopy.velocity = bulletCopy.transform.TransformDirection(Vector3.forward * speed);
        bulletnum.Value= bulletnum.Value - 100;
        print(bulletnum.Value);
        

    }
}
