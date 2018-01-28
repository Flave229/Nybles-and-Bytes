using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusebox : MonoBehaviour, ICircuitComponent
{
	public bool Enabled;
	public List<GameObject> PrevGameObjects;
	public List<GameObject> NextGameObjects;

	List<ICircuitComponent> PrevCircuitComponents = new List<ICircuitComponent>();
	List<ICircuitComponent> NextCircuitComponents = new List<ICircuitComponent>();

    public Sprite OnSprite;
    public Sprite OffSprite;
    private SpriteRenderer _spriteRenderer;


    void Start()
    {
        _spriteRenderer = GetComponentInParent<SpriteRenderer>();
        if (Enabled)
            _spriteRenderer.sprite = OnSprite;
        else
            _spriteRenderer.sprite = OffSprite;

        foreach (GameObject prevObject in PrevGameObjects)
			PrevCircuitComponents.Add(prevObject.GetComponent<ICircuitComponent>());

		foreach (GameObject nextObject in NextGameObjects)
			NextCircuitComponents.Add(nextObject.GetComponent<ICircuitComponent>());
	}

	public void Execute()
	{
		if (Enabled == false)
			return;

		foreach (ICircuitComponent connectedComponent in NextCircuitComponents)
		{
			connectedComponent.Execute();
		}
	}

	public void Press(Transform characterTransform)
	{
		if ((characterTransform.position.y - 3.0f < transform.position.y) &&
			characterTransform.position.y + 3.0f > transform.position.y &&
			characterTransform.localPosition.x > transform.localPosition.x - 2.5f && characterTransform.localPosition.x < transform.localPosition.x + 2.5f)
		{
			Enabled = !Enabled;

            if (Enabled)
                _spriteRenderer.sprite = OnSprite;
            else
                _spriteRenderer.sprite = OffSprite;
		}
	}

	public List<ICircuitComponent> Peek()
	{
		if (Enabled == false)
			return new List<ICircuitComponent>();

		List<ICircuitComponent> components = new List<ICircuitComponent>();
		foreach (ICircuitComponent connectedComponents in NextCircuitComponents)
		{
			components.AddRange(connectedComponents.Peek());
		}
		//Debug.Log("I am a " + this.GetType().ToString() + " called " + this.transform.name + " with " + components.Count.ToString() + " connected components.");
		return components;
	}

	public List<ICircuitComponent> SeekNext()
	{
		return NextCircuitComponents;
	}

	public List<ICircuitComponent> SeekPrev()
	{
		return PrevCircuitComponents;
	}
}
