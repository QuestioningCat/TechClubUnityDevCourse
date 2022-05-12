using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private Camera cam;

    private float playerNextAttackTime = 0f;
    private Stats stats;

    [SerializeField]
    private CombatChannelSO combatChannel;

    private void Start()
    {
        cam = Camera.main;

        stats = GetComponent<PlayerController>().playerStats;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > playerNextAttackTime)
        {
            playerNextAttackTime = Time.time + stats.AttackSpeed;
            RaycastHit hit;
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
            {
                if(hit.transform.tag == "Zombie")
                {
                    combatChannel.PlayerAttackedZombie(stats.AttackDamage, hit.transform.GetComponent<ZombieController>());
                }
            }
        }
    }
}
