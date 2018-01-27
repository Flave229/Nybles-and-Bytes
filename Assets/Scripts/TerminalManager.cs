using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalManager : MonoBehaviour
{
	PlayerCameraController CameraController;
    bool FollowPlayerToggle = true;
    int CurrentTerminal = 0;
	UniquePlayerCTRL UniquePlayerCTRL;

	public List<TerminalCircuitComponent> TerminalList = new List<TerminalCircuitComponent>();
	List<ICircuitComponents> prevCurrentCircuitComponents = new List<ICircuitComponents>();
	List<ICircuitComponents> nextCurrentCircuitComponents = new List<ICircuitComponents>();

    void Start ()
    {
		UniquePlayerCTRL = Object.FindObjectOfType (typeof(UniquePlayerCTRL)) as UniquePlayerCTRL;
		CameraController = Object.FindObjectOfType (typeof(PlayerCameraController)) as PlayerCameraController;
    }
	
	void Update ()
    {
		if (TerminalList != null) 
		{
			for (int i = 0; i < TerminalList.Count; i++)
			{
				TerminalCircuitComponent terminal = TerminalList [i];

				if (terminal != null) 
				{
					if (terminal.IsPlayerColliding ()) 
					{
						// current is not a clone
						if (Input.GetKeyUp (KeyCode.F))
						{
							PeekAllTerminals (terminal);
							CurrentTerminal = 0;
							FollowPlayerToggle = !FollowPlayerToggle;
							CameraController.FollowPlayer (FollowPlayerToggle);

						}

						if (Input.GetKeyDown (KeyCode.Comma) && !FollowPlayerToggle) 
						{
							CurrentTerminal -= 1;

							if (CurrentTerminal <= 0)
							{
								CurrentTerminal = 0;
							}

							//MoveCameraTo (TerminalList [CurrentTerminal]);

						} 
						else if (Input.GetKeyUp (KeyCode.Period) && !FollowPlayerToggle) 
						{
							CurrentTerminal += 1;

							if (CurrentTerminal >= TerminalList.Count) 
							{
								CurrentTerminal = TerminalList.Count - 1;
							}

							//MoveCameraTo (TerminalList [CurrentTerminal]);
						}

						if (Input.GetKeyUp (KeyCode.Space)) 
						{
							UniquePlayerCTRL.CreateClone (TerminalList [CurrentTerminal].transform.position);
							FollowPlayerToggle = true;
							CameraController.FollowPlayer (FollowPlayerToggle);
						}
					}
				}
			}
		}
	}

	void PeekAllTerminals(TerminalCircuitComponent terminal)
	{
		PeekNext (terminal);
		PeekPrev (terminal);

		for (int i = 0; i < nextCurrentCircuitComponents.Count; i++) 
		{
			TerminalList.Add (nextCurrentCircuitComponents[i] as TerminalCircuitComponent);
		}

		for (int i = 0; i < prevCurrentCircuitComponents.Count; i++) 
		{
			TerminalList.Add (prevCurrentCircuitComponents[i] as TerminalCircuitComponent);
			
		}
		
	}

	void PeekNext (TerminalCircuitComponent terminalCircuitComp)
	{
		if (terminalCircuitComp == null) 
		{
			return;
		}

		for (int i = 0; i < terminalCircuitComp.NextGameObjects.Count; i++)
		{
			nextCurrentCircuitComponents.Add (terminalCircuitComp.NextGameObjects[i].GetComponent<ICircuitComponents>());
			PeekNext (terminalCircuitComp.NextGameObjects[i].GetComponent<ICircuitComponents>() as TerminalCircuitComponent);
		}
	}

	void PeekPrev (TerminalCircuitComponent terminalCircuitComp)
	{
		if (terminalCircuitComp == null)
		{
			return;
		}
		for (int i = 0; i < terminalCircuitComp.PrevGameObjects.Count; i++)
		{
			prevCurrentCircuitComponents.Add (terminalCircuitComp.PrevGameObjects[i].GetComponent<ICircuitComponents>());
			PeekPrev (terminalCircuitComp.PrevGameObjects[i].GetComponent<ICircuitComponents>() as TerminalCircuitComponent);
		}
	}

	//void MoveCameraTo(TerminalCircuitComponent terminal)
	//{
	//	terminal.t
	//	Transform trans = GameObject.FindGameObjectWithTag("Terminal_0").GetComponent<Transform>();
	//	CameraController.SetTargetPos(trans.position);
	//}
}
