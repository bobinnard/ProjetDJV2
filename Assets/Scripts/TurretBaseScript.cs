using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBaseScript : MonoBehaviour
{
    [SerializeField] public TurretInfo info;
    [SerializeField] private GameObject mesh;
    [SerializeField] private GameObject bulletMesh;
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
    IEnumerator AnimateAttack(Transform target)
    {
        var pos = transform.position + new Vector3(0.5f, Mathf.Max(level, 2) * 0.3f + 0.5f, 0.5f);
        var bullet = Instantiate(bulletMesh, pos, Quaternion.identity);
        bullet.SetActive(true);
        bullet.transform.localScale = Vector3.zero;
        float time = 0;
        // appear
        while (time < 0.25f)
        {   
            bullet.transform.localScale = time * 4 * Vector3.one;
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        time = 0;
        // turn faster and faster
        while (time < 0.25f)
        {
            bullet.transform.rotation = Quaternion.AngleAxis(Mathf.Exp(time * 4) - 1, Vector3.up);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        time = 0;
        // attack
        var direction = target.position - bullet.transform.position;
        while (time < 0.5f)
        {
            bullet.transform.rotation = Quaternion.AngleAxis(time*100, Vector3.up);
            bullet.transform.position = pos + direction * time * 2;
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
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
