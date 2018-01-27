using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public Scenes.Scene scene;

    // Use this for initialization
    void Start()
    {
        Button startButton = GetComponent<Button>();
        startButton.onClick.AddListener(onClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void onClick()
    {
        Scenes.instance.LoadScene(scene);
    }

}
