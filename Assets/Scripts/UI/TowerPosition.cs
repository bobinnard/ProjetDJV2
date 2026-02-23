using UnityEngine;
using UnityEngine.UI;

public class TowerPosition : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private Button button;

    private bool _isTowerMenuActive = false;
    private bool _isOccupied = false;

    private void Start()
    {
        
    }
    
    private void ToggleMenu()
    {
        if (!_isOccupied)
        {
        }
        else _isTowerMenuActive = !_isTowerMenuActive;
        menu.SetActive(_isTowerMenuActive);
        return;
        
    }

    public void BuildTower()
    {
        _isOccupied = true;
    }
}
