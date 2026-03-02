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

public class SetupTesting
{
    GameObject gameManager;
    // A Test behaves as an ordinary method
    [Test]
    public void SetupTestingSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnitySetUp]
    public IEnumerator GameManagerInstatiation()
    {
        SceneManager.LoadScene("GameTest");
        var request = Addressables.LoadAsset<GameObject>("Assets/Prefab/GameManager.prefab");
        yield return request;
        gameManager = GameObject.Instantiate(request.Result,Vector3.zero,Quaternion.identity);
    }

    [UnityTest]
    public IEnumerator RoundTest()
    {
        var request = Addressables.LoadAsset<GameObject>("Assets/Prefab/Turrets/Liien.prefab");
        yield return request;
        GameObject liien = GameObject.Instantiate(request.Result,Vector3.forward,Quaternion.identity);
        liien.transform.LookAt(Vector3.zero);
        Assert.That(gameManager, Is.Not.Null);
        GameManager.Instance.isInBuildPhase = false;
        GameManager.Instance.LaunchNextRound();
        yield return new WaitForSeconds(10f);
        Assert.That(GameManager.Instance.aliveEnnemies <= 0);
        Assert.That(GameManager.Instance.score >= 0);
        Assert.That(GameManager.Instance.isInBuildPhase == true);
    }
}
