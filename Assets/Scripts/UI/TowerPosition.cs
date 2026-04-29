using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerPosition : MonoBehaviour
{
    // The UI to upgrade or delete the tower
    [SerializeField] private GameObject towerMenu;
    // Main button that indicates the tower position
    [SerializeField] private Button button;
    
    // whether a tower is built here 
    private bool _isOccupied;
    // the tower that is built
    private TurretBaseScript _tower;
    // whether the main button is selected, used to ask confirmation on tower placement
    private bool _isSelected;
    // placement whose upgrade/delete UI is shown, used to make sure only one UI is shown at once.
    private static TowerPosition _selectedButton;

    private void Update()
    {
        if (_isSelected && EventSystem.current.currentSelectedGameObject != button.gameObject) _isSelected = false;
    }
        
    public void ToggleMenu()    
    {
        // If no turret is built on this position, the player needs to double press the position to build one
        if (!_isOccupied)
        {
            if (_isSelected)
            {
                _isSelected = false;
                BuildTower();
            }
            else _isSelected = true;
            return;
        }

        // If a turret is built, we deactivate the currently shown UI and show the UI of this placement
        if (_selectedButton == this)
        {
            _selectedButton = null;
            towerMenu.SetActive(false);
            return;
        }
        if (_selectedButton) _selectedButton.ToggleMenu();
        towerMenu.SetActive(true);
        _selectedButton = this;
    }

    private void BuildTower()
    {
        _isOccupied = true;
        _tower = Instantiate(
                    ShopManager.Instance.towerTypesPrefabs[ShopManager.Instance.selectedTowerType],
                    transform)
                .GetComponent<TurretBaseScript>();
        _tower.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void DestroyTower()
    {
        ToggleMenu();
        _isOccupied = false;
        Destroy(_tower.gameObject);
    }

    public void UpgradeTower()
    {
        if (_tower.Upgrade()) Debug.Log("Tower upgraded");
        else Debug.Log("Tower not upgraded");
    }
}
