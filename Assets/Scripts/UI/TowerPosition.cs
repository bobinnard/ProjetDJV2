using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerPosition : MonoBehaviour
{
    [SerializeField] private GameObject towerMenu;
    [SerializeField] private Button button;

    private bool _isTowerMenuActive;
    private bool _isOccupied;
    private bool _isSelected;

    private void Update()
    {
        if (_isSelected && EventSystem.current.currentSelectedGameObject != button.gameObject) _isSelected = false;
    }
        
    public void ToggleMenu()    
    {
        if (!_isOccupied)
        {
            Debug.Log("1");
            if (_isSelected)
            {
                Debug.Log("2");
                BuildTower();
            }
            else _isSelected = true;
            return;
        }
        Debug.Log("3");
        _isTowerMenuActive = !_isTowerMenuActive;
        towerMenu.SetActive(_isTowerMenuActive);
    }

    private void BuildTower()
    {
        _isOccupied = true;
        var towerToBuild = Instantiate(ShopManager.Instance.towerTypesPrefabs[ShopManager.Instance.selectedTowerType],transform);
        towerToBuild.SetActive(true);
    }
}
