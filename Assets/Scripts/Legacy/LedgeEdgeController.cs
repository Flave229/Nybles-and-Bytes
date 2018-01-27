using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum LedgeAttachment
{
    CLIMB_LEFT = 0,
    CLIMB_RIGHT = 1
}

public class LedgeEdgeController : MonoBehaviour
{
    [SerializeField]
    private float _mSecondsToClimb;
    private float _mSecondsToClimbUp;
    private float _mSecondsToClimbOver;
    private float _mClimbTimeProgess;
    [SerializeField]
    private LedgeAttachment _mLedgeDirection;
    private CharacterFacing _mCompatibleClimbDir;
    private PlayerCTRL _mPlayerCTRL = null;
    private bool _mIsClimbing = false;

    private float _mPlayerHeight;
    private float _mClimbHeightOffset;
    private Rigidbody _mPlayerRigidBdy;

    private Vector3 _mClimbStartPos;
    private Vector3 _mClimbUpPos;
    private Vector3 _mClimbSidePos;

    // Use this for initialization
    void Start()
    {
        _mCompatibleClimbDir = _mLedgeDirection == LedgeAttachment.CLIMB_LEFT ? CharacterFacing.FACE_LEFT : CharacterFacing.FACE_RIGHT;
        _mSecondsToClimbUp = _mSecondsToClimb * 0.7f;
        _mSecondsToClimbOver = _mSecondsToClimb * 0.3f;

        _mClimbStartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mPlayerCTRL == null) return;
        if (_mIsClimbing)
        {
            ClimbLedge();
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            _mPlayerCTRL.SetFacingDir(CharacterFacing.FACE_UP);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _mPlayerCTRL.SetFacingDir(CharacterFacing.FACE_DOWN);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _mPlayerCTRL.SetFacingDir(CharacterFacing.FACE_LEFT);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _mPlayerCTRL.SetFacingDir(CharacterFacing.FACE_RIGHT);
        }
        else if (Input.GetKey(KeyCode.K))
        {
            Debug.Log("Key K");
            Scenes.instance.LoadScene(Scenes.Scene.GAME_OVER);
        }
        else
        {
            _mPlayerCTRL.SetFacingDir(CharacterFacing.FACE_CENTRE);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            CharacterFacing playerDir = _mPlayerCTRL.GetFacingDir();
            if (playerDir != CharacterFacing.FACE_CENTRE && playerDir != _mCompatibleClimbDir)
            {
                ReleasePlayer();
            }
            else
            {
                Debug.Log("Climbing");
                _mClimbTimeProgess = 0;
                _mIsClimbing = true;
            }
        }
    }

    private void ReleasePlayer()
    {
        Debug.Log("Releasing");
        _mPlayerCTRL.SetPossessed(false);
        _mPlayerCTRL = null;
    }

    private void ClimbLedge()
    {
        _mClimbTimeProgess += Time.deltaTime;

        if (_mClimbTimeProgess < _mSecondsToClimbUp)
        {
            float progress = (_mSecondsToClimbUp - _mClimbTimeProgess) / _mSecondsToClimbUp;
            _mPlayerCTRL.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(_mClimbUpPos, _mClimbStartPos, progress));
        }
        else if (_mClimbTimeProgess < _mSecondsToClimb)
        {
            float progress = (_mSecondsToClimbOver - (_mClimbTimeProgess - _mSecondsToClimbUp)) / _mSecondsToClimbOver;
            _mPlayerCTRL.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(_mClimbSidePos, _mClimbUpPos, progress));
        }

        if (_mClimbTimeProgess > _mSecondsToClimb)
        {
            _mIsClimbing = false;
            _mClimbTimeProgess = 0;
            ReleasePlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ledge Hit");
        if (other.gameObject.tag == "Player")
        {
            _mPlayerCTRL = other.gameObject.GetComponent<PlayerCTRL>();
            if (_mPlayerCTRL != null)
            {
                _mPlayerCTRL.SetPossessed(true);
                _mPlayerRigidBdy = _mPlayerCTRL.GetComponent<Rigidbody>();
                _mPlayerRigidBdy.velocity = Vector3.zero;
                _mPlayerCTRL.transform.position = _mClimbStartPos;
                _mPlayerCTRL.SetFacingDir(CharacterFacing.FACE_CENTRE);
                _mPlayerHeight = _mPlayerCTRL.GetComponent<CapsuleCollider>().height;
                
                _mClimbUpPos = transform.position;
                _mClimbUpPos.y += 0.5f + (_mPlayerHeight / 2);

                _mClimbSidePos = _mClimbUpPos;
                _mClimbSidePos.x += _mLedgeDirection == LedgeAttachment.CLIMB_LEFT ? -1 : 1;
            }
        }
    }
}
