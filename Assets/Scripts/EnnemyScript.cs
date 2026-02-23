using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyScript : MonoBehaviour
{
    public Vector3[] path;
    [SerializeField] private CharacterController chara;
    private int currentTarget;
    private float Error = 0.4f;
    public float speed;
    public int score;
    public int reward;
    public int hp;

    private bool isDead = false;

    void Start()
    {
        currentTarget = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(path[currentTarget], transform.position) <= Error) 
        {
            currentTarget++;
        }
        if(currentTarget == path.Length) DealDamage();
        else
        {
            transform.LookAt(path[currentTarget]);
            chara.Move(transform.forward*speed*Time.deltaTime);
        }
    }

    public void TakeDamage(int damage){
        hp -= damage;
        if(hp <= 0 && !isDead) Die();
    }

    private void DealDamage(){
        GameManager.Instance.RemoveScore(score);
        GameManager.Instance.aliveEnnemies --;
        Destroy(gameObject);
    }

    private void Die(){
        isDead = true;
        GameManager.Instance.AddScore(score);
        GameManager.Instance.money += reward;
        GameManager.Instance.aliveEnnemies --;
        Destroy(gameObject);
    }
}
