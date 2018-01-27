using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalManager : MonoBehaviour
{
	Terminal[] TerminalArray = null;
	PlayerCameraController CameraController;
    bool FollowPlayerToggle = true;
    int CurrentTerminal = 0;
	UniquePlayerCTRL UniquePlayerCTRL;
		
    void Start ()
    {
		UniquePlayerCTRL = Object.FindObjectOfType (typeof(UniquePlayerCTRL)) as UniquePlayerCTRL;
		TerminalArray = Object.FindObjectsOfType(typeof(Terminal)) as Terminal[];
		CameraController = Object.FindObjectOfType (typeof(PlayerCameraController)) as PlayerCameraController;
    }
	
	void Update ()
    {
		for (int i = 0; i < TerminalArray.Length; i++) 
		{
			Terminal terminal = TerminalArray[i];

			if (terminal.IsPlayerColliding)
			{
				if (Input.GetKeyDown(KeyCode.F)) 
				{
					CurrentTerminal = 0;
					FollowPlayerToggle = !FollowPlayerToggle;
					CameraController.FollowPlayer(FollowPlayerToggle);
				}

				if (Input.GetKeyDown (KeyCode.Comma) && !FollowPlayerToggle) 
				{
					CurrentTerminal -= 1;

					if (CurrentTerminal <= 0) 
					{
						CurrentTerminal = 0;
					}

					MoveCameraTo(TerminalArray[CurrentTerminal]);
				}
				else if (Input.GetKeyDown (KeyCode.Period) && !FollowPlayerToggle) 
				{
					CurrentTerminal += 1;

					if (CurrentTerminal >= TerminalArray.Length)
					{
						CurrentTerminal = TerminalArray.Length - 1;
					}

					MoveCameraTo(TerminalArray[CurrentTerminal]);
				}

				if (Input.GetKeyDown(KeyCode.Space))
				{
					UniquePlayerCTRL.CreateClone(TerminalArray[CurrentTerminal].transform.position);
				}
			}
		}

	}

	void MoveCameraTo(Terminal terminal)
	{
		CameraController.SetTargetPos(terminal.transform.position);
	}
}
