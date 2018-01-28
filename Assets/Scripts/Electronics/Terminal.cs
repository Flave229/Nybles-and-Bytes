using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Assets.Scripts;

public class Terminal : MonoBehaviour, ICircuitComponent
{
    private bool _currentlySelected;
    private bool _surprise;
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

	private PlayerCTRL MyClonePC = null;

	private SpriteRenderer _mySR;

    void Start ()
	{
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
		_mySR = GetComponentInParent<SpriteRenderer>();
		SetHighlight(false);
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (_currentlySelected == false)
            return;

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (_surprise == false)
            {
                _cameraController.FollowPlayer(true);
				Deselect();
            }
            else
                _surprise = false;
		}
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
			(_currentTerminal as Terminal).SetHighlight(false);
            _currentTerminalIndex = _currentTerminalIndex - 1 < 0 ? _connectedTerminals.Count - 1 : (_currentTerminalIndex - 1);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
			(_currentTerminal as Terminal).SetHighlight(false);
			_currentTerminalIndex = _currentTerminalIndex + 1 >= _connectedTerminals.Count ? 0 : _currentTerminalIndex + 1;
        }

        _currentTerminal = _connectedTerminals[_currentTerminalIndex];
		(_currentTerminal as Terminal).SetHighlight(true);

		if (Input.GetKeyUp(KeyCode.Space))
		{
            GameManager.Instance().GetSoundManager().PlaySoundEffect("Sounds/Player/Transfer/TransferNoise6", false);
            Deselect();
            _cameraController.FollowPlayer(true);
            _connectedTerminals[_currentTerminalIndex].Execute();
		}
	}

	void Deselect()
	{
		_currentlySelected = false;
		UPlayer.GetComponentInParent<PlayerCTRL>()._mIsAtTerminal = false;
		SetHighlight(false);
		(_currentTerminal as Terminal).SetHighlight(false);
	}

	public void SetHighlight(bool bGreen)
	{
		_mySR.color = bGreen ? Color.green : Color.white;
	}

    public void Press()
    {
        if (IsPlayerColliding())
        {
            GameManager.Instance().GetSoundManager().PlaySoundEffect("Sounds/Keyboard/KeyboardButtons", false);
            _cameraController.FollowPlayer(false);
			UPlayer.GetComponentInParent<PlayerCTRL>()._mIsAtTerminal = true;
			SetHighlight(true);
            _connectedTerminals = Peek();
			_connectedTerminals.Remove(this);
            _currentTerminalIndex = 0;
            _currentlySelected = true;
            _surprise = true;
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
		if (MyClonePC != null) return;

        MyClonePC = UPlayer.CreateClone(this.transform.position);
		UPlayer.GetComponentInParent<PlayerCTRL>().SetUserControlEnabled(false);
		UPlayer.PossessClone(MyClonePC);
        _cameraController.FollowPlayer(true);
    }
	public bool IsPlayerColliding()
	{
		return IsPlayerCollided && UPlayer.GetComponentInParent<PlayerCTRL>().IsControlledByUser();
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