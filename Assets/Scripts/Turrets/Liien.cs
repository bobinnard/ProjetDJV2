using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liien : TurretBaseScript
{
    private bool canAttack = true;

    // Update is called once per frame
    void Update()
    {
        Collider[] nearEnnemies = Physics.OverlapSphere(transform.position, range, 8);
        if(canAttack && nearEnnemies.Length > 0) StartCoroutine(Attack(nearEnnemies));
    }

    private IEnumerator Attack(Collider[] nearEnnemies){
        canAttack = false;
        float minDist = 9999999;
        int minI = 0;
        for (int i = 0; i<nearEnnemies.Length; i++){
            float curDist = Vector3.Distance(nearEnnemies[i].gameObject.transform.position, GameManager.Instance.EndingPoint);
            if(curDist < minDist){
                minDist = curDist;
                minI = i;
            }
        }
        transform.LookAt(nearEnnemies[minI].gameObject.transform.position);
        EnnemyScript ennemyHp;
        if(nearEnnemies[minI].gameObject.TryGetComponent<EnnemyScript>(out ennemyHp)) ennemyHp.TakeDamage(damage);
        if (level == 3) MultiHit(nearEnnemies[minI]);
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }

    //if level 3, liien gets triple shot
    private void MultiHit(Collider centralEnnemy)
    {
        Collider[] collateralEnnemies = Physics.OverlapSphere(centralEnnemy.gameObject.transform.position, 3, 8);
        if(collateralEnnemies.Length == 0) return;
        float minDist1 = 9999999;
        int minI1 = 0;
        float minDist2 = 9999999;
        int minI2 = 0;
        for (int i = 0; i<collateralEnnemies.Length; i++){
            float curDist = Vector3.Distance(collateralEnnemies[i].gameObject.transform.position, GameManager.Instance.EndingPoint);
            if(curDist < minDist1){
                minDist2 = minDist1;
                minI2 = minI1;
                minDist1 = curDist;
                minI1 = i;
            }
            else if(curDist < minDist2){
                minDist2 = curDist;
                minI2 = i;
            }
        }
        if(collateralEnnemies.Length == 1)
        {
            EnnemyScript ennemyHP;
            if(collateralEnnemies[minI1].gameObject.TryGetComponent<EnnemyScript>(out ennemyHP)) ennemyHP.TakeDamage(damage);
        }
        else 
        {
            EnnemyScript ennemyHP;
            if(collateralEnnemies[minI1].gameObject.TryGetComponent<EnnemyScript>(out ennemyHP)) ennemyHP.TakeDamage(damage);
            if(collateralEnnemies[minI2].gameObject.TryGetComponent<EnnemyScript>(out ennemyHP)) ennemyHP.TakeDamage(damage);
        }
    }

    public override void Upgrade(){
        range += 1;
        damage += 2;
        attackSpeed -= 0.1f;
    }
}
