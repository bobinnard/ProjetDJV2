using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int money;
    public int score;
    public int multiplier;
    private int roundNumber;

    private int aliveEnnemies;
    [SerializeField] private RoundScript[] rounds;
    [SerializeField] private Vector3 spawnPoint;
    private bool canSpawn = true;

    [SerializeField] private Vector3[] path;
    
    private static GameManager instance = null;
    public static GameManager Instance => instance;

    private int ennemyMultiplier = 7;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

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
        StartCoroutine(spawnEnnemy(ennemies, ennemyCooldown, nbEnnemy, 0));
        roundNumber++;
    }

    private IEnumerator spawnEnnemy(GameObject[] ennemieTypes, float[] ennemyCooldowns, int remainingEnnemies, int curEnnemy)
    {
        EnnemyScript myEnnemy;
        GameObject instantiatedEnnemy = Instantiate(ennemieTypes[(int)ennemyCooldowns[2*curEnnemy]], spawnPoint, Quaternion.identity);
        if(instantiatedEnnemy.TryGetComponent<EnnemyScript>(out myEnnemy)) myEnnemy.path = path;
        aliveEnnemies++;
        yield return new WaitForSeconds(ennemyCooldowns[2*curEnnemy+1]);
        if(remainingEnnemies > 1) StartCoroutine(spawnEnnemy(ennemieTypes, ennemyCooldowns, remainingEnnemies-1, curEnnemy+1));
    }

    public void RemoveScore(int removedScore){
        ennemyMultiplier -= 1;
        if(multiplier > 0) multiplier = -1;
        else if(ennemyMultiplier <= 0)
        {
            multiplier -= 1;
            ennemyMultiplier = 7;
        }
        score += removedScore*multiplier;
    }
}
