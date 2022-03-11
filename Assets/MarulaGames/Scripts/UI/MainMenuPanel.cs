using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class MainMenuPanel : UIPanel, IPointerDownHandler, IPointerUpHandler
{
    public CustomButton ButtonPlay;
    public CustomButton ButtonSkins;
    public CustomButton ButtonRemoveAds;
    public CustomButton ButtonSettings;
    public CustomButton ButtonPurchaseCoin;

    public event Action OnButtonClickedAlertTutor;

    [Header("Play Button Options")] 
    public TMP_Text TextPlayButton;
    
    [Space(10)]
    public TMP_Text LevelNumber;
    public TMP_Text CoinCount;

    private TMP_InputField mNameInputField;

    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);

        ButtonPlay.Initialize(uiManager, OnButtonClickedPlay);
        ButtonSkins.Initialize(uiManager, OnButtonClickedSkins,false);
        ButtonSettings.Initialize(uiManager, OnButtonClickedSettings);
        ButtonRemoveAds.Initialize(uiManager, OnButtonClickedRemoveAds,false);
        ButtonPurchaseCoin.Initialize(uiManager, OnButtonClickedPurchaseCoin);

        InitializeCustomOptions();

        GameManager.OnResetToMainMenu += OnResetToMainMenu;
    }

    private void InitializeCustomOptions()
    {
        switch (GameManager.GameOptions.GameMechanic)
        {
            case GameMechanics.Swipe:
                SetPlayButtonToSwipe();
                break;

            case GameMechanics.Tap:
            case GameMechanics.TapOnTime:
            case GameMechanics.Other:
                SetPlayButtonToTap();
                break;
        }
    }

    private void SetPlayButtonToSwipe()
    {
        TextPlayButton.SetText(Constants.SWIPE_TO_PLAY);
        TextPlayButton.raycastTarget = false;
        ButtonPlay.interactable = false;
        ButtonPlay.Image.raycastTarget = false;
    }

    private void SetPlayButtonToTap()
    {
        TextPlayButton.SetText(Constants.TAP_TO_PLAY);
        TextPlayButton.raycastTarget = true;
        ButtonPlay.interactable = true;
        ButtonPlay.Image.raycastTarget = true;
        TextPlayButton.transform.DOScale(1.05f, 0.2f).SetLoops(-1, LoopType.Yoyo).From(1f);
    }

	public override void ShowPanel()
	{
        GameManager.InputManager.OnSwiped += OnSwiped;

        UpdateLabels();
		base.ShowPanel();
	}

	public override void HidePanel()
	{
        GameManager.InputManager.OnSwiped -= OnSwiped;

        base.HidePanel();
	}

    public void SetNameInputFieldTo(TMP_InputField field)
    {
        mNameInputField = field;
    }

	public override void UpdateLabels()
    {
        base.UpdateLabels();
        LevelNumber.SetText("LEVEL " + GameManager.LevelManager.CurrentLevelNumber);
        CoinCount.SetText(GameManager.PlayerManager.GetTotalCoinCount().ToString());

        GameManager.GameOptions.GetName();
    }

    private void UpdatePlayerName()
    {
        if (mNameInputField != null)
        {
            var tmpName = mNameInputField.text;

            if (!string.IsNullOrEmpty(tmpName))
            {
                GameManager.PlayerManager.UpdateNameData(tmpName);
            }
        }
    }

    private void CheckTutorial()
    {
        if(GameManager.LevelManager.CurrentLevelNumber > 1)
        {
            GameManager.StartGame();
        }
        else
        {
            GameManager.StartGame();
            //UIManager.GetPanel(Panels.Tutorial).ShowPanel();
        }
    }

	#region Button Events

	private void OnButtonClickedPlay()
    {
        GameManager.SoundManager.PlayClickSound(ClickSounds.Click);
        UpdatePlayerName();
        CheckTutorial();
        HidePanel();
        if (OnButtonClickedAlertTutor != null)
        {
            OnButtonClickedAlertTutor();
        }
    }

    private void OnButtonClickedSettings()
    {
        GameManager.SoundManager.PlayClickSound(ClickSounds.Click);
        UIManager.GetPanel(Panels.Settings).ShowPanel();
    }

    private void OnButtonClickedSkins()
    {
        GameManager.SoundManager.PlayClickSound(ClickSounds.Click);
        //UIManager.GetPanel(Panels.Skins).ShowPanel();
        HidePanel();
    }

    private void OnButtonClickedRemoveAds()
    {
        Debug.Log("Remove Ads");
    }

    private void OnButtonClickedPurchaseCoin()
    {
        
    }

    #endregion

    #region Events
    
    public void OnPointerDown(PointerEventData eventData)
    {
        UIManager.GameManager.InputManager.Touched(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UIManager.GameManager.InputManager.TouchEnd(eventData);
    }

    private void OnResetToMainMenu()
    {
        ShowPanel();
    }

    private void OnSwiped()
    {
        OnButtonClickedPlay();
    }

    #endregion
}
