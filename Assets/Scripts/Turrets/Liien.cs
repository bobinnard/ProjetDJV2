using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liien : TurretBaseScript
{
    [SerializeField] private GameObject bullet;
    private bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        //EnnemyScript chosenEnnemy;
        //if(nearEnnemies[minI].TryGetComponent<EnnemyScript>(out chosenEnnemy)) chosenEnnemy.hp -= damage;
        Destroy(nearEnnemies[minI].gameObject);
        if(canAttack == false) yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }

    public override void Upgrade(){
        range += 1;
        damage *= 2;
        attackSpeed -= 0.1f;
    }
}
