using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteConfigManager : CustomBehaviour
{
    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        InitializeRemoteConfigs();
    }

    private void InitializeRemoteConfigs()
    {
        //Example Elephant Remote Config Usage.
        //int foo = RemoteConfig.GetInstance().GetInt(RemoteConfigKeys.REMOTE_FOO, DefaultConfigs.DEFAULT_FOO);
        //string foo1 = RemoteConfig.GetInstance().Get(RemoteConfigKeys.REMOTE_FOO, DefaultConfigs.DEFAULT_FOO);
    }
}
