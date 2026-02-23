using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PXL : TurretBaseScript
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
        if(level == 3) StartCoroutine(Stun(ennemyHp));
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }

    private IEnumerator Stun(EnnemyScript ennemyHp)
    {   
        float ennemySpeed = ennemyHp.speed;
        ennemyHp.speed = 0;
        yield return new WaitForSeconds(1);
        ennemyHp.speed = ennemySpeed;
    }
    
    public override void Upgrade(){
        if(level < 3)
        {
            damage *= 2;
            attackSpeed -= 0.1f;
        }
        else
        {
            range *= 2;
        }
    }
}
