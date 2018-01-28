using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class LevelExit : MonoBehaviour
{
    public string _mNextScene;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "DataPacket")
        {
            GameManager.Instance().GetSoundManager().PlaySoundEffect("Sounds/Door and Switch/Door", false);
            GameManager.Instance().ResetListOfEntities();
			SceneManager.LoadScene(_mNextScene);
        }
    }
}
