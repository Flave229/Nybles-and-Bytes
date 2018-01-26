using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class WireRenderer : MonoBehaviour
{
    public GameObject otherEnd;
    public Material wireMaterial;
    
	void Start()
    {
		
	}
	
	void Update()
    {
        if (!EditorApplication.isPlaying)
        {
            UpdateLineRenderer();
        }
    }

    public void UpdateLineRenderer()
    {
        LineRenderer line = GetComponent<LineRenderer>();
        if (line)
        {
            if (otherEnd)
            {
                line.enabled = true;
                line.material = wireMaterial;
                Vector3[] positions = new Vector3[]
                {
                    transform.position,
                    otherEnd.transform.position
                };
                line.positionCount = positions.Length;
                line.SetPositions(positions);
            }
            else
            {
                line.enabled = false;
                line.positionCount = 0;
            }
        }
    }
}
