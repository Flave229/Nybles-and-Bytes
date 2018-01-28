using System.Collections.Generic;
using UnityEngine;

public enum OpenState
{
    CLOSED, OPEN
}

public class Door : MonoBehaviour, ICircuitComponent
{
    public OpenState openState;
    Vector3 scale;

    public GameObject PrevGameObject;
    ICircuitComponent PrevCircuitComponent;

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
        return new List<ICircuitComponent>
        {
            this
        };
    }

    public void Execute()
    {
        if (openState == OpenState.OPEN)
            openState = OpenState.CLOSED;
        else
            openState = OpenState.OPEN;
    }
}
