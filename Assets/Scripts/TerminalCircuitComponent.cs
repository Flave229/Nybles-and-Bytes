using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TerminalCircuitComponent : MonoBehaviour, ICircuitComponent
{
	public List<GameObject> PrevGameObjects;
	public List<GameObject> NextGameObjects;

	List<ICircuitComponent> PrevCircuitComponents = new List<ICircuitComponent>();
	List<ICircuitComponent> NextCircuitComponents = new List<ICircuitComponent>();

	UniquePlayerCTRL UPlayer;
	bool IsPlayerCollided = false;

	void Start () {
		try 
		{
			foreach (var item in PrevGameObjects)
				PrevCircuitComponents.Add(item.GetComponent<ICircuitComponent>());

			foreach (var item in NextGameObjects) 
				NextCircuitComponents.Add(item.GetComponent<ICircuitComponent>());

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

	public List<ICircuitComponent> SeekNext()
	{
		return NextCircuitComponents;
	}

	public List<ICircuitComponent> SeekPrev()
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

	public List<ICircuitComponent> Peek()
	{
		List<ICircuitComponent> ICC = new List<ICircuitComponent> ();

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