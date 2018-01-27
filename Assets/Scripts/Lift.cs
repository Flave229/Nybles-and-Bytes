﻿using Assets.Scripts;
using Assets.Scripts.AI;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
	private ICharacter _travellingCharacter;
    private Transform _travellingTransform;
    public GameObject DestinationDoor;
	bool bActive;
	public float LiftAcceleration;
	public float LiftPeakSpeed;
	private float LiftCurrentSpeed;
	private int Direction;
    public Node PathfindNode;

    void Awake()
    {
        PathfindNode = new Node
        {
            Position = transform.position,
            Owner = this.gameObject
        };
    }

    // Use this for initialization
    void Start()
	{
		if (DestinationDoor.transform.position.y > transform.position.y)
		{
			Direction = 1;
		}
		else
		{
			Direction = -1;
		}

        PathfindNode.ConnectingNodes = new List<Node>
        {
            DestinationDoor.GetComponent<Lift>().PathfindNode
        };
	}
	
    public void Travel(ICharacter character, Transform characterTransform)
    {
        if (!bActive && (characterTransform.position.y == transform.position.y - 0.75f) && characterTransform.localPosition.x > transform.localPosition.x - 1.0f && characterTransform.localPosition.x < transform.localPosition.x + 1.0f)
        {
            bActive = true;
            _travellingCharacter = character;
            _travellingTransform = characterTransform;
            character.SetPossessed(true);
            characterTransform.position = new Vector3(characterTransform.position.x, characterTransform.position.y, 20);
            LiftCurrentSpeed = 0.0f;
        }
    }

	// Update is called once per frame
	void Update()
	{
		if (bActive)
		{
			if ((Direction == 1 && _travellingTransform.position.y + 0.75f < DestinationDoor.transform.position.y) || (Direction == -1 && _travellingTransform.position.y + 0.75f > DestinationDoor.transform.position.y))
			{
				if (Mathf.Abs(LiftCurrentSpeed) < LiftPeakSpeed)
				{
					LiftCurrentSpeed += Direction*LiftAcceleration;
					if (Mathf.Abs(LiftCurrentSpeed) > LiftPeakSpeed) LiftCurrentSpeed = Direction * LiftPeakSpeed;
				}

                _travellingTransform.Translate(new Vector3(0, LiftCurrentSpeed, 0));
			}
			else
			{
				bActive = false;
                _travellingTransform.Translate(new Vector3(0, 0, -20));
                _travellingCharacter.SetPossessed(false);
                _travellingCharacter = null;
                _travellingTransform = null;
			}
		}
	}
}
