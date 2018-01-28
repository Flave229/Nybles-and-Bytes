﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Terminal : MonoBehaviour, ICircuitComponent
{
    private bool _currentlySelected;

	public List<GameObject> PrevGameObjects;
	public List<GameObject> NextGameObjects;

	List<ICircuitComponent> PrevCircuitComponents = new List<ICircuitComponent>();
	List<ICircuitComponent> NextCircuitComponents = new List<ICircuitComponent>();

	UniquePlayerCTRL UPlayer;
	bool IsPlayerCollided = false;

    private PlayerCameraController _cameraController;
    private List<ICircuitComponent> _connectedTerminals;
    private int _currentTerminalIndex;
    private ICircuitComponent _currentTerminal;

    void Start () {
		try 
		{
			foreach (var item in PrevGameObjects)
				PrevCircuitComponents.Add(item.GetComponent<ICircuitComponent>());

			foreach (var item in NextGameObjects) 
				NextCircuitComponents.Add(item.GetComponent<ICircuitComponent>());

		} 
		catch (Exception) 
		{
			Debug.Log ("CircuitComponent List has a none ICircuitComponent.");
        }

        UPlayer = UnityEngine.Object.FindObjectOfType(typeof(UniquePlayerCTRL)) as UniquePlayerCTRL;
        _cameraController = UnityEngine.Object.FindObjectOfType(typeof(PlayerCameraController)) as PlayerCameraController;
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (_currentlySelected == false)
            return;

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            _cameraController.FollowPlayer(true);
            _currentlySelected = false;
			UPlayer.GetComponentInParent<PlayerCTRL>().SetPossessed(false);
		}
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _currentTerminalIndex = _currentTerminalIndex - 1 <= 0 ? 0 : (_currentTerminalIndex - 1);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            _currentTerminalIndex = _currentTerminalIndex + 1 >= _connectedTerminals.Count ? _connectedTerminals.Count - 1 : _currentTerminalIndex + 1;
        }

        _currentTerminal = _connectedTerminals[_currentTerminalIndex];

        if (Input.GetKeyUp(KeyCode.Space))
            _connectedTerminals[_currentTerminalIndex].Execute();
	}

    public void Press()
    {
        if (IsPlayerColliding())
        {
            _cameraController.FollowPlayer(false);
			UPlayer.GetComponentInParent<PlayerCTRL>().SetPossessed(true);
            _connectedTerminals = Peek();
			// this terminal should always be the last one
			_connectedTerminals.Remove(this);
			// this didn't work
            //_connectedTerminals.Where(x => (x as Terminal).gameObject != gameObject).ToList();
            foreach(Terminal terminal in _connectedTerminals)
                Debug.Log(terminal);
            _currentTerminalIndex = 0;
            _currentlySelected = true;
        }
    }

	public List<ICircuitComponent> SeekNext()
	{
		return NextCircuitComponents;
	}

	public List<ICircuitComponent> SeekPrev()
	{
		return PrevCircuitComponents;
	}

	public void Execute()
    {
        UPlayer.GetComponentInParent<PlayerCTRL>().SetPossessed(false);
        PlayerCTRL clonedPlayer = UPlayer.CreateClone (this.transform.position);
        _cameraController.SetTargetPlayerObject(clonedPlayer);
        _cameraController.FollowPlayer(true);
    }

	public bool IsPlayerColliding()
	{
		return IsPlayerCollided;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "Player_Unique") 
		{
			IsPlayerCollided = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.name == "Player_Unique") 
		{
			IsPlayerCollided = false;
		}
	}

	public List<ICircuitComponent> Peek()
	{
		List<ICircuitComponent> ICC = new List<ICircuitComponent> ();

		for (int i = 0; i < NextCircuitComponents.Count; i++)
			ICC.AddRange(NextCircuitComponents [i].Peek());

		ICC.Add (this);
        ICC = ICC.Where(x => x as Terminal != null).ToList();
		return ICC;
	}
}