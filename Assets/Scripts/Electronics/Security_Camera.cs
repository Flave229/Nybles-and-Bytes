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
    }

    // Update is called once per frame
    void Update()
    {
        if (Enabled == false)
            return;

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
                //Debug.DrawLine(transform.position, _lineOfSight.position, Color.blue);

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

                Debug.DrawLine(transform.position, fovLine1Pos, Color.red);
                Debug.DrawLine(transform.position, fovLine2Pos, Color.red);

                if (col.gameObject.GetComponent<UniquePlayerCTRL>() != null)
                {
                    

                    float angleToPlayer = Vector2.Angle(targetDir, lineOfSight * detectDistance);

                    if (angleToPlayer < FieldOfView)
                    {
                        
                        Debug.DrawLine(transform.position, col.gameObject.transform.position, Color.green);
                        col.gameObject.GetComponent<UniquePlayerCTRL>().DetectedByCamera();
                    } 
                    
                    return;
                }
                else
                {
                    float angleToPlayer = Vector2.Angle(targetDir, lineOfSight * detectDistance);
                    // Clones
                    if (col.gameObject.GetComponent<PlayerCTRL>() != null)
                    {
                        if (angleToPlayer < FieldOfView)
                        {

                            col.gameObject.GetComponent<PlayerCTRL>().DetectableBehaviour.Detected();

                            _mCloneIndex = GameManager.Instance().GetListOfEntities().Count - 1;
                            _mCloneIndex -= 1;
                            _mCamera.SetTargetPlayerObject(GameManager.Instance().GetListOfEntities()[_mCloneIndex]);
                            GameManager.Instance().GetListOfEntities()[_mCloneIndex].SetUserControlEnabled(true);
                        }
                    }
                }                                
            }
        }
    }
}