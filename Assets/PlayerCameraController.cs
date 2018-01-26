using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerController _mPlayerController;
    [SerializeField]
    private float _mLoftRightOffset;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CharacterFacing facing = _mPlayerController.GetFacingDir();
        Vector3 targetPos = _mPlayerController.GetComponent<Transform>().position;
        targetPos.z = this.transform.position.z;

        if (facing == CharacterFacing.FACE_LEFT)
        {
            targetPos.x -= _mLoftRightOffset;
        }
        else if (facing == CharacterFacing.FACE_RIGHT)
        {
            targetPos.x += _mLoftRightOffset;
        }
        if (facing == CharacterFacing.FACE_UP)
        {
            targetPos.y += _mLoftRightOffset;
        }
        else if (facing == CharacterFacing.FACE_DOWN)
        {
            targetPos.y -= _mLoftRightOffset;
        }

        Vector3 difference = targetPos - transform.position;
        float differenceMag = difference.magnitude;
        transform.position += (difference * differenceMag) * Time.deltaTime;
    }
}
