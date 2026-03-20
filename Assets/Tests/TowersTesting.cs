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
        GameManager.Instance.money += 1000;
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
        request = Addressables.LoadAsset<GameObject>("Assets/Prefab/Ennemies/Ennemy3.prefab");
        yield return request;
        GameObject ennemy1 = GameObject.Instantiate(request.Result,liien.transform.position+new Vector3(1,0,0),Quaternion.identity);
        GameObject ennemy2 = GameObject.Instantiate(request.Result,liien.transform.position+new Vector3(1,0,0),Quaternion.identity);
        GameObject ennemy3 = GameObject.Instantiate(request.Result,liien.transform.position+new Vector3(1,0,0),Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Assert.That(GameManager.Instance.aliveEnnemies <= -3);
    }

    [UnityTest]
    public IEnumerator PXLTest()
    {
        var request = Addressables.LoadAsset<GameObject>("Assets/Prefab/Turrets/PXL.prefab");
        yield return request;
        GameObject pxl = GameObject.Instantiate(request.Result,Vector3.forward,Quaternion.identity);
        pxl.transform.LookAt(Vector3.zero);
        PXL pixelCode;
        GameManager.Instance.money += 1000;
        if(pxl.TryGetComponent<PXL>(out pixelCode))
        {
            Assert.That(pixelCode.VerifyValues(1,5,1));
            pixelCode.Upgrade();
            Assert.That(pixelCode.VerifyValues(1,10,0.8f));
            pixelCode.Upgrade();
            Assert.That(pixelCode.VerifyValues(1,20,0.6f));
            pixelCode.Upgrade();
            Assert.That(pixelCode.VerifyValues(2,20,0.6f));
        } 
        GameManager.Instance.isInBuildPhase = false;
        for (int i = 0; i<5; i++){
            GameManager.Instance.LaunchNextRound();
            yield return new WaitForSeconds(10f);
            Assert.That(GameManager.Instance.aliveEnnemies <= 0);
            Assert.That(GameManager.Instance.score >= 0);
            Assert.That(GameManager.Instance.isInBuildPhase == true);
        }
        request = Addressables.LoadAsset<GameObject>("Assets/Prefab/Ennemies/Ennemy3.prefab");
        yield return request;
        GameObject ennemy1 = GameObject.Instantiate(request.Result,pxl.transform.position+new Vector3(1,0,0),Quaternion.identity);
        GameObject ennemy2 = GameObject.Instantiate(request.Result,pxl.transform.position+new Vector3(1,0,0),Quaternion.identity);
        GameObject ennemy3 = GameObject.Instantiate(request.Result,pxl.transform.position+new Vector3(1,0,0),Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Assert.That(GameManager.Instance.aliveEnnemies <= -3);
    }
}
