using UnityEngine;

public class Terminal : MonoBehaviour
{
    public string TerminalName;
    public GameObject ThisTerminal;
	public bool IsPlayerColliding;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player") 
		{
			IsPlayerColliding = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Player") 
		{
			IsPlayerColliding = false;
		}
	}
}
