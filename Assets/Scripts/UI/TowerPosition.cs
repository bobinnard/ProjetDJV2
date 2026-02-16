using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPosition : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    private bool _isMenuActive = false;
    private bool _isOccupied = false;
    
    private void ToggleMenu()
    {
        _isMenuActive = !_isMenuActive;
        menu.SetActive(_isMenuActive);
    }

    public void BuildTower()
    {
        _isOccupied = true;
    }
}
