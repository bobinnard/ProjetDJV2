using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "roundData", menuName = "TD/GenerateEnnemy", order = 1)]
public class RoundScript : ScriptableObject
{
    public int nbEnnemy;
    public GameObject[] ennemieTypes;
    public float[] ennemyCooldown;
}
