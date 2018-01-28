using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class WireCircuitComponent : MonoBehaviour, ICircuitComponent 
{
	public List<GameObject> PrevGameObjects;
	public List<GameObject> NextGameObjects;

	List<ICircuitComponent> PrevCircuitComponents = new List<ICircuitComponent>();
	List<ICircuitComponent> NextCircuitComponents = new List<ICircuitComponent>();

	UniquePlayerCTRL UPlayer;
	// Use this for initialization
	void Start () 
	{
		try 
		{
			foreach (var item in PrevGameObjects) {
				PrevCircuitComponents.Add(item.GetComponent<ICircuitComponent>());
			}

			foreach (var item in NextGameObjects) {
				NextCircuitComponents.Add(item.GetComponent<ICircuitComponent>());
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

	public List<ICircuitComponent> SeekNext()
	{
		return NextCircuitComponents;
	}

	public List<ICircuitComponent> SeekPrev()
	{
		return PrevCircuitComponents;
	}

	public List<ICircuitComponent> Peek()
	{
		List<ICircuitComponent> ICC = new List<ICircuitComponent> ();

		for (int i = 0; i < NextCircuitComponents.Count; i++) 
		{
			ICC.AddRange(NextCircuitComponents [i].Peek());
		}
        
        return ICC;
	}

	public void Execute()
	{
        foreach (ICircuitComponent component in NextCircuitComponents)
            component.Execute();
	}
}
