using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InputManager : CustomBehaviour
{
    public event Action OnTouched; // Returns input event to screen
    public event Action OnTouchEnd;
    public event Action OnSwiped;

    public bool Interactable = false;
    public float MinimumSwipeDistanceInViewportPoint;
    public bool IsUIOverride { get;  private set; }

    private Vector2 mPointerDownLocation;
    private Vector2 mPointerUpLocation;

    private RaycastHit mHit;
    private Ray mRay;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        
        GameManager.OnStartGame += OnStartGame;
        GameManager.OnRestartGame += OnRestartGame;
        GameManager.OnResumeGame += OnResumeGame;
        GameManager.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.OnCountdownFinished += OnCountdownFinished;
    }

	private void Update()
	{
        UpdateUIOverride();
        UpdateInput();
	}

    private void UpdateInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Interactable && !IsUIOverride && GameManager.GameOptions.CanUseTap())
            {
                mRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(mRay, out mHit))
                {
                    //  TODO : control object with tag
                    //if (mHit.collider.tag == "X")
                    //{
                    //    // TODO : call function
                    //}
                }
            }
        }
    }

    private void UpdateUIOverride()
    {
        IsUIOverride = EventSystem.current.IsPointerOverGameObject();
    }

	#region Events

	public void Touched()
    {
        if (GameManager.GameOptions.GameMechanic == GameMechanics.Tap && Interactable && OnTouched != null)
        {
            OnTouched();
        }
    }

    public void TouchEnd()
    {
        if (OnTouchEnd != null)
        {
            OnTouchEnd();
        }
    }

    public void Touched(PointerEventData eventData)
    {
        mPointerDownLocation = MainCamera.ScreenToViewportPoint(eventData.pressPosition);
    }

    public void TouchEnd(PointerEventData eventData)
    {
        mPointerUpLocation = MainCamera.ScreenToViewportPoint(eventData.position);
        float distance = Vector2.Distance(mPointerDownLocation, mPointerUpLocation);

        ControlSwipe(distance);
    }

    private void ControlSwipe(float distance)
    {
        if (GameManager.GameOptions.GameMechanic != GameMechanics.Swipe) return;

        if (distance >= MinimumSwipeDistanceInViewportPoint)
        {
            if (OnSwiped != null)
            {
                OnSwiped();
            }
        }
    }

    private void OnStartGame()
    {
        if (GameManager.GameOptions.UseCountdown) return;

        Interactable = true;
    }

    private void OnLevelPassed()
    {
        Interactable = true;
    }

    private void OnRestartGame()
    {
        Interactable = true;
    }

    private void OnResumeGame()
    {
        Interactable = true;
    }

    private void OnResetToMainMenu()
    {
        Interactable = false;
    }

    private void OnCountdownFinished()
    {
        Interactable = true;
    }

	private void OnDestroy()
	{
        if(GameManager != null)
        {
            GameManager.OnStartGame -= OnStartGame;
            GameManager.OnRestartGame -= OnRestartGame;
            GameManager.OnResumeGame -= OnResumeGame;
            GameManager.OnResetToMainMenu -= OnResetToMainMenu;
            GameManager.OnCountdownFinished -= OnCountdownFinished;
        }
	}

	#endregion
}
