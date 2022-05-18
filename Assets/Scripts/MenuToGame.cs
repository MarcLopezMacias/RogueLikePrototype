using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuToGame : MonoBehaviour
{
    [SerializeField]
    public Button stoopidButton;

    void Start()
    {
        stoopidButton = gameObject.GetComponent<Button>();
        stoopidButton.onClick.AddListener(TaskOnClick);
    }

    public void GoToInGameScene()
    {
        Debug.Log("Should load Game Scene");
        SceneManager.LoadScene("RogueLikeInGame");
    }

    void TaskOnClick()
    {
        GoToInGameScene();
    }
}
