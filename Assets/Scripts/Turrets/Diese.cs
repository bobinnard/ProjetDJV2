using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diese : TurretBaseScript
{
    private bool isOnCooldown = false;

    private float offersCooldown = 5;
    private bool canOffer = true;
    [SerializeField] private GameObject offerIcon;

    // Update is called once per frame
    void Update()
    {
        if(!isOnCooldown && !GameManager.Instance.isInBuildPhase) StartCoroutine(ProduceMoney());
        if(canOffer && !GameManager.Instance.isInBuildPhase) StartCoroutine(Offers());
    }

    private IEnumerator Offers()
    {
        canOffer = false;
        offerIcon.SetActive(true);
        yield return new WaitForSeconds(offersCooldown);
        canOffer = true;
    }

    private IEnumerator ProduceMoney()
    {
        isOnCooldown = true;
        GameManager.Instance.money += damage;
        yield return new WaitForSeconds(attackSpeed);
        isOnCooldown = false;
    }

    public override void Upgrade(){
        if(level == 1)
        {
            damage += 5;
            attackSpeed -= 1f;
        }
        else if(level == 2)
        {
            damage += 15;
            attackSpeed -= 1f;
        }
        else damage += 100;
    }
}