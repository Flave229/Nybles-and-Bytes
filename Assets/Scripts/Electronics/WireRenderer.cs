using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]*/
public class WireRenderer : MonoBehaviour
{
    public Material _mWireMaterial;
    public float _mWidth;
    private WireCircuitComponent _mCircuitComp;
    private LineRenderer _mLineDrawer;

    void Start()
    {
        _mCircuitComp = GetComponent<WireCircuitComponent>();
        if (_mCircuitComp.SeekPrev().Count > 0 && _mCircuitComp.SeekNext().Count > 0)
        {
            _mLineDrawer = gameObject.AddComponent<LineRenderer>();

            Keyframe[] width = new Keyframe[2];
            width[0] = new Keyframe(0.0f, _mWidth);
            width[1] = new Keyframe(1.0f, _mWidth);

            _mLineDrawer.enabled = true;
            _mLineDrawer.material = _mWireMaterial;
            _mLineDrawer.startWidth = _mWidth;
            _mLineDrawer.startWidth = _mWidth;
            _mLineDrawer.endWidth = _mWidth;
            //_mLineDrawer.widthCurve = new AnimationCurve(width);
        }
        else
        {
            Debug.Log("ABORT");
        }
    }

    void Update()
    {
        if (!EditorApplication.isPlaying)
        {
            UpdateLineRenderer();
        }
        else
        {
            UpdateLineRenderer();
        }
    }

    public void UpdateLineRenderer()
    {
        if (_mCircuitComp.SeekPrev().Count > 0 && _mCircuitComp.SeekNext().Count > 0)
        {

            List<Vector3> positions = new List<Vector3>();

            for (int i = 0; i < _mCircuitComp.SeekPrev().Count; i++)
            {
                List<Vector3> path = new List<Vector3>();
                for (int j = 0; j < _mCircuitComp.SeekNext().Count; j++)
                {
                    List<ICircuitComponent> inPoint = _mCircuitComp.SeekPrev();
                    List<ICircuitComponent> outPoint = _mCircuitComp.SeekNext();

                    MonoBehaviour start = inPoint[i] as MonoBehaviour;
                    MonoBehaviour end = outPoint[j] as MonoBehaviour;

                    if (start == end)
                    {
                        continue;
                    }

                    if (start && end)
                    {
                        path.Add(end.transform.position);
                        path.Add(start.transform.position);
                        //Debug.Log("Drawing " + start.name + " to " + end.name);
                    }
                }
                try
                {
                    _mLineDrawer.positionCount = path.Count;
                    _mLineDrawer.SetPositions(path.ToArray());
                }
                catch (Exception exception)
                {
                    Debug.Log(gameObject.name + " - HAS NO DEBUG RENDERER" + exception.ToString());
                }
            }
        }
    }
}
