using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
	private PlayerCTRL PC;
	public GameObject DestinationDoor;
	bool bActive;
	public float LiftAcceleration;
	public float LiftPeakSpeed;
	private float LiftCurrentSpeed;
	private int Direction;

	// Use this for initialization
	void Start()
	{
		PC = FindObjectOfType<PlayerCTRL>();

		if (DestinationDoor.transform.position.y > transform.position.y)
		{
			Direction = 1;
		}
		else
		{
			Direction = -1;
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!bActive && Input.GetKey(KeyCode.E) && (PC.transform.position.y == transform.position.y - 0.75f) && PC.transform.localPosition.x > transform.localPosition.x-1.0f && PC.transform.localPosition.x < transform.localPosition.x + 1.0f)
		{
			bActive = true;
			PC.SetPlayerPossessed(true);
			PC.transform.position = new Vector3(PC.transform.position.x, PC.transform.position.y, 20);
			LiftCurrentSpeed = 0.0f;
		}

		if (bActive)
		{
			if ((Direction == 1 && PC.transform.position.y + 0.75f < DestinationDoor.transform.position.y) || (Direction == -1 && PC.transform.position.y + 0.75f > DestinationDoor.transform.position.y))
			{
				if (/*((Direction == 1 && PC.transform.position.y + 0.75f - transform.position.y < (DestinationDoor.transform.position.y - transform.position.y) / 2) ||
					(Direction == -1 && PC.transform.position.y + 0.75f - transform.position.y < (DestinationDoor.transform.position.y - transform.position.y) / 2))
					&& */Mathf.Abs(LiftCurrentSpeed) < LiftPeakSpeed)
				{
					LiftCurrentSpeed += Direction*LiftAcceleration;
					if (Mathf.Abs(LiftCurrentSpeed) > LiftPeakSpeed) LiftCurrentSpeed = Direction * LiftPeakSpeed;
				}
				/*else if (PC.transform.position.y + 0.75f - transform.position.y > (DestinationDoor.transform.position.y - transform.position.y) / 2)
				{
					LiftCurrentSpeed -= Direction*LiftAcceleration;
					if (LiftCurrentSpeed <= 0.0f) LiftCurrentSpeed = 0.01f;
				}*/

				PC.transform.Translate(new Vector3(0, LiftCurrentSpeed, 0));
			}
			else
			{
				bActive = false;
				PC.transform.Translate(new Vector3(0, 0, -20));
				PC.SetPlayerPossessed(false);
			}
		}
	}
}
