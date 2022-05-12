using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField]
    private Image HealthBar;
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private Transform gameOverScreen;

    [SerializeField]
    private Text waveNumberText;
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private CombatChannelSO combatChannel;

    private float score = 0;

    private void OnEnable()
    {
        combatChannel.ZombieAttackedEventRaised += OnPlayerDamageTaken;
        combatChannel.IncreaseScoreEventRaised += AddScore;
        combatChannel.NewWaveEventRaised += NewWave;
        combatChannel.GameOverEventRaised += GameOver;

        scoreText.text = "Score: 0";
        waveNumberText.text = "Wave: 1";
    }
    private void OnDisable()
    {
        combatChannel.ZombieAttackedEventRaised -= OnPlayerDamageTaken;
        combatChannel.IncreaseScoreEventRaised -= AddScore;
    }

    private void OnPlayerDamageTaken(float ammount)
    {
        HealthBar.fillAmount = player.playerStats.Health / player.playerStats.MaxHeath;
    }

    private void AddScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    private void NewWave(float waveNumber)
    {
        waveNumberText.text = "Wave: " + waveNumber;
    }

    private void GameOver()
    {
        gameOverScreen.gameObject.SetActive(true);
    }
}
