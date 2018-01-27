using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class Security_Camera : MonoBehaviour, ICircuitComponent
{
    public int detectDistance;

    private Vector3 securityCameraPos;
    private Collider[] cols;
    private int _mCloneIndex;

    public bool Enabled;

    public GameObject PrevGameObject;
    ICircuitComponent PrevCircuitComponent;

    [SerializeField]
    private PlayerCameraController _mCamera;

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
        securityCameraPos = transform.position;
        cols = Physics.OverlapSphere(securityCameraPos, detectDistance);
        foreach (Collider col in cols)
        {
            if (col != null)
            {              
                if (col.gameObject.GetComponent<UniquePlayerCTRL>() != null)
                {
                    Vector3 targetDir = securityCameraPos - col.gameObject.transform.position;
                    float angleToPlayer = (Vector3.Angle(targetDir, transform.up));
                    
                    if (angleToPlayer >= -45 && angleToPlayer <= 45)
                    {
                        Debug.DrawLine(securityCameraPos, col.gameObject.transform.position, Color.red);
                        col.gameObject.GetComponent<UniquePlayerCTRL>().DetectedByCamera();
                    } 
                    
                    return;
                }
                else
                {
                    // Clones
                    if (col.gameObject.GetComponent<PlayerCTRL>() != null)
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