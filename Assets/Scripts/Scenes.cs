using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public enum Scene
    {
        MAIN_MENU = 0,
        GAME = 1,
        GAME_OVER = 2,
    }
    public string[] sceneNames;

    public static Scenes instance;

    private void OnEnable()
    {
        instance = this;
    }

    public string GetSceneName(Scene scene)
    {
        return sceneNames[(int)scene];
    }

    public void LoadScene(Scene scene, LoadSceneMode mode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(GetSceneName(scene), mode);
    }
}