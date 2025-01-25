using System;
using _Scripts.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]private Button startButton;
    [SerializeField]private Button quitButton;

    private void Awake()
    {
        startButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        });
        
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
