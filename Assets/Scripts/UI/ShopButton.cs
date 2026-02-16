using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private TurretInfo info;
    private static string _selectedButton = "";

    private Button _button;
    private string _turretType;
    private int _turretCost;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _turretType = info.name;
        _turretCost = info.Cost;
    }

    private void Update()
    {
        if (_button.interactable && _selectedButton != _turretType) 
        {
            _button.interactable = false;
        }
    }
}
