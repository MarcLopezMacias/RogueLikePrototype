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

    void TaskOnClick()
    {
        GameManager.Instance.GoToGame();
    }
}
