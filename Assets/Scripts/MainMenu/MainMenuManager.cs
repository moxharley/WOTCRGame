using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenu;
    [Space]

    [Header("Menus")]
    [SerializeField] GameObject[] menus;
    [Space]

    [Header("Back Button")]
    [SerializeField] GameObject backButton;

    void Start()
    {
        ShowOnly(mainMenu);
    }

    public void OpenMenu(GameObject menu)
    {
        if (!menu) return;

        ShowOnly(menu);
    }

    public void GoBack()
    {
        ShowOnly(mainMenu);
    }

    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }

    private void ShowOnly(GameObject menu)
    {
        if (menu == mainMenu)
        {
            mainMenu.SetActive(true);
            backButton.SetActive(false);
        }
        else {
            mainMenu.SetActive(false);
        }

        foreach (GameObject m in menus)
        {
            if (m == menu)
            {
                m.SetActive(true);
                backButton.SetActive(true);
            }
            else
            {
                m.SetActive(false);
            }
        }
    }
}
