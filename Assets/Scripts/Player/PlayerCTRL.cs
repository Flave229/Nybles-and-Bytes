using Assets;
using Assets.Scripts;
using System.Linq;
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


public class PlayerCTRL : MonoBehaviour, ICharacter
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
        DetectableBehaviour = new CloneDetected(gameObject);
		_mMoveForce = 10;
    }

    void Start()
    {
        _mRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (_mIsPossessed) return;

        // artificial gravity stronger than regular gravity
        _mRigidBody.AddForce( Vector3.down * 20.0f * _mRigidBody.mass );

        if (!_mIsControlledByUser) return;

        /*if (Input.GetKey(KeyCode.K))
        {
            Debug.Log("Key K");
            Scenes.instance.LoadScene(Scenes.Scene.GAME_OVER);
        }*/

        if (Input.GetKeyDown(KeyCode.E))
        {
            List<Lift> lifts = FindObjectsOfType<Lift>().OfType<Lift>().ToList();
            List<Switch> switches = FindObjectsOfType<Switch>().OfType<Switch>().ToList();

            foreach (Lift lift in lifts)
                lift.Travel(this, transform);

            foreach (Switch button in switches)
                button.Press(transform);
        }

        if (Input.GetKey(KeyCode.F))
        {
            List<Terminal> terminals = FindObjectsOfType<Terminal>().OfType<Terminal>().ToList();

            foreach (Terminal terminal in terminals)
                terminal.Press();
        }
        
        float leftRight = CalcLeftRightMovement();
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

    public void SetPossessed(bool possessed)
    {
        _mIsPossessed = possessed;
    }

    public void SetUserControlEnabled(bool state)
    {
        _mIsControlledByUser = state;
    }

    public bool GetPossessed()
    {
        return _mIsPossessed;
    }

    private float CalcLeftRightMovement()
    {
        float result = 0.0f;

        if ( Input.GetKey( KeyCode.D ) )
        {
            result += 1.0f;
        }

        if ( Input.GetKey( KeyCode.A ) )
        {
            result -= 1.0f;
        }

        return result;
    }
}
