using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using DG.Tweening.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class TapOnTimeController : CustomBehaviour
{
    private const float DEFAULT_POINTER_SPEED = 0.5f;
    private const float MAX_POINTER_SPEED_INCREASE = 0.5f;
    private const float FREEZE_TIME_ON_CLICK = 0.5f;

    private bool mCountdownFinished = false;
    private bool mPassedMid = true;
    private Vector2 mDirectionVector = Vector2.right;
    private Vector2 mDefaultAnchoredPosition;
    private float mLastFireTime;
    
    public float FarRigth;
    public float FarLeft;
    public float FireInterval;
    public float PointerSpeed;
    
    public RectTransform Pointer;
    public Image PointerImage;
    public Image ScalaBackgroundImage;
    public TMP_Text TextZeroL;
    public TMP_Text TextZeroR;
    public TMP_Text TextMaxMultiplier;
    
    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        mDefaultAnchoredPosition = Pointer.anchoredPosition;
        mLastFireTime = -FireInterval;

        FarRigth = ScalaBackgroundImage.rectTransform.rect.width / 2;
        FarLeft = ScalaBackgroundImage.rectTransform.rect.width / -2;

        GameManager.OnCountdownFinished += OnCountdownFinished;
        GameManager.OnGameFinished += OnGameFinished;
        GameManager.OnLevelCompleted += OnLevelCompleted;
        GameManager.OnLevelFailed += OnLevelFailed;
        
        UpdatePointerSpeed();
    }

    public void Clicked()
    {
        if (Time.time - mLastFireTime >= FireInterval && mCountdownFinished)
        {
            mLastFireTime = Time.time;
            GameManager.TargetPressed(GetValue());

            Pointer.DOScale(1.25f, 0.1f).OnComplete(() =>
            {
                Pointer.DOScale(0.75f, 0.2f).OnComplete(() =>
                {
                    Pointer.DOScale(1f, 0.1f);
                });
            });

            //Pointer.DORotate(-Vector3.one, 0.4f);

            PointerImage.DOColor(Color.gray, FREEZE_TIME_ON_CLICK);
            ScalaBackgroundImage.DOColor(Color.gray, FREEZE_TIME_ON_CLICK);
            TextZeroL.DOColor(Color.gray, FREEZE_TIME_ON_CLICK);
            TextZeroR.DOColor(Color.gray, FREEZE_TIME_ON_CLICK);
            TextMaxMultiplier.DOColor(Color.gray, FREEZE_TIME_ON_CLICK);
        }
    }

    private float GetValue()
    {
        float PointerX = Mathf.Abs(Pointer.anchoredPosition.x);
        float HalfWidth = ScalaBackgroundImage.rectTransform.rect.width / 2;

        if (PointerX <= HalfWidth * 1 / 9)
        {
            return 4f;
        } 
        else if (PointerX <= HalfWidth * 3 / 9)
        {
            return 3f;
        } else if (PointerX <= HalfWidth * 5 / 9)
        {
            return 2f;
        } else if (PointerX <= HalfWidth * 7 / 9)
        {
            return 1f;
        }
        else
        {
            return 0;
        }
    }

    private void Update()
    {
        UpdatePointer();
    }

    private void UpdatePointer()
    {
        var pointerAnchoredPositionX = Pointer.anchoredPosition.x;
        
        if (Time.time - mLastFireTime < FREEZE_TIME_ON_CLICK || !mCountdownFinished)
        {
            return;
        }

        if (Time.time - mLastFireTime >= FireInterval - FREEZE_TIME_ON_CLICK)
        {
            PointerImage.DOColor(Color.white, FREEZE_TIME_ON_CLICK);
            ScalaBackgroundImage.DOColor(Color.white, FREEZE_TIME_ON_CLICK);
            TextZeroL.DOColor(Color.white, FREEZE_TIME_ON_CLICK);
            TextZeroR.DOColor(Color.white, FREEZE_TIME_ON_CLICK);
            TextMaxMultiplier.DOColor(Color.white, FREEZE_TIME_ON_CLICK);
        }
        
        if ((pointerAnchoredPositionX >= FarRigth || pointerAnchoredPositionX <= FarLeft) && mPassedMid)
        {
            ChangeDirection();
        }

        Pointer.anchoredPosition += mDirectionVector * (ScalaBackgroundImage.rectTransform.rect.width * Time.deltaTime * PointerSpeed);

        if (pointerAnchoredPositionX * Pointer.anchoredPosition.x < 0)
        {
            mPassedMid = true;
        }
    }

    private void ChangeDirection()
    {
        mDirectionVector *= -1;
        mPassedMid = false;
    }

    private void UpdatePointerSpeed()
    {
        PointerSpeed = DEFAULT_POINTER_SPEED +
                       MAX_POINTER_SPEED_INCREASE * ((float)GameManager.LevelManager.CurrentLevelNumber / GameManager.LevelManager.Levels.Count);
    }

    #region Events

    private void OnCountdownFinished()
    {
        mCountdownFinished = true;
        UpdatePointerSpeed();
    }

    private void OnGameFinished()
    {
        mCountdownFinished = false;
        mPassedMid = true;
        Pointer.anchoredPosition = mDefaultAnchoredPosition;
    }
    
    public void OnLevelCompleted()
    {
        mCountdownFinished = false;
        mPassedMid = true;
        Pointer.anchoredPosition = mDefaultAnchoredPosition;
    }

    public void OnLevelFailed()
    {
        mCountdownFinished = false;
        mPassedMid = true;
        Pointer.anchoredPosition = mDefaultAnchoredPosition;
    }

    #endregion
}
