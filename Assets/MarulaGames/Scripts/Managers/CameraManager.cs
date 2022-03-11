using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : CustomBehaviour
{
    public List<CameraOptions> CameraOptions;
    public int ActiveCameraOption;

    private Transform mCameraTransform;
    private Vector3 mUpdatedCameraPosition;

    public Transform PlayerTransform;
    private Vector3 mCurrentOffset;
    private Vector3 mInitialOffset;
    private float mOffsetX;
    private bool mCanFollow = false;
    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        GameManager.OnStartGame += OnStartGame;
        GameManager.OnResetToMainMenu += OnResetToMainMenu;

        InitializeCustomOptions();
    }

    private void InitializeCustomOptions()
    {
        mCameraTransform = MainCamera.transform;
        mCameraTransform.position = CameraOptions[ActiveCameraOption].Position;

        mCameraTransform.eulerAngles = CameraOptions[ActiveCameraOption].Rotation;

        mCurrentOffset = mCameraTransform.position - PlayerTransform.position;
        mInitialOffset = mCurrentOffset;

        mOffsetX = mCameraTransform.position.x - PlayerTransform.position.x;

        mUpdatedCameraPosition = new Vector3(mOffsetX, mCurrentOffset.y, mCurrentOffset.z + PlayerTransform.position.z);
    }

    private void LateUpdate()
    {
        CameraFollowPlayer();
    }

    private void CameraFollowPlayer()
    {
        if (!mCanFollow)
        {
            return;
        }
        else
        {
            mUpdatedCameraPosition = new Vector3(mOffsetX + PlayerTransform.position.x, mCurrentOffset.y, mCurrentOffset.z + PlayerTransform.position.z);
            mCameraTransform.position = mUpdatedCameraPosition;
        }
    }
    
    private void ResetOffset()
    {
        mCurrentOffset = mInitialOffset;
    }

    #region GameManager Events
    private void OnStartGame()
    {
        mCanFollow = true;
    }

    private void OnResetToMainMenu()
    {
        ResetOffset();
    }
    #endregion

    public void CameraShake(float delay, float duration)
    {

    }
}

[System.Serializable]
public struct CameraOptions
{
    public Vector3 Position;
    public Vector3 Rotation;
}
