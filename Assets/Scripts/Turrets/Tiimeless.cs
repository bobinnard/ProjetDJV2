using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiimeless : TurretBaseScript
{
    private bool canAttack = true;
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
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }
    public override void Upgrade(){
        if(level < 3)
        {
            damage += 2;
            range += 2;
        }
        else
        {
            damage += 2;
            GameManager.Instance.isSlown++;
            GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Ennemy");
            for(int i = 0; i<ennemies.Length; i++)
            {
                EnnemyScript ennemyhp;
                if(ennemies[i].TryGetComponent<EnnemyScript>(out ennemyhp)) ennemyhp.speed /= 2;
            }  
        } 
    }
}
