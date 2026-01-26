using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private KeyCode toggleKey = KeyCode.M; // Key to open/close menu

    private bool isMenuOpen = false;

    void Start()
    {
        if (menuPanel != null)
            menuPanel.SetActive(isMenuOpen);
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        if (menuPanel == null) return;

        isMenuOpen = !isMenuOpen;
        menuPanel.SetActive(isMenuOpen);

    }

    public void OpenMenu()
    {
        isMenuOpen = true;
        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
            
        }
    }

    public void CloseMenu()
    {
        isMenuOpen = false;
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
    
        }
    }
}
