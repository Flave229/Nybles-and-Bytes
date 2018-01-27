using Assets;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterFacing
{
    FACE_LEFT = 0,
    FACE_RIGHT = 1,
    FACE_UP = 2,
    FACE_DOWN = 3,
    FACE_CENTRE = 4
}


public class PlayerCTRL : MonoBehaviour
{
    [SerializeField]
    private float _mMoveForce;
    [SerializeField]
    private CharacterFacing _mFacingDirection;
    private bool _mIsPossessed;
    private bool _mIsControlledByUser = true;
    private Rigidbody _mRigidBody;

    public IDetectable DetectableBehaviour;

    private void Awake()
    {
        DetectableBehaviour = new CloneDetected();
    }

    void Start()
    {
        _mRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (_mIsPossessed) return;
                
        if (!_mIsControlledByUser) return;

        // artificial gravity stronger than regular gravity
        _mRigidBody.AddForce(Vector3.down * 20.0f * _mRigidBody.mass);

        if (Input.GetKey(KeyCode.K))
        {
            Debug.Log("Key K");
            Scenes.instance.LoadScene(Scenes.Scene.GAME_OVER);
        }
        
        float leftRight = Input.GetAxis("Horizontal");
        if (leftRight != 0)
        {
            _mFacingDirection = leftRight > 0 ? CharacterFacing.FACE_RIGHT : CharacterFacing.FACE_LEFT;
            _mRigidBody.MovePosition(this.transform.position + (new Vector3(leftRight * _mMoveForce, 0.0f, 0.0f) * Time.deltaTime));
        }
    }

    public void SetFacingDir(CharacterFacing dir)
    {
        _mFacingDirection = dir;
    }

    public CharacterFacing GetFacingDir()
    {
        return _mFacingDirection;
    }

    public void SetPlayerPossessed(bool possessed)
    {
        _mIsPossessed = possessed;
    }

    public void SetUserControlEnabled(bool state)
    {
        _mIsControlledByUser = state;
    }
}
