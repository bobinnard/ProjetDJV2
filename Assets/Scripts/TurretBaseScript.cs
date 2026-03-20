using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBaseScript : MonoBehaviour
{
    [SerializeField] private TurretInfo info;
    protected float range;
    protected int damage;
    protected float attackSpeed;
    protected int Cost;
    protected int[] UpgradeCost;
    protected int level = 0;

    public virtual void Upgrade(){
        if(GameManager.Instance.money >= UpgradeCost[level] && level < UpgradeCost.Length){
            GameManager.Instance.money -= UpgradeCost[level];
            level++;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        range = info.range;
        damage = info.damage;
        attackSpeed = info.attackSpeed;
        Cost = info.Cost;
        UpgradeCost = info.UpgradeCost;
    }

    public void setStats(string stat, float set)
    {
        if(stat == "attackSpeed") attackSpeed = set;
        if(stat == "damage") damage = (int)set;
        if(stat == "range") range = (int)set;
    }

    public float getStats(string stat)
    {
        if(stat == "attackSpeed") return attackSpeed;
        if(stat == "damage") return damage;
        if(stat == "range") return range;
        else return -1;
    }

    //--------------------------Testing functions---------------------------

    public bool VerifyValues(int r, int d, float aspeed)
    {
        return(r == range && d == damage && aspeed == attackSpeed);
    }
}
