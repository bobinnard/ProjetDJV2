using System.Collections;
using UnityEngine;

public class Liien : TurretBaseScript
{
    private bool _canAttack = true;

    private void Update()
    {
        Collider[] nearEnemies = Physics.OverlapSphere(transform.position, range, 8);
        if(_canAttack && nearEnemies.Length > 0) StartCoroutine(Attack(nearEnemies));
    }

    private IEnumerator Attack(Collider[] nearEnemies){
        _canAttack = false;
        float minDist = 9999999;
        int minI = 0;
        for (int i = 0; i<nearEnemies.Length; i++){
            float curDist = Vector3.Distance(nearEnemies[i].gameObject.transform.position, GameManager.Instance.EndingPoint);
            if(curDist < minDist){
                minDist = curDist;
                minI = i;
            }
        }
        transform.LookAt(nearEnemies[minI].gameObject.transform.position);
        if(nearEnemies[minI].gameObject.TryGetComponent<EnnemyScript>(out var enemyHp)) enemyHp.TakeDamage(damage);
        if (level == 3) MultiHit(nearEnemies[minI]);
        yield return new WaitForSeconds(attackSpeed);
        _canAttack = true;
    }

    // if level 3, liien gets triple shot
    private void MultiHit(Collider centralEnemy)
    {
        Collider[] collateralEnemies = Physics.OverlapSphere(centralEnemy.gameObject.transform.position, range, 8);
        if(collateralEnemies.Length == 0) return;
        float minDist1 = 9999999;
        int minI1 = 0;
        float minDist2 = 9999999;
        int minI2 = 0;
        for (int i = 0; i<collateralEnemies.Length; i++){
            float curDist = Vector3.Distance(collateralEnemies[i].gameObject.transform.position, GameManager.Instance.EndingPoint);
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
        if(collateralEnemies.Length == 1)
        {
            if(collateralEnemies[minI1].gameObject.TryGetComponent<EnnemyScript>(out var enemyHp)) enemyHp.TakeDamage(damage);
        }
        else 
        {
            EnnemyScript enemyHp;
            if(collateralEnemies[minI1].gameObject.TryGetComponent<EnnemyScript>(out enemyHp)) enemyHp.TakeDamage(damage);
            if(collateralEnemies[minI2].gameObject.TryGetComponent<EnnemyScript>(out enemyHp)) enemyHp.TakeDamage(damage);
        }
    }

    public override bool Upgrade(){
        if (!base.Upgrade()) return false;
        range += 1;
        damage += 2;
        attackSpeed -= 0.1f;
        return true;
    }
}
