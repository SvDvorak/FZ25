using System;
using UnityEngine;

public static class Events
{
	public static event Action OnPlayerDied;
	public static event Action OnGameStarted;

	public static void PlayerDied() => OnPlayerDied?.Invoke();
	public static void GameStarted() => OnGameStarted?.Invoke();
}
