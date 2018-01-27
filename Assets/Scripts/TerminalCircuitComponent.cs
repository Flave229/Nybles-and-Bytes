using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TerminalCircuitComponent : MonoBehaviour, ICircuitComponents
{
	public List<GameObject> PrevGameObjects;
	public List<GameObject> NextGameObjects;

	List<ICircuitComponents> PrevCircuitComponents = new List<ICircuitComponents>();
	List<ICircuitComponents> NextCircuitComponents = new List<ICircuitComponents>();

	UniquePlayerCTRL UPlayer;
	bool IsPlayerCollided = false;

	void Start () {
		try 
		{
			foreach (var item in PrevGameObjects)
				PrevCircuitComponents.Add(item.GetComponent<ICircuitComponents>());

			foreach (var item in NextGameObjects) 
				NextCircuitComponents.Add(item.GetComponent<ICircuitComponents>());

			UPlayer = this.GetComponent<UniquePlayerCTRL>();
		} 
		catch (Exception) 
		{
			Debug.Log ("CircuitComponent List has a none ICircuitComponent.");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public List<ICircuitComponents> SeekNext()
	{
		return NextCircuitComponents;
	}

	public List<ICircuitComponents> SeekPrev()
	{
		return NextCircuitComponents;
	}

	public void Execute()
	{
		// Call execute on all connected components 
		UPlayer.CreateClone (this.transform.position);
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

	public List<ICircuitComponents> Peek()
	{
		List<ICircuitComponents> ICC = new List<ICircuitComponents> ();

		for (int i = 0; i < NextCircuitComponents.Count; i++) 
		{
			ICC.AddRange(NextCircuitComponents [i].Peek());
		}

		ICC.Add (this);

		return ICC;
	}

	// This is over-engineering. Maybe don't need this. 
	//List<ICircuitComponents> Peek(Type type)
	//{
		// Start new list
		// For each component attached to the output of THIS component
			// Call Peek on current iterated component.
			// Append results onto the list
		// End For
		// Add this component (If it is a component that needs addings (not wires))
		// Return List
	//}
}