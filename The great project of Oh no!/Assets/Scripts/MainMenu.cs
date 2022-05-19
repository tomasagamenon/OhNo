using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public List<Button> buttonsMenu;
    public GameObject menuCamera;

    public GameObject playerCamera;
    public PlayableDirector directorPlay;
    public PlayableDirector directorMenuPlay;
    public GameObject play;
    public GameObject[] playerCanvas;

    public PlayableDirector directorOptions;
    public PlayableDirector directorMenuOptions;
    public GameObject options;
    public List<Button> buttonsOptions;

    public PlayableDirector directorCredits;
    public PlayableDirector directorMenuCredits;
    public GameObject credits;
    public List<Button> buttonsCredits;

    public PlayableDirector directorAchievements;
    public PlayableDirector directorMenuAchievements;
    public GameObject achievements;
    public List<Button> buttonsAchievements;


    private void Update()
    {
        if(play.activeSelf)
            if(directorPlay.duration <= directorPlay.time)
            {
                playerCamera.SetActive(true);
                play.SetActive(false);
                menuCamera.SetActive(false);
            }
        if(options.activeSelf)
            if(directorMenuOptions.duration <= directorMenuOptions.time)
            {
                directorMenuOptions.time = directorMenuOptions.initialTime;
                directorMenuOptions.gameObject.SetActive(false);
                options.SetActive(false);
                GoInMenu();
            }
        if (achievements.activeSelf)
            if (directorMenuAchievements.duration <= directorMenuAchievements.time)
            {
                directorMenuAchievements.time = directorMenuAchievements.initialTime;
                directorMenuAchievements.gameObject.SetActive(false);
                achievements.SetActive(false);
                GoInMenu();
            }
        if (credits.activeSelf)
            if (directorMenuCredits.duration <= directorMenuCredits.time)
            {
                directorMenuCredits.time = directorMenuCredits.initialTime;
                directorMenuCredits.gameObject.SetActive(false);
                credits.SetActive(false);
                GoInMenu();
            }
    }


    public void Play()
    {
        play.SetActive(true);
        directorPlay.gameObject.SetActive(true);
        directorPlay.Play(); 
        GoOutMenu();
    }


    public void Options()
    {
        options.SetActive(true);
        directorOptions.gameObject.SetActive(true);
        directorOptions.time = directorOptions.initialTime;
        directorOptions.Play();
        GoOutMenu();
        GoInOptions();
    }
    public void OptionsMenu()
    {
        options.SetActive(true);
        directorOptions.gameObject.SetActive(false);
        directorMenuOptions.gameObject.SetActive(true);
        directorMenuOptions.Play();
        GoOutOptions();
    }


    public void Achievements()
    {
        achievements.SetActive(true);
        directorAchievements.gameObject.SetActive(true);
        directorAchievements.time = directorAchievements.initialTime;
        directorAchievements.Play();
        GoOutMenu();
        GoInAchievements();
    }
    public void AchievementsMenu()
    {
        achievements.SetActive(true);
        directorAchievements.gameObject.SetActive(false);
        directorMenuAchievements.gameObject.SetActive(true);
        directorMenuAchievements.Play();
        GoOutAchievements();
    }


    public void Credits()
    {
        credits.SetActive(true);
        directorCredits.gameObject.SetActive(true);
        directorCredits.time = directorCredits.initialTime;
        directorCredits.Play();
        GoOutMenu();
        GoInCredits();
    }
    public void CreditsMenu()
    {
        credits.SetActive(true);
        directorCredits.gameObject.SetActive(false);
        directorMenuCredits.gameObject.SetActive(true);
        directorMenuCredits.Play();
        GoOutCredits();
    }


    public void Quit()
    {
        Application.Quit();
    }


    public void GoOutMenu()
    {
        foreach (Button button in buttonsMenu)
            button.interactable = false;
    }
    public void GoInMenu()
    {
        foreach (Button button in buttonsMenu)
            button.interactable = true;
    }


    private void GoOutOptions()
    {
        foreach (Button button in buttonsOptions)
            button.interactable = false;
    }
    private void GoInOptions()
    {
        foreach (Button button in buttonsOptions)
            button.interactable = true;
    }


    private void GoOutAchievements()
    {
        foreach (Button button in buttonsAchievements)
            button.interactable = false;
    }
    private void GoInAchievements()
    {
        foreach (Button button in buttonsAchievements)
            button.interactable = true;
    }


    private void GoOutCredits()
    {
        foreach (Button button in buttonsCredits)
            button.interactable = false;
    }
    private void GoInCredits()
    {
        foreach (Button button in buttonsCredits)
            button.interactable = true;
    }
}
