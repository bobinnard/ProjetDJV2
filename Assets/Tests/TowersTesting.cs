using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using System.Reflection;


public class TowersTesting
{
    GameObject gameManager;

    [UnitySetUp]
    public IEnumerator GameManagerInstatiation()
    {
        SceneManager.LoadScene("GameTest");
        var request = Addressables.LoadAsset<GameObject>("Assets/Prefab/GameManager.prefab");
        yield return request;
        gameManager = GameObject.Instantiate(request.Result,Vector3.zero,Quaternion.identity);
    }
    
    [UnityTest]
    public IEnumerator LiienTest()
    {
        var request = Addressables.LoadAsset<GameObject>("Assets/Prefab/Turrets/Liien.prefab");
        yield return request;
        GameObject liien = GameObject.Instantiate(request.Result,Vector3.forward,Quaternion.identity);
        liien.transform.LookAt(Vector3.zero);
        Liien liienCode;
        if(liien.TryGetComponent<Liien>(out liienCode))
        {
            Assert.That(liienCode.VerifyValues(3,3,1));
            liienCode.Upgrade();
            Assert.That(liienCode.VerifyValues(4,5,0.9f));
            liienCode.Upgrade();
            Assert.That(liienCode.VerifyValues(5,7,0.8f));
            liienCode.Upgrade();
            Assert.That(liienCode.VerifyValues(6,9,0.7f));
        } 
        GameManager.Instance.isInBuildPhase = false;
        GameManager.Instance.LaunchNextRound();
        yield return new WaitForSeconds(10f);
        Assert.That(GameManager.Instance.aliveEnnemies <= 0);
        Assert.That(GameManager.Instance.score >= 0);
        Assert.That(GameManager.Instance.isInBuildPhase == true);
    }
}
