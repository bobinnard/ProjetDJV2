using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private float animationDuration;
    [SerializeField] private RectTransform shopPanel;

    private bool _isAnimating;
    private bool _isUp;
    private float _time = 0f;
    
    public void Switch()
    {
        if (_isAnimating)
        {
            return;
        }
        if (_isUp) StartCoroutine(AnimateDown());
        else StartCoroutine(AnimateUp());
    }

    private IEnumerator AnimateUp()
    {
        _time = 0;
        _isAnimating = true;
        while (_time < 1)
        {
            _time += Time.deltaTime / animationDuration;
            float shift = Mathf.SmoothStep(0, 1, _time);
            shopPanel.anchorMax = new Vector2(1, shift * 0.25f);
            shopPanel.anchorMin = new Vector2(0, -0.25f + shift * 0.25f);
            yield return 0;
        }
        _isAnimating = false;
        _isUp = true;
    }
    
    private IEnumerator AnimateDown()
    {
        _time = 0;
        _isAnimating = true;
        while (_time < 1)
        {
            _time += Time.deltaTime / animationDuration;
            float shift = Mathf.SmoothStep(0, 1, _time);
            shopPanel.anchorMax = new Vector2(1, 0.25f - shift * 0.25f);
            shopPanel.anchorMin = new Vector2(0, - shift * 0.25f);
            yield return 0;
        }
        _isAnimating = false;
        _isUp = false;
    }
}
