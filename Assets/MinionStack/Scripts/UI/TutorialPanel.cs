using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class TutorialPanel : UIPanel
{
    public CustomButton ButtonPlay;
    public List<RectTransform> Steps;

    private Sequence mFadeSequence;

    private List<float> mStepsInitialPosList = new List<float>
    {
        -1700,
        -2355,
        -3000
    };

    private List<float> mStepsTargetPosList = new List<float>
    {
        750,
        100,
        -550
    };

    private float mButtonPlayInitialPos = -3500;

    private float mButtonPlayTargetPos = -1040;

    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);

        ButtonPlay.Initialize(uiManager, OnButtonClickedPlay);
        InitializeSequence();
    }

    private void InitializeSequence()
    {
        mFadeSequence = DOTween.Sequence().SetAutoKill(false).Pause();

        mFadeSequence.Append(Steps[0].DOAnchorPosY(mStepsTargetPosList[0], 0.4f).From(new Vector2(0, mStepsInitialPosList[0])).SetEase(Ease.OutExpo));
        mFadeSequence.Insert(0.1f, Image.DOFade(0.6f, 0.3f).From(0f));
        mFadeSequence.Insert(0.15f, Steps[1].DOAnchorPosY(mStepsTargetPosList[1], 0.4f).From(new Vector2(0, mStepsInitialPosList[1])).SetEase(Ease.OutExpo));
        mFadeSequence.Insert(0.3f, Steps[2].DOAnchorPosY(mStepsTargetPosList[2], 0.4f).From(new Vector2(0, mStepsInitialPosList[2])).SetEase(Ease.OutExpo));
        mFadeSequence.Insert(0.45f, ButtonPlay.RectTransform.DOAnchorPosY(mButtonPlayTargetPos, 0.4f).From(new Vector2(0, mButtonPlayInitialPos)).SetEase(Ease.OutExpo));
    }

	public override void ShowPanel()
	{
		base.ShowPanel();
        FadeInAnimation();
	}

	public override void HidePanel()
	{
		base.HidePanel();
	}

    private void FadeInAnimation()
    {
        mFadeSequence.PlayForward();
    }

    private void FadeOutAnimation()
    {
        mFadeSequence.PlayBackwards();
        Invoke("InvokeFadeOut", 0.8f);
    }

    private void InvokeFadeOut()
    {
        HidePanel();
        GameManager.StartGame();
    }

	#region Button Events

	private void OnButtonClickedPlay()
    {
        FadeOutAnimation();

    }

    #endregion
}