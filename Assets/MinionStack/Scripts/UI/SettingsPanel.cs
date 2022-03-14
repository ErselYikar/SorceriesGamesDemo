using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsPanel : UIPanel
{
    public CustomButton ButtonClose;
    public CustomToggle ToggleSound;
    public CustomToggle ToggleVibration;

    private Image mToggleSoundBg;
    private Image mToggleVibrationBg;

    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);

        ButtonClose.Initialize(uiManager, OnButtonClickedClose);
        ToggleSound.Initialize(uiManager, OnToggleClickedSound);
        ToggleVibration.Initialize(uiManager, OnToggleClickedVibration);

        mToggleSoundBg = ToggleSound.GetComponentInChildren<Image>();
        mToggleVibrationBg = ToggleVibration.GetComponentInChildren<Image>();
    }

	public override void ShowPanel()
	{
        InitializeToggles();
		base.ShowPanel();
	}

	public override void HidePanel()
	{
        base.HidePanel();
	}

    private void InitializeToggles()
    {
        ToggleSound.isOn = GameManager.SoundManager.IsSoundOn;
        ToggleVibration.isOn = GameManager.VibrationsManager.IsVibrationOn;

        mToggleSoundBg.enabled = !ToggleSound.isOn;
        mToggleVibrationBg.enabled = !ToggleVibration.isOn;
    }


	#region Button Events

	private void OnButtonClickedClose()
    {
        GameManager.SoundManager.PlayClickSound(ClickSounds.Click);
        HidePanel();
    }

    private void OnToggleClickedSound()
    {
        GameManager.SoundManager.PlayClickSound(ClickSounds.Click);
        GameManager.SoundManager.ToggleSound();

        mToggleSoundBg.enabled = !ToggleSound.isOn;
    }

    private void OnToggleClickedVibration()
    {
        GameManager.SoundManager.PlayClickSound(ClickSounds.Click);
        GameManager.VibrationsManager.ToggleVibration();

        mToggleVibrationBg.enabled = !ToggleVibration.isOn;
    }

    #endregion
}
