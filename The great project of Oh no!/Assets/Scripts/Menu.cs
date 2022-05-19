using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menuInGame = null;
    [SerializeField] private GameObject journal = null;
    [SerializeField] private GameObject settings = null;

    private GameObject player;

    private bool openJournal;
    private bool openMenu;

    public enum MenuType { Journal, MenuInGame }
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (openMenu)
            {
                ToggleMenu("MenuInGame", false);
                ToggleMenu("Settings", false);
            }
            else ToggleMenu("MenuInGame", true);
        }
        if (Input.GetButtonDown("Journal"))
        {
            if (openMenu)
                ToggleMenu("Journal", false);
            else ToggleMenu("Journal", true);
        }
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ToggleMenu(string menuType, bool open)
    {
        GameObject menu = null;
        if (menuType == "Journal")
            menu = journal;
        else if (menuType == "MenuInGame")
            menu = menuInGame;
        else if (menuType == "Settings")
            menu = settings;

        if (!open)
        {
            menu.SetActive(false);
            openMenu = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player.GetComponent<PlayerController>().enabled = true;
            //player.GetComponent<Gun>().enabled = true;
            player.GetComponentInChildren<PlayerCamera>().enabled = true;
        }
        else if(open)
        {
            menu.SetActive(true);
            openMenu = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            player.GetComponent<PlayerController>().enabled = false;
            //player.GetComponent<Gun>().enabled = false;
            player.GetComponentInChildren<PlayerCamera>().enabled = false;
        }
    }
    public void MenuButton()
    {
        ToggleMenu("MenuInGame", false);
    }

    public void JournalButton()
    {
        ToggleMenu("Journal", false);
    }

    public void Settings()
    {
        ToggleMenu("Settings", false);
    }
}
