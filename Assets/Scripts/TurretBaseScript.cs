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

    // Update is called once per frame
    void Update()
    {
        
    }
}
