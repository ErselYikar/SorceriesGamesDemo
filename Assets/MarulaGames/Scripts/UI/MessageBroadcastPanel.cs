using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class MessageBroadcastPanel : UIPanel
{
    public TMP_Text TextMessage;

    private Sequence mSequence;
    
    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);
        
        mSequence = DOTween.Sequence();
    }

    public void OutputMessage(string message, MessageAnimation animation)
    {
        switch (animation)
        {
            case MessageAnimation.Pop:
                Pop(message);
                break;
            case MessageAnimation.Shrink:
                Shrink(message);
                break;
        }
    }
    
    #region Outputs

    private void Pop(string message)
    {
        mSequence.Kill();
        
        mSequence = DOTween.Sequence();
        
        ShowPanel();
        
        TextMessage.transform.localScale = Vector3.zero;
        
        TextMessage.SetText(message);
        
        mSequence.Append(TextMessage.transform.DOScale(Vector3.one, 1f));
        
        mSequence.OnComplete(HidePanel);
    }

    private void Shrink(string message)
    {
        mSequence.Kill();
        
        mSequence = DOTween.Sequence();
        
        ShowPanel();
        
        TextMessage.transform.localScale = Vector3.one;
        
        TextMessage.SetText(message);
        
        mSequence.Append(TextMessage.transform.DOScale(Vector3.zero, 1f));
        
        mSequence.OnComplete(HidePanel);
    }

    #endregion
}
