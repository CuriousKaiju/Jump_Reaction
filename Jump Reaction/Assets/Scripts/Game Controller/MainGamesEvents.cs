using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class MainGamesEvents
{
    public static event Action<int> ActionPlayerWin;

    public static event Action ActionPlayerLose;
    public static event Action<int> ActionCollectCoin;
    public static event Action ActionPlayerSpentJump;
    public static event Action ActionSaveLevelStatus;
    public static event Action ActionPlayerLastJump;

    public static event Action ActionPlayerHaveNoJumps;
    public static event Action ActionAddAdditionalJumps;

    public static void OnActionCollectCoin(int coinValue)
    {
        ActionCollectCoin?.Invoke(coinValue);
    }

    public static void OnActionPlayerLose()
    {
        ActionPlayerLose?.Invoke();
    }
    public static void OnActionPlayerWin(int starValue)
    {
        ActionPlayerWin?.Invoke(starValue);
    }
    public static void OnActionPlayerSpentJump()
    {
        ActionPlayerSpentJump?.Invoke();
    }
    public static void OnActionPlayerLastJump()
    {
        ActionPlayerLastJump?.Invoke();
    }
    public static void OnActionSaveLevelStatus()
    {
        ActionSaveLevelStatus?.Invoke();
    }
    public static void OnActionPlayerHaveNoJumps()
    {
        ActionPlayerHaveNoJumps?.Invoke();
    }
    public static void OnActionAddAdditionalJumps()
    {
        ActionAddAdditionalJumps?.Invoke();
    }
}
