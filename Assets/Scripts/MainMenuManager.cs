using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    GameObject MenuPage;
    GameObject CreditsPage;
    static public bool impossible = false;

    void Start () {
        MenuPage = GameObject.Find("MenuPage");
        CreditsPage = GameObject.Find("CreditsPage");
        CreditsPage.SetActive(false);
	}


    public void StartButton()
    {
        SceneManager.LoadScene("Main Scene");
    }


    public void ImpossibleButton()
    {
        impossible = true;
        SceneManager.LoadScene("Main Scene");
    }


    public void CreditsButton()
    {
        CreditsPage.SetActive(true);
        MenuPage.SetActive(false);
    }


    public void BackButton()
    {
        MenuPage.SetActive(true);
        CreditsPage.SetActive(false);
    }


    public void QuitButton()
    {
        Application.Quit();
    }
}
