using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Security_Camera : MonoBehaviour, ICircuitComponent
{
    public int detectDistance;
    public float FieldOfView;

    private Vector3 securityCameraPos;
    private Collider[] cols;
    private int _mCloneIndex;

    public bool Enabled;

    public GameObject PrevGameObject;
    ICircuitComponent PrevCircuitComponent;

    [SerializeField]
    private PlayerCameraController _mCamera;
    [SerializeField]
    private Transform _lineOfSight;

    private GameObject _line1Obj;
    private LineRenderer _lineRenderer1;
    private GameObject _line2Obj;
    private LineRenderer _lineRenderer2;

    public void Execute()
    {
        Enabled = !Enabled;
    }

    public List<ICircuitComponent> Peek()
    {
        return new List<ICircuitComponent>
        {
            this
        };
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

    // Use this for initialization
    void Start()
    {
        //player = GameObject.Find("Player_Unique");
        _line1Obj = new GameObject("Line 1");
        _lineRenderer1 = _line1Obj.AddComponent<LineRenderer>();
        _line2Obj = new GameObject("Line 2");
        _lineRenderer2 = _line2Obj.AddComponent<LineRenderer>();

        _lineRenderer1.startColor = Color.red;
        _lineRenderer1.startWidth = 0.1f;
        _lineRenderer1.endWidth = 0.1f;

        _lineRenderer2.startColor = Color.red;
        _lineRenderer2.startWidth = 0.1f;
        _lineRenderer2.endWidth = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enabled == false)
        {
            _lineRenderer1.enabled = false;
            _lineRenderer2.enabled = false;
            return;
        }

        if (transform.rotation.z > 360)
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        securityCameraPos = transform.position;
        cols = Physics.OverlapSphere(securityCameraPos, detectDistance);

        //transform.Rotate(new Vector3(0.0f, 0.0f, 0.2f));
        foreach (Collider col in cols)
        {
            if (col != null)
            {
                Vector2 targetDir = col.gameObject.transform.position - transform.position;
                Debug.DrawLine(transform.position, _lineOfSight.position, Color.blue);

                Vector2 lineOfSight = _lineOfSight.position - transform.position;
                lineOfSight.Normalize();
                float angleToLineOfSight;

                if (transform.eulerAngles.z >= 0 && transform.eulerAngles.z <= 180)
                {
                    angleToLineOfSight = Vector2.Angle(Vector2.right, lineOfSight);
                }
                else
                {
                    angleToLineOfSight = Vector2.Angle(-Vector2.right, lineOfSight);
                }

                float cos = (float)Math.Cos((Math.PI / 180.0f) * (angleToLineOfSight - FieldOfView));
                float cos2 = (float)Math.Cos((Math.PI / 180.0f) * (angleToLineOfSight + FieldOfView));
                float sin = (float)Math.Sin((Math.PI / 180.0f) * (angleToLineOfSight - FieldOfView));
                float sin2 = (float)Math.Sin((Math.PI / 180.0f) * (angleToLineOfSight + FieldOfView));
                float hypotenuse = Vector2.Distance(_lineOfSight.position, transform.position);
                Vector2 fovEdge1;
                Vector2 fovEdge2;

                if (transform.eulerAngles.z >= 0 && transform.eulerAngles.z <= 180)
                {
                    fovEdge1 = new Vector2(hypotenuse * cos, hypotenuse * sin);
                    fovEdge2 = new Vector2(hypotenuse * cos2, hypotenuse * sin2);
                }
                else
                {
                    fovEdge1 = new Vector2(hypotenuse * -cos, hypotenuse * -sin);
                    fovEdge2 = new Vector2(hypotenuse * -cos2, hypotenuse * -sin2);
                }

                fovEdge1 = Vector3.Normalize(fovEdge1);
                fovEdge2 = Vector3.Normalize(fovEdge2);

                fovEdge1 *= detectDistance;
                fovEdge2 *= detectDistance;

                Vector2 fovLine1Pos = (Vector2)transform.position + fovEdge1;
                Vector2 fovLine2Pos = (Vector2)transform.position + fovEdge2;

                DrawFOVLines(transform.position, fovLine1Pos, fovLine2Pos);

               // Debug.DrawLine(transform.position, fovLine1Pos, Color.red);
               // Debug.DrawLine(transform.position, fovLine2Pos, Color.red);

                if (col.gameObject.GetComponent<UniquePlayerCTRL>() != null)
                {
                    

                    float angleToPlayer = Vector2.Angle(targetDir, lineOfSight * detectDistance);

                    if (angleToPlayer < FieldOfView)
                    {
                        
                        //Debug.DrawLine(transform.position, col.gameObject.transform.position, Color.green);
                        col.gameObject.GetComponent<UniquePlayerCTRL>().DetectedByCamera();
                    } 
                }
                else
                {
                    // Clones
                    if (col.gameObject.GetComponent<PlayerCTRL>() != null)
                    {
                        targetDir = col.gameObject.transform.position - transform.position;
                        float angleToPlayer = Vector2.Angle(targetDir, lineOfSight * detectDistance);
                        if (angleToPlayer < FieldOfView)
                        {

                            col.gameObject.GetComponent<PlayerCTRL>().DetectableBehaviour.Detected();

                            _mCloneIndex = GameManager.Instance().GetListOfEntities().Count - 1;
                            //_mCloneIndex -= 1;
                            _mCamera.SetTargetPlayerObject(GameManager.Instance().GetListOfEntities()[_mCloneIndex]);
                            GameManager.Instance().GetListOfEntities()[_mCloneIndex].SetUserControlEnabled(true);
                        }
                    }
                }                                
            }
        }
    }

    void DrawFOVLines(Vector2 sourcePos, Vector2 fovLine1, Vector2 fovLine2)
    {
        _lineRenderer1.enabled = true;
        _lineRenderer2.enabled = true;
        List<Vector3> paths = new List<Vector3>();

        paths.Add(fovLine1);
        paths.Add(sourcePos);
        _lineRenderer1.positionCount = paths.Count;
        _lineRenderer1.SetPositions(paths.ToArray());

        paths.Clear();
        paths.Add(fovLine2);
        paths.Add(sourcePos);
        _lineRenderer2.positionCount = paths.Count;
        _lineRenderer2.SetPositions(paths.ToArray());
    }
}