using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerCTRL _mPlayerController;
    [SerializeField]
    private float _mLoftRightOffset;

	public bool _mIsFollowingPlayer = true;
    private Vector3 _mTargetPos = Vector3.zero;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CharacterFacing facing = _mPlayerController.GetFacingDir();
		if(_mIsFollowingPlayer)
		{
			_mTargetPos = _mPlayerController.GetComponent<Transform>().position;

        	if (facing == CharacterFacing.FACE_LEFT)
        	{
        	    _mTargetPos.x -= _mLoftRightOffset;
        	}
        	else if (facing == CharacterFacing.FACE_RIGHT)
        	{
        	    _mTargetPos.x += _mLoftRightOffset;
        	}
			
			
        	if (facing == CharacterFacing.FACE_UP)
        	{
        	    _mTargetPos.y += _mLoftRightOffset;
        	}
        	else if (facing == CharacterFacing.FACE_DOWN)
        	{
        	    _mTargetPos.y -= _mLoftRightOffset;
        	}
		}
		else
		{
			
		}
		_mTargetPos.z = this.transform.position.z;

        Vector3 difference = _mTargetPos - transform.position;
        float differenceMag = difference.magnitude;
        transform.position += (difference * differenceMag) * Time.deltaTime;
    }

    public void SetTargetPlayerObject(PlayerCTRL newTarget)
    {
        _mPlayerController = newTarget;
    }

	public void FollowPlayer(bool followPlayer)
	{
        _mIsFollowingPlayer = followPlayer;
    }

	public void SetTargetPos(Vector3 target)
	{
        _mTargetPos = target;
    }
}
