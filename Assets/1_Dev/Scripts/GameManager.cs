using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameStateEnum GameState => myGameState;

    public int StoryProgress;
    public List<string> TemporaryInventory; 
    public bool Reset;

    [SerializeField] private PlayerController playerController;
    private GameStateEnum myGameState;

#region Lifecycle
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SaveSystem.Instance.LoadGame();
        ChangeGameState(GameStateEnum.Start);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SaveSystem.Instance.ResetGameData();
        }
    }

#endregion    

    public void ChangeGameState(GameStateEnum gameState)
    {
        myGameState = gameState;
        switch (myGameState)
        {
            case GameStateEnum.Start:
                GameStateEvents.TriggerGameStart();
                break;
            case GameStateEnum.Play:
                GameStateEvents.TriggerGamePlay();
                break;
            case GameStateEnum.Pause:
                GameStateEvents.TriggerGamePause();
                break;
            case GameStateEnum.End:
                GameStateEvents.TriggerGameEnd();
                break;
            case GameStateEnum.Checkpoint:
                GameStateEvents.TriggerGameCheckpoint();
                break;
            case GameStateEnum.Die:
                GameStateEvents.TriggerDie();
                break;
        }
    }

    public void OnQuestCompleted()
    {
        InventoryController.Instance.InventorySave();
        SaveSystem.Instance.SaveGame();
    }
}
