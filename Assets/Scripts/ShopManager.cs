using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // Toggle group which's gameObject will be the parent of the toggles
    [SerializeField] private ToggleGroup toggleGroup;
    
    // List of all tower prefabs
    public GameObject[] towerTypesPrefabs;
    
    // Index of the selected tower type
    public int selectedTowerType;
    
    // Prefab for the shop buttons
    [SerializeField] private GameObject shopUITowerTypeTogglePrefab;
    
    // Variables to make the ShopManager singleton
    private static ShopManager _instance;
    public static ShopManager Instance => _instance;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        SetupShop();
    }

    // Setups the shop UI toggles from the tower prefab list
    private void SetupShop()
    {
        for (var i = 0; i < towerTypesPrefabs.Length; i++)
        {
            // Set up the tower information
            var toggle = Instantiate(shopUITowerTypeTogglePrefab, toggleGroup.gameObject.transform).GetComponentInChildren<Toggle>();
            toggle.gameObject.name = i.ToString();
            var towerInfo = towerTypesPrefabs[i].GetComponentInChildren<TurretBaseScript>().info;
            toggle.GetComponentInChildren<TMP_Text>().text = towerInfo.name + " - " + towerInfo.Cost + " gold";
            toggle.group = toggleGroup;
            
            // Add a listener to each toggle to make them work
            toggle.onValueChanged.AddListener(state =>
            {
                if (!state) return;
                selectedTowerType = int.Parse(toggle.name);
            });
            
            // Set the first toggle on by default
            if (i==0) toggle.isOn = true;
            
            toggle.gameObject.SetActive(true);
        }
    }
}
