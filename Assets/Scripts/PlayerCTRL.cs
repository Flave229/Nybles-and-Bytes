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
    private CharacterFacing _mFacingDirection;
    private bool _mIsPossessed;
    private bool _mIsControlledByUser = true;


    void FixedUpdate()
    {
        if (_mIsPossessed) return;
                
        if (!_mIsControlledByUser) return;

        if (Input.GetKey(KeyCode.K))
        {
            Debug.Log("Key K");
            Scenes.instance.LoadScene(Scenes.Scene.GAME_OVER);
        }
        
        float leftRight = Input.GetAxis("Horizontal");
        if (leftRight != 0)
            _mFacingDirection = leftRight > 0 ? CharacterFacing.FACE_RIGHT : CharacterFacing.FACE_LEFT;
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
