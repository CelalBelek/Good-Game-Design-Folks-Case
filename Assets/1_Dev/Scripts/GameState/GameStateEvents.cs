using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class GameStateEvents
{
    public static event Action OnGameStart;
    public static event Action OnGamePlay;
    public static event Action OnGamePause;
    public static event Action OnGameEnd;
    public static event Action OnCehckpoint;
    public static event Action OnDie;

    public static void TriggerGameStart() => OnGameStart?.Invoke();
    public static void TriggerGamePlay() => OnGamePlay?.Invoke();
    public static void TriggerGamePause() => OnGamePause?.Invoke();
    public static void TriggerGameEnd() => OnGameEnd?.Invoke();
    public static void TriggerGameCheckpoint() => OnCehckpoint?.Invoke();
    public static void TriggerDie() => OnDie?.Invoke();
}
