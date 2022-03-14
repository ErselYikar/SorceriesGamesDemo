using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Target : CustomBehaviour
{
    public float Multiplier { get; private set; }

    private float mLastFireTime;
    private float FireInterval = 2;
    private bool mCountdownFinished = false;

    public SpriteRenderer ReloadIcon;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        GameManager.OnCountdownFinished += OnCountdownFinished;
        GameManager.OnGameFinished += OnGameFinished;
        GameManager.OnLevelCompleted += OnLevelCompleted;
        GameManager.OnLevelFailed += OnLevelFailed;

        ReloadIcon.enabled = false;
    }

    public void SetMultiplier(float multiplier)
    {
        Multiplier = multiplier;
        
        
    }

    public void Clicked()
    {
        if (Time.time - mLastFireTime >= FireInterval && mCountdownFinished)
        {
            mLastFireTime = Time.time;
            GameManager.TargetPressed(Multiplier);
            GameManager.CameraManager.CameraShake(0, 0.5f);
            GameManager.VibrationsManager.Vibrate();

            Animator.SetFloat("Speed", 0f);
            
            ReloadAnimation();

            Invoke(nameof(ResumeAnimation), 2f);
        }
    }

    private void ResumeAnimation()
    {
        Animator.SetFloat("Speed", 1f);
    }

    private void ReloadAnimation()
    {
        ReloadIcon.enabled = true;

        ReloadIcon.DOColor(Color.gray, 1f).SetEase(Ease.Linear).OnComplete(DoColorWhiteAnimation);
    }

    private void DoColorWhiteAnimation()
    {
        ReloadIcon.DOColor(Color.white, 1f).SetEase(Ease.Linear).OnComplete(DisableReloadIcon);
    }

    private void DisableReloadIcon()
    {
        ReloadIcon.enabled = false;
    }

    #region Events

    private void OnCountdownFinished()
    {
        mCountdownFinished = true;
    }

    private void OnGameFinished()
    {
        mCountdownFinished = false;
    }
    
    public void OnLevelCompleted()
    {
        mCountdownFinished = false;
    }

    public void OnLevelFailed()
    {
        mCountdownFinished = false;
    }

    #endregion
}
