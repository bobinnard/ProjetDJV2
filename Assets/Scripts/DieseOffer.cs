using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieseOffer : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }

    public void OnClick()
    {
        GameManager.Instance.money += 100;
        gameObject.SetActive(false);
    }
}
