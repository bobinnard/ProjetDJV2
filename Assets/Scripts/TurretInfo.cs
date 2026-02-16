using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "roundData", menuName = "TD/Turret", order = 2)]
public class TurretInfo : ScriptableObject
{
    public float range;
    public int damage;
    public float attackSpeed;
    public int Cost;
    public int[] UpgradeCost;
}
