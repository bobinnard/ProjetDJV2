using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LeBarC : TurretBaseScript
{
    private bool canBoost = true;
    private bool canKilometre = false;

    // Update is called once per frame
    void Update()
    {
        Collider[] nearTowers = Physics.OverlapSphere(transform.position, range, 64);
        if(canBoost && nearTowers.Length > 0) StartCoroutine(Boost(nearTowers));
        if(canKilometre) StartCoroutine(Kilometre(nearTowers));
    }

    private IEnumerator Kilometre(Collider[] nearTowers){
        canKilometre = false;
        for (int i = 0; i<nearTowers.Length; i++){
            TurretBaseScript turret;
            if(nearTowers[i].TryGetComponent<TurretBaseScript>(out turret))
            {
                turret.setStats("attackSpeed",turret.getStats("attackSpeed")/2);
                nearTowers[i].transform.localScale += new Vector3(0.1f,0.1f,0.1f);
            }
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i<nearTowers.Length; i++){
            TurretBaseScript turret;
            if(nearTowers[i].TryGetComponent<TurretBaseScript>(out turret))
            {
                turret.setStats("attackSpeed",turret.getStats("attackSpeed")*2);
                nearTowers[i].transform.localScale -= new Vector3(0.1f,0.1f,0.1f);   
            }
        }
        yield return new WaitForSeconds(2f);
        canKilometre = true;
    }

    private IEnumerator Boost(Collider[] nearTowers){
        canBoost = false;
        TurretBaseScript turret;
        int boostedTower = Random.Range(0,nearTowers.Length);
        if(nearTowers[boostedTower].gameObject.TryGetComponent<TurretBaseScript>(out turret));
        turret.setStats("damage",turret.getStats("damage")+2);
        nearTowers[boostedTower].transform.localScale += new Vector3(0.1f,0.1f,0.1f);
        yield return new WaitForSeconds(attackSpeed);
        if(level >= 2) yield return new WaitForSeconds(0.5f);
        turret.setStats("damage",turret.getStats("damage")-2);
        nearTowers[boostedTower].transform.localScale -= new Vector3(0.1f,0.1f,0.1f);
        yield return new WaitForSeconds(attackSpeed);
        canBoost = true;
    }

    public override void Upgrade(){
        if(level == 2){
            damage += 1;
            attackSpeed -= 0.5f;
        }
        else{
            if(level == 3) canKilometre = true;
            damage += 2;
        } 
    }
}
