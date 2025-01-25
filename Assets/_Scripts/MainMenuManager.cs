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
            AudioManager.Instance.PlaySound(AudioManager.Instance.uiPopClip);
            SceneController.Instance.NextLevel();
        });
        
        quitButton.onClick.AddListener(Application.Quit);
    }
}
