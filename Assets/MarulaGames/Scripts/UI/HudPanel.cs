using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class HudPanel : UIPanel, IPointerDownHandler, IPointerUpHandler
{
    public TMP_Text CoinCount;

    [Header("INPUT")]
    public GuideTypes GuideType = GuideTypes.None;
    public List<GameObject> SwipeGuides;

    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);

        GameManager.OnStartGame += OnStartGame;
        GameManager.OnGameFinished += OnGameFinished;
        GameManager.OnResetToMainMenu += OnResetToMainMenu;
    }

    public override void ShowPanel()
    {
        ActivateGuide();
        UpdateLabels();
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void UpdateLabels()
    {
        base.UpdateLabels();
        CoinCount.SetText(GameManager.PlayerManager.GetTotalCoinCount().ToString());
    }

    private void ActivateGuide()
    {
        SwipeGuides.ForEach(x => x.SetActive(false));
        SwipeGuides[(int)GuideType].SetActive(true);
    }

    public void CloseGuide()
    {
        if (SwipeGuides[(int)GuideType].activeInHierarchy)
        {
            SwipeGuides[(int)GuideType].SetActive(false);
        }
    }

    #region Events

    public void OnPointerDown(PointerEventData eventData)
    {
        if (UIManager.GameManager.InputManager.Interactable)
        {
            UIManager.GameManager.InputManager.Touched(eventData);
            CloseGuide();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UIManager.GameManager.InputManager.TouchEnd(eventData);
    }

    private void OnStartGame()
    {
        ShowPanel();
    }

    private void OnGameFinished()
    {
        HidePanel();
    }

    private void OnResetToMainMenu()
    {
        HidePanel();
    }

    
    private void OnDestroy()
	{
        if(GameManager != null)
        {
            GameManager.OnStartGame -= OnStartGame;
            GameManager.OnGameFinished -= OnGameFinished;
            GameManager.OnResetToMainMenu -= OnResetToMainMenu;
        }
    }

    #endregion
}
