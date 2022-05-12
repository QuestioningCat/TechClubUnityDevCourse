using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(menuName ="Scrptable Objects/Combat")]
public class CombatChannelSO : ScriptableObject
{
    public Action<float> ZombieAttackedEventRaised;
    public Action<float> PlayerAttackEventRaised;
    public Action<Transform> ZombieDiedEventRaised;
    public Action IncreaseScoreEventRaised;
    public Action<float> NewWaveEventRaised;
    public Action GameOverEventRaised;


    public void ZombieAttackedPlayer(float ammount)
    {
        ZombieAttackedEventRaised?.Invoke(ammount);
    }

    public void PlayerAttackedZombie(float ammount, ZombieController zombieController)
    {
        
        zombieController.ZombieTakenDamage(ammount);
        PlayerAttackEventRaised?.Invoke(ammount);
    }

    public void ZombieDied(Transform zombie)
    {
        IncreaseScoreEventRaised.Invoke();
        ZombieDiedEventRaised?.Invoke(zombie);
    }

    public void NewWave(float waveNumber)
    {
        NewWaveEventRaised?.Invoke(waveNumber);
    }

    public void GameOver()
    {
        GameOverEventRaised?.Invoke();
    }
}
