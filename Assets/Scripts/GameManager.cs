using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int money;
    public static int score;
    private int multiplier;
    private int roundNumber;

    private int aliveEnnemies;
    [SerializeField] private RoundScript[] rounds;
    [SerializeField] private Vector3 spawnPoint;
    
    void Start()
    {
        money = 0;
        score = 0;
        multiplier = 1;
        roundNumber = 0;
        LaunchNextRound();
    }

    // Update is called once per frame
    void Update()
    {
        if(aliveEnnemies <= 0) LaunchNextRound();
    }

    private void LaunchNextRound()
    {
        GameObject[] ennemies = rounds[roundNumber].ennemieTypes;
        int nbEnnemy = rounds[roundNumber].nbEnnemy;
        float[] ennemyCooldown = rounds[roundNumber].ennemyCooldown;
        for(int i = 0; i<nbEnnemy; i++){
            StartCoroutine(spawnEnnemy(ennemies[(int)ennemyCooldown[2*i]], ennemyCooldown[2*i+1]));
        }
        roundNumber++;
    }

    private IEnumerator spawnEnnemy(GameObject ennemy, float cooldown)
    {
        GameObject instantiatedEnnemy = Instantiate(ennemy, spawnPoint, Quaternion.identity);
        aliveEnnemies++;
        yield return new WaitForSeconds(cooldown);
    }
}
