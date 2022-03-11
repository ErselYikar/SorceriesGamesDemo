using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.UI;

public class LevelFinishedPanel : UIPanel
{
    [Header("Fail")]
    public CustomButton ButtonReplay;
    public CanvasGroup FailCanvas;

    [Header("Success")]
    public CustomButton ButtonContinue;
    public CanvasGroup SuccessCanvas;

    [Header("Reward")]
    public TMP_Text TextDiamondReward;
    public TMP_Text TextDiamondTotal;
    public Image ImageDiamondReward;
    public Image ImageDiamondTotal;
    
    private Image mToggleSoundBg;
    private Image mToggleVibrationBg;

    private int mDiamondReward;
    
    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);
        ButtonReplay.Initialize(uiManager, OnButtonClickedReplay);
        ButtonContinue.Initialize(uiManager, OnButtonClickedContinue);

        GameManager.OnLevelCompleted += OnLevelCompleted;
        GameManager.OnLevelFailed += OnLevelFailed;
    }
    

    public override void ShowPanel()
    {
        base.ShowPanel();
        
        UpdateLabels();
        StartCoroutine(AnimateRewards(1.5f, 2f));
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void UpdateLabels()
    {
        base.UpdateLabels();

        var totalDiamonds = GameManager.PlayerManager.GetTotalCoinCount();
        var tmpDiamonds = totalDiamonds - mDiamondReward;
        
        TextDiamondReward.SetText(mDiamondReward.ToString());
        TextDiamondTotal.SetText(tmpDiamonds.ToString());
    }
    
    #region Reward Animations
    
    private IEnumerator AnimateRewards(float delayOnStart, float duration)
    {
        if (delayOnStart > 0f)
        {
            yield return new WaitForSeconds(delayOnStart);
        }

        AnimateDiamondImage(duration);
        StartCoroutine(AnimateRewardText(duration));
    }

    private void AnimateDiamondImage(float duration)
    {
        var fromImage = ImageDiamondReward;
        var toImage = ImageDiamondTotal;
        var imageCount = 5;
        
        for (int i = 1; i < imageCount + 1; i++)
        {
            var tmpImage = Instantiate(fromImage, fromImage.transform.position, Quaternion.identity, RectTransform);
			
            var durationScale = (float)i / imageCount;

            tmpImage.transform.DOScale(Vector3.one, duration * durationScale);
            tmpImage.transform.DOMove(toImage.transform.position, duration * durationScale);
            
            Destroy(tmpImage.gameObject, duration * durationScale);
        }
    }
    
    private IEnumerator AnimateRewardText(float duration)
    {
        float increasePerSecond = mDiamondReward / duration;
        float endValue = GameManager.PlayerManager.GetTotalCoinCount();
        float tmpValue = endValue - mDiamondReward;
        
        TextDiamondTotal.SetText(tmpValue.ToString());

        while (endValue > tmpValue)
        {
            tmpValue += Time.deltaTime * increasePerSecond;
            TextDiamondTotal.SetText(((int)tmpValue).ToString());
            
            yield return null;
        }
    }
    
    #endregion

    #region Button Events

    private void OnButtonClickedReplay()
    {
        GameManager.SoundManager.PlayClickSound(ClickSounds.Click);
        HidePanel();
        GameManager.ResetToMainMenu();
    }

    private void OnButtonClickedContinue()
    {
        GameManager.SoundManager.PlayClickSound(ClickSounds.Click);
        HidePanel();
        GameManager.ResetToMainMenu();
    }

    #endregion

    #region Events

    private void OnLevelCompleted()
    {
        mDiamondReward = Constants.LEVEL_COMPLETE_REWARD;

        FailCanvas.Close();
        SuccessCanvas.Open();
        Invoke("ShowPanel", 1f);
    }

    private void OnLevelFailed()
    {
        mDiamondReward = Constants.LEVEL_FAIL_REWARD;
        
        SuccessCanvas.Close();
        FailCanvas.Open();
        Invoke("ShowPanel", 1f);
    }

	private void OnDestroy()
	{
        if(GameManager != null)
        {
            GameManager.OnLevelCompleted -= OnLevelCompleted;
            GameManager.OnLevelFailed -= OnLevelFailed;
        }
	}

	#endregion
}
