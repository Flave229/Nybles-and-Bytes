﻿using System.Collections;
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

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _mMoveForce;
    [SerializeField]
    private float _mJumpForce;
    [SerializeField]
    private CharacterFacing _mFacingDirection;
    private Rigidbody _mRigidBody;
    private bool _mIsPossessed;

    // Use this for initialization
    void Start()
    {
        _mRigidBody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (_mIsPossessed) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jumping");
            _mRigidBody.AddForce(Vector3.up * _mJumpForce);
        }
    }


    void FixedUpdate()
    {
        if (_mIsPossessed) return;

        float leftRight = Input.GetAxis("Horizontal");
        if (leftRight != 0)
        {
            _mFacingDirection = leftRight > 0 ? CharacterFacing.FACE_RIGHT : CharacterFacing.FACE_LEFT;
            _mRigidBody.MovePosition(this.transform.position + (new Vector3(leftRight * _mMoveForce, 0.0f, 0.0f) * Time.deltaTime));
        }

        _mRigidBody.AddForce(Vector3.down * 20.0f * _mRigidBody.mass);// artificial gravity stronger than regular gravity
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
}
