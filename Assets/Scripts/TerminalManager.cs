using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalManager : MonoBehaviour
{
	Terminal[] terminalArray = null;
	Camera Camera = null;

	void Start ()
    {
		terminalArray = Object.FindObjectsOfType(typeof(Terminal)) as Terminal[];
		Camera = Object.FindObjectOfType (typeof(Camera)) as Camera;
    }
	
	void Update ()
    {
		for (int i = 0; i < terminalArray.Length; i++) 
		{
			Terminal terminal = terminalArray[i];

			if (terminal.IsPlayerColliding && Input.GetKeyDown(KeyCode.F))
			{
				MoveCameraTo (terminalArray[1]);
			}
		}
	}

	void MoveCameraTo(Terminal terminal)
	{
		Vector3 terminalVec3 = terminal.transform.position;
		terminalVec3.z = Camera.transform.position.z;
		Camera.transform.position = terminalVec3;
		Debug.Log ("X: " + terminalVec3.x + "Y: " + terminalVec3.y + "Z: " + terminalVec3.z);
	}
}
