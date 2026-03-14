using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diese : TurretBaseScript
{
    private bool _isOnCooldown = false;

    private float _offersCooldown = 5;
    private bool _canOffer = true;
    [SerializeField] private GameObject offerIcon;

    // Update is called once per frame
    void Update()
    {
        if(!_isOnCooldown && !GameManager.Instance.isInBuildPhase) StartCoroutine(ProduceMoney());
        if(_canOffer && !GameManager.Instance.isInBuildPhase) StartCoroutine(Offers());
    }

    private IEnumerator Offers()
    {
        _canOffer = false;
        offerIcon.SetActive(true);
        yield return new WaitForSeconds(_offersCooldown);
        _canOffer = true;
    }

    private IEnumerator ProduceMoney()
    {
        _isOnCooldown = true;
        GameManager.Instance.money += damage;
        yield return new WaitForSeconds(attackSpeed);
        _isOnCooldown = false;
    }

    public override bool Upgrade(){
        if (!base.Upgrade()) return false;
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
        return true;
    }
}