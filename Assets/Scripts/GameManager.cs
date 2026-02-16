using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int money;
    public int score;
    public int multiplier;
    private int roundNumber;

    public int aliveEnnemies;
    [SerializeField] private RoundScript[] rounds;
    [SerializeField] private Vector3 spawnPoint;

    [SerializeField] private Vector3[] path;
    public Vector3 EndingPoint;
    
    private static GameManager instance = null;
    public static GameManager Instance => instance;

    private int ennemyMultiplier = 7;
    private int maxMult = 5;

    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        EndingPoint = path[path.Length-1];
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
        if(aliveEnnemies <= 0 && roundNumber < rounds.Length) LaunchNextRound();
        scoreText.text = "Score: " + score + "\nMultiplier: " + multiplier + "\nMoney: " + money;
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
        if(multiplier > 0){
            multiplier = -1;
            ennemyMultiplier = -1;
        }
        else{
            ennemyMultiplier -= 1;
            if(ennemyMultiplier == -7 && multiplier > -1*maxMult){
                multiplier -= 1;
                ennemyMultiplier = 0;
            }
        }
        score += removedScore*multiplier;
    }

    public void AddScore(int addedScore){
        if(multiplier < 0){
            multiplier = 1;
            ennemyMultiplier = 1;
        }
        else{
            ennemyMultiplier += 1;
            if(ennemyMultiplier == 7 && multiplier < maxMult){
                multiplier += 1;
                ennemyMultiplier = 0;
            }
        }
        score += addedScore*multiplier;
    }
}
