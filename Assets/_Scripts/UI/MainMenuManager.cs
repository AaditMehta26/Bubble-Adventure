using System;
using _Scripts.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]private Button startButton;
    [SerializeField]private Button levelSelectButton;
    [SerializeField]private Button quitButton;

    private void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.uiPopClip);
            SceneController.Instance.LoadScene("L1");
        });

        levelSelectButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.uiPopClip);
            SceneController.Instance.LoadScene("LevelSelect");
        });
        
        quitButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.uiPopClip);
            Application.Quit();
        });
        
    }
}
