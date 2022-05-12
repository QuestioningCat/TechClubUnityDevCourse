using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private NavMeshAgent agent;

    private Stats stats;

    private float distanceToTarget = 0;
    private float timeForNextAttack = 0;

    private bool GamePlayState = true;

    [SerializeField]
    private CombatChannelSO combatChannel;


    private void OnEnable()
    {
        combatChannel.GameOverEventRaised += GameOver;
    }
    private void OnDisable()
    {
        combatChannel.GameOverEventRaised -= GameOver;
    }
    private void OnDestroy()
    {
        combatChannel.GameOverEventRaised -= GameOver;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;

        stats = new Stats(100);
    }

    private void Update()
    {
        if(GamePlayState)
        {
            distanceToTarget = 999;
            if(target)
                distanceToTarget = Vector3.Distance(this.transform.position, target.position);
            else
                Debug.LogError("ERROR:: NO Target Found or Set!!");

            if(distanceToTarget <= 1.6f && Time.time > timeForNextAttack)
            {
                timeForNextAttack = Time.time + stats.AttackSpeed;
                combatChannel.ZombieAttackedPlayer(stats.AttackDamage);
            }

            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        if(distanceToTarget <= 1.5f)
        {
            Quaternion rotationTarget = Quaternion.LookRotation(target.position - this.transform.position);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotationTarget, 15 * Time.deltaTime);
            agent.SetDestination(this.transform.position);
        }
        else
            agent.SetDestination(target.position);
    }

    public void ZombieTakenDamage(float ammount)
    {
        stats.TakeDamage(ammount);
        if(stats.Health <= 0)
        {
            combatChannel.ZombieDied(this.transform);
        }
    }

    private void GameOver()
    {
        GamePlayState = false;
    }
}
