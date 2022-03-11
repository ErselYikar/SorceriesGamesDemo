using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class UIManager : CustomBehaviour
{
    public UIPanel CurrentUIPanel { get; set; }
    public List<UIPanel> UIPanels;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        UIPanels.ForEach(x =>
        {
            x.Initialize(this);
            x.gameObject.SetActive(false);
        });

        GetPanel(Panels.MainMenu).ShowPanel();
    }

    public void SetCurrentUIPanel(UIPanel uiPanel)
    {
        CurrentUIPanel = uiPanel;
    }

    public void SetCurrentUIPanel(Panels panel)
    {
        var tempPanel = GetPanel(panel);
        CurrentUIPanel = tempPanel;
    }

    public UIPanel GetPanel(Panels panel)
    {
        return UIPanels[(int)panel];
    }

    public void ShowBroadcastMessageTo(string message, MessageAnimation animation)
    {
        ((MessageBroadcastPanel)GetPanel(Panels.MessageBroadccast)).OutputMessage(message, animation);
    }
}
