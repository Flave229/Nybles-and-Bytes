using System.Collections.Generic;
using UnityEngine;

public class TerminalManager : MonoBehaviour
{
	PlayerCameraController CameraController;
    bool FollowPlayerToggle = true;
    int CurrentTerminal = 0;
	UniquePlayerCTRL UniquePlayerCTRL;

	public List<Terminal> TerminalList = new List<Terminal>();
	List<ICircuitComponent> prevCurrentCircuitComponents = new List<ICircuitComponent>();
	List<ICircuitComponent> nextCurrentCircuitComponents = new List<ICircuitComponent>();

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
				Terminal terminal = TerminalList [i];

				if (terminal != null) 
				{
					if (terminal.IsPlayerColliding ()) 
					{
						// current is not a clone
						if (Input.GetKeyUp (KeyCode.E))
						{
                            terminal.Peek();
                            CurrentTerminal = 0;
							FollowPlayerToggle = !FollowPlayerToggle;
							CameraController.FollowPlayer (FollowPlayerToggle);
						}

						if (Input.GetKeyDown (KeyCode.LeftArrow) && !FollowPlayerToggle) 
						{
							CurrentTerminal -= 1;

							if (CurrentTerminal <= 0)
							{
								CurrentTerminal = 0;
							}

							//MoveCameraTo (TerminalList [CurrentTerminal]);

						} 
						else if (Input.GetKeyUp (KeyCode.RightArrow) && !FollowPlayerToggle) 
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
}
