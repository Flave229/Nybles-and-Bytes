﻿using Assets.Scripts;
using Assets.Scripts.AI;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour, ICircuitComponent
{
	private ICharacter _travellingCharacter;
    private Transform _travellingTransform;
    public GameObject DestinationDoor;
    public bool Locked;
	bool bActive;
	public float LiftAcceleration;
	public float LiftPeakSpeed;
	private float LiftCurrentSpeed;
	private int Direction;
    public Node PathfindNode;

    public GameObject PrevGameObject;
    ICircuitComponent PrevCircuitComponent;

	public Sprite OnSprite;
	public Sprite OffSprite;
	private SpriteRenderer _mySR;

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

		_mySR = GetComponentInParent<SpriteRenderer>();
	}
	
    public void Travel(ICharacter character, Transform characterTransform)
    {
        if (Locked)
            return;

        if (!bActive &&
            (characterTransform.position.x > transform.position.x - 1.0f && characterTransform.position.x < transform.position.x + 1.0f) && 
            (characterTransform.position.y > transform.position.y - 1.0f && characterTransform.position.y < transform.position.y + 1.0f)
            )
        {
            GameManager.Instance().GetSoundManager().PlaySoundEffect("Sounds/Elevator/ElevatorStart", false);
            bActive = true;
            _travellingCharacter = character;
            _travellingTransform = characterTransform;
            character.SetPossessed(true);
            characterTransform.position = new Vector3(characterTransform.position.x, characterTransform.position.y, 20);
            characterTransform.GetComponent<SpriteRenderer>().sortingOrder = -10;
            LiftCurrentSpeed = 0.0f;
            GameManager.Instance().GetSoundManager().PlaySoundEffect("Sounds/Elevator/ElevatorRunning", true);
        }
    }

	// Update is called once per frame
	void Update()
	{
		if (bActive)
		{
			if ((Direction == 1 && _travellingTransform.position.y + 0.67f - 0.382309f < DestinationDoor.transform.position.y) || 
                (Direction == -1 && _travellingTransform.position.y + 0.67f - 0.382309f > DestinationDoor.transform.position.y)
                )
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
                GameManager.Instance().GetSoundManager().PlaySoundEffect("Sounds/Elevator/ElevatorDoor", false);
                bActive = false;
                _travellingTransform.Translate(new Vector3(0, 0, -20));
                _travellingTransform.GetComponent<SpriteRenderer>().sortingOrder = 10;
                _travellingCharacter.SetPossessed(false);
                _travellingCharacter = null;
                _travellingTransform = null;
            }

			_mySR.sprite = OnSprite;
		}
		else
		{
			_mySR.sprite = Locked ? OffSprite : OnSprite;
		}
	}

    public List<ICircuitComponent> SeekNext()
    {
        return new List<ICircuitComponent>();
    }

    public List<ICircuitComponent> SeekPrev()
    {
        return new List<ICircuitComponent>
        {
            PrevCircuitComponent
        };
    }

    public List<ICircuitComponent> Peek()
    {
		//Debug.Log("I am a " + this.GetType().ToString() + " called " + this.transform.name + ".");
		return new List<ICircuitComponent>
        {
            this
        };
    }

    public void Execute()
    {
        Locked = !Locked;
        DestinationDoor.GetComponent<Lift>().Locked = Locked;
    }
}
