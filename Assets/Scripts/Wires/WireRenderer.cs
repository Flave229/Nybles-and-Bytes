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
    public bool startHorizontal = true;

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

                List<Vector3> positions = new List<Vector3>();
                positions.Add(transform.position);
                Vector3 toOtherEnd = otherEnd.transform.position - transform.position;
                if (toOtherEnd.x != 0 && toOtherEnd.y != 0)
                {
                    Vector3 midpoint = new Vector3();
                    if (startHorizontal)
                    {
                        midpoint.y = transform.position.y;
                        midpoint.x = otherEnd.transform.position.x;
                    }
                    else
                    {
                        midpoint.x = transform.position.x;
                        midpoint.y = otherEnd.transform.position.y;
                    }
                    positions.Add(midpoint);
                }
                positions.Add(otherEnd.transform.position);

                line.positionCount = positions.Count;
                line.SetPositions(positions.ToArray());
            }
            else
            {
                line.enabled = false;
                line.positionCount = 0;
            }
        }
    }
}
