using System;
using System.Collections.Generic;
using _Scripts.Manager;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelsController : MonoBehaviour
{
    [SerializeField] private List<Button> levelButtons;

    private void Start()
    {
        int i = 1;
        foreach (var button in levelButtons)
        {
            string levelName = "L"+i.ToString();
            button.onClick.AddListener(()=>
            {
                print("L"+i);
                SceneController.Instance.LoadScene(levelName);
            });
            i++;
        }
    }
}
