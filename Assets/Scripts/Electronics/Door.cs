using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OpenState
{
    CLOSED, OPEN
}

public class Door : MonoBehaviour
{
    public OpenState openState;

    Vector3 scale;
    
	void Start()
    {
        scale = transform.localScale;
	}
	
	void Update()
    {
        bool open = openState == OpenState.OPEN;
        GetComponent<Collider>().enabled = !open;
        transform.localScale = new Vector3(
            scale.x * (open ? 0.1f : 1),
            scale.y,
            scale.z);
	}

    public void Open()
    {
        openState = OpenState.OPEN;
    }

    public void Close()
    {
        openState = OpenState.CLOSED;
    }
}
