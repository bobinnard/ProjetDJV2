using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LaunchRoundButton : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image buttonSprite;
    
    public void OnRoundLaunch()
    {
        if(GameManager.Instance.isInBuildPhase){
            GameManager.Instance.isInBuildPhase = false;
            GameManager.Instance.LaunchNextRound();
            nameText.enabled = false;
            buttonSprite.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isInBuildPhase == true){
            nameText.enabled = true;
            buttonSprite.enabled = true;
        }
    }
}
