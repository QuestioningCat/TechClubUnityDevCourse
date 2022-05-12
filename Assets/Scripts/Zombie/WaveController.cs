using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{

    public List<Transform> SpawnPoints;

    public GameObject ZombiePrefab;

    public int FirstWaveZombieCount = 5;
    public int WaveNumber = 1;
    public float WaveDifficultyMult = 1.2f;

    private List<GameObject> zombiesInCurrentWave;

    [SerializeField]
    private CombatChannelSO combatChannel;

    private void OnEnable() { combatChannel.ZombieDiedEventRaised += ZombieDied; }
    private void OnDisable() { combatChannel.ZombieDiedEventRaised -= ZombieDied; }

    private void Start()
    {
        zombiesInCurrentWave = new List<GameObject>();
        SpawnWave(5);
    }

    private void Update()
    {
        if(zombiesInCurrentWave.Count <= 0)
        {
            SpawnWave(Mathf.RoundToInt(FirstWaveZombieCount * WaveDifficultyMult));
            WaveDifficultyMult += 0.2f;
            WaveNumber++;
            combatChannel.NewWave(WaveNumber);
        } 
    }

    private void SpawnWave(int ammount)
    {
        for(int i = 0; i < ammount; i++)
        {
            int random = Random.Range(0, SpawnPoints.Count > 0 ? SpawnPoints.Count : -1);
            if (random >= 0 )
            {
                GameObject zombieGameObject = Instantiate(ZombiePrefab, SpawnPoints[random].position, Quaternion.identity);
                zombieGameObject.transform.parent = this.transform;
                zombiesInCurrentWave.Add(zombieGameObject);
            }
        }
    }

    private void ZombieDied(Transform zombie)
    {
        zombiesInCurrentWave.Remove(zombie.gameObject);
        Destroy(zombie.gameObject);
    }
}
