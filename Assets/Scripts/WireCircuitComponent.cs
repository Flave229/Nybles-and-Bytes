using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class WireCircuitComponent : MonoBehaviour, ICircuitComponents 
{
	public List<GameObject> PrevGameObjects;
	public List<GameObject> NextGameObjects;

	List<ICircuitComponents> PrevCircuitComponents = new List<ICircuitComponents>();
	List<ICircuitComponents> NextCircuitComponents = new List<ICircuitComponents>();

	UniquePlayerCTRL UPlayer;
	// Use this for initialization
	void Start () 
	{
		try 
		{
			foreach (var item in PrevGameObjects) {
				PrevCircuitComponents.Add(item.GetComponent<ICircuitComponents>());
			}

			foreach (var item in NextGameObjects) {
				NextCircuitComponents.Add(item.GetComponent<ICircuitComponents>());
			}
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

	public List<ICircuitComponents> Peek()
	{
		List<ICircuitComponents> ICC = new List<ICircuitComponents> ();

		for (int i = 0; i < NextCircuitComponents.Count; i++) 
		{
			ICC.AddRange(NextCircuitComponents [i].Peek());
		}

		return ICC;
	}

	public void Execute()
	{
		
	}
}
