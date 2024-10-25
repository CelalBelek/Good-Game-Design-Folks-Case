using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateControl : MonoBehaviour
{
    [SerializeField] private GameStateEnum gameState;

    [SerializeField] private GameObject[] startObjects;
    [SerializeField] private GameObject[] playObjects;
    [SerializeField] private GameObject[] pauseObjects;
    [SerializeField] private GameObject[] endObjects;
    [SerializeField] private GameObject[] cehckpointObjects;
    [SerializeField] private GameObject[] dieObjects;

    private void OnEnable() {
        SubscribeToEvents(true);
    }

    private void OnDisable() {
        SubscribeToEvents(false);
    }

    // Tek seferde tüm eventleri abone ol veya abonelikten çıkar
    private void SubscribeToEvents(bool subscribe)
    {
        if (subscribe)
        {
            GameStateEvents.OnGameStart += GameStart;
            GameStateEvents.OnGamePlay += GamePlay;
            GameStateEvents.OnGamePause += GamePause;
            GameStateEvents.OnGameEnd += GameEnd;
            GameStateEvents.OnCehckpoint += Checkpoint;
            GameStateEvents.OnDie += Die;
        }
        else
        {
            GameStateEvents.OnGameStart -= GameStart;
            GameStateEvents.OnGamePlay -= GamePlay;
            GameStateEvents.OnGamePause -= GamePause;
            GameStateEvents.OnGameEnd -= GameEnd;
            GameStateEvents.OnCehckpoint -= Checkpoint;
            GameStateEvents.OnDie -= Die;
        }
    }

    private void GameStart() {
        SetActiveObjects(startObjects);
    }

    private void GamePlay() {
        SetActiveObjects(playObjects);
    }

    private void GamePause() {
        SetActiveObjects(pauseObjects);
    }

    private void GameEnd() {
        SetActiveObjects(endObjects);
    }

    private void Checkpoint() {
        SetActiveObjects(cehckpointObjects);
    }

    private void Die() {
        SetActiveObjects(dieObjects);
    }

    // Aktif olacak object array'i alır ve diğerlerini devre dışı bırakır
    private void SetActiveObjects(GameObject[] activeObjects) {
        SetObjectsActive(startObjects, activeObjects == startObjects);
        SetObjectsActive(playObjects, activeObjects == playObjects);
        SetObjectsActive(pauseObjects, activeObjects == pauseObjects);
        SetObjectsActive(endObjects, activeObjects == endObjects);
        SetObjectsActive(cehckpointObjects, activeObjects == cehckpointObjects);
        SetObjectsActive(dieObjects, activeObjects == dieObjects);

    }

    // Nesneleri toplu olarak aktif veya pasif yap
    private void SetObjectsActive(GameObject[] objects, bool isActive) {
        foreach (var obj in objects)
        {
            obj.SetActive(isActive);
        }
    }
}
