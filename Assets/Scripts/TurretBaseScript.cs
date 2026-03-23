using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBaseScript : MonoBehaviour
{
    [SerializeField] public TurretInfo info;
    [SerializeField] private GameObject mesh;
    [SerializeField] private GameObject bulletMesh;
    [SerializeField] private GameObject statsMenu;
    protected float range;
    protected int damage;
    protected float attackSpeed;
    protected int Cost;
    protected int level = 0;
    private int[] _upgradeCost;

    // returns bool indicating if upgrade was successful
    public virtual bool Upgrade(){
        // We check whether the player has enough money to upgrade
        if (level < _upgradeCost.Length && GameManager.Instance.money >= _upgradeCost[level]){
            GameManager.Instance.money -= _upgradeCost[level];
            // We upgrade, the heritage manages the stat changes
            level++;
            // We raise the mesh to visually show the upgrade
            mesh.transform.position += new Vector3(0f, 0.25f, 0f);
            return true;
        }
        
        return false;
    }
    
    private void Awake()
    {
        range = info.range;
        damage = info.damage;
        attackSpeed = info.attackSpeed;
        Cost = info.Cost;
        _upgradeCost = info.UpgradeCost;
    }

    // purely visual effect, entirely hard coded
    protected IEnumerator AnimateAttack(Transform target)
    {
        var pos = transform.position + new Vector3(0, Mathf.Max(level, 2) * 0.25f + 0.5f,0);
        var bullet = Instantiate(bulletMesh, pos, Quaternion.identity);
        bullet.SetActive(true);
        bullet.transform.localScale = Vector3.zero;
        // appear
        float time = 0;
        while (time < attackSpeed/4)
        {   
            bullet.transform.localScale = time * 4/attackSpeed * bulletMesh.transform.localScale;
            time += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(attackSpeed/4);
        // attack
        time = 0;
        while (time < attackSpeed/4)
        {
            var direction = target.position - pos;
            bullet.transform.rotation = Quaternion.AngleAxis(time*100, Vector3.up);
            bullet.transform.position = pos + time * 4/attackSpeed * direction;
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(bullet);
        if(target.gameObject.TryGetComponent<EnnemyScript>(out var enemyHp)) enemyHp.TakeDamage(damage);
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
/*
    private void OnMouseEnter()
    {
        statsMenu.SetActive(true);
    }
    
    private void OnMouseExit()
    {
        statsMenu.SetActive(false);
    }
*/
    //--------------------------Testing functions---------------------------

    public bool VerifyValues(int r, int d, float aspeed)
    {
        return r == range && d == damage && aspeed == attackSpeed;
    }
}
