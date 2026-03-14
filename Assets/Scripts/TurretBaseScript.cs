using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBaseScript : MonoBehaviour
{
    [SerializeField] public TurretInfo info;
    [SerializeField] private GameObject mesh;
    protected float range;
    protected int damage;
    protected float attackSpeed;
    protected int Cost;
    protected int[] UpgradeCost;
    protected int level = 0;

    // returns bool indicating if upgrade was successful
    public virtual bool Upgrade(){
        // We check whether the player has enough money to upgrade
        if (level < UpgradeCost.Length && GameManager.Instance.money >= UpgradeCost[level]){
            GameManager.Instance.money -= UpgradeCost[level];
            // We upgrade, the heritage manages the stat changes
            level++;
            // We raise the mesh to visually show the upgrade
            mesh.transform.position += new Vector3(0f, 0.25f, 0f);
            return true;
        }
        
        return false;
    }
    
    void Awake()
    {
        range = info.range;
        damage = info.damage;
        attackSpeed = info.attackSpeed;
        Cost = info.Cost;
        UpgradeCost = info.UpgradeCost;
    }
}
