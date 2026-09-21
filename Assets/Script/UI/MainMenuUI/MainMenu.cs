using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject ButtonsTest;

    void Start()
    {
        ButtonsTest.SetActive(false);
    }

    void Update()
    {
        //Para activar los botones de test para ir a las scenes de test
        #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.T))
            {
                ButtonsTest.SetActive(!ButtonsTest.gameObject.activeSelf);
            }
        #endif
    }

    public void Play(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Exit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}