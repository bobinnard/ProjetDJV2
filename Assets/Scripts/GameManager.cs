using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //values to show to the player, its money count, int current score, its score multiplier
    public int money;
    public int score;
    public int multiplier;
    //The round the player is on
    private int roundNumber;

    //Amount of ennemies currently alive in the current round
    public int aliveEnnemies;
    //List of all rounds going to be played
    [SerializeField] private RoundScript[] rounds;
    //The coordinates of where ennemies spawn
    [SerializeField] private Vector3 spawnPoint;
    //A bool to make sure rounds launch one by one
    private bool canLaunch = true;

    //List of points ennemies need to reach on the map
    [SerializeField] private Vector3[] path;
    //Coordinates of the point ennemies reach last
    public Vector3 EndingPoint;
    
    //Variables to make the GameManager singleton
    private static GameManager instance = null;
    public static GameManager Instance => instance;

    //Variable to count how many ennemies are dead to modify the multiplier every 7 dead ennemies
    private int ennemyMultiplier = 7;
    //The maximum the mult can reach
    private int maxMult = 5;

    //Where we display the score
    [SerializeField] private TMP_Text scoreText;

    //Bool describing if we are in build phase or battle phase
    public bool isInBuildPhase = true;

    //Int indicating how many third level Tiimeless are in effect
    public int isSlown;

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
        GoToBuildPhase();
    }

    // Update is called once per frame
    void Update()
    {
        if(aliveEnnemies <= 0 && roundNumber < rounds.Length && canLaunch && !isInBuildPhase) GoToBuildPhase();
        scoreText.text = "Score: " + score + "\nMultiplier: " + multiplier + "\nMoney: " + money;
    }

    private void GoToBuildPhase()
    {
        isInBuildPhase = true;
    }

    public void LaunchNextRound()
    {
        canLaunch = false;
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
        if(instantiatedEnnemy.TryGetComponent<EnnemyScript>(out myEnnemy))
        {
            myEnnemy.path = path;
            for (int i = 0; i<isSlown; i++)
            {
                myEnnemy.speed /= 2;
            }
        } 
        aliveEnnemies++;
        yield return new WaitForSeconds(ennemyCooldowns[2*curEnnemy+1]);
        if(remainingEnnemies > 1) StartCoroutine(spawnEnnemy(ennemieTypes, ennemyCooldowns, remainingEnnemies-1, curEnnemy+1));
        else canLaunch = true;
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
