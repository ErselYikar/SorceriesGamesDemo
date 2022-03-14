using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownController : CustomBehaviour
{
    public TMP_Text CountdownText;
    private RectTransform mCountDownTransform;

    public string FinishText;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        GameManager.OnStartGame += OnStartGame;
    }

    private IEnumerator StartCountdown()
    {
        CountdownText.gameObject.SetActive(true);
        mCountDownTransform = CountdownText.GetComponent<RectTransform>();
        CountdownText.enabled = true;

        for (int i = 3; i >= 0; i--)
        {
            mCountDownTransform.localScale = Vector3.one;

            if (i == 0)
            {
                CountdownText.SetText(FinishText);
                GameManager.SoundManager.PlayGameStateSound(GameStateSounds.ReadyGo);
            }
            else
            {
                CountdownText.SetText(i.ToString());
                GameManager.SoundManager.PlayGameStateSound(GameStateSounds.Countdown);
            }

            for (var scale = 1f; scale > 0f; scale -= Time.deltaTime / 0.75f)
            {
                mCountDownTransform.localScale = new Vector3(scale, scale, scale);
                yield return null;
            }
        }

        CountdownText.enabled = false;
        GameManager.CountdownFinished();

    }

    #region Events

    private void OnStartGame()
    {
        StopCoroutine("StartCountdown");
        StartCoroutine(StartCountdown());
    }

    private void OnDestroy()
    {
       if(GameManager != null)
        {
            GameManager.OnStartGame -= OnStartGame;
        }
    }
    #endregion
}
