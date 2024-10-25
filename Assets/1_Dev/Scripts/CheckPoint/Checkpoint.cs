using System.Collections.Generic;
using UnityEngine;

// Checkpoint classı, checkpointlerin işlevlerini ve özelliklerini içerir.
public class Checkpoint : MonoBehaviour
{
    public bool Transition;
    public int LoadSceneIndex;

    [SerializeField] private bool isActivated = false;
    [SerializeField] private List<GameObject> itemList = new List<GameObject>(); // Gizlenecek itemler

#region Lifecycle
    void OnEnable()
    {
        GameStateEvents.OnGameStart += GameStart;
    }

     void OnDisable()
    {
        GameStateEvents.OnGameStart -= GameStart;
    }
#endregion
    
    // Checkpointin aktif olup olmadığını kontrol eder.
    public void Check()
    {
        if (!isActivated)
        {
            isActivated = true;
            CheckpointMenu.ActivatedCheckpoints.Add(this);
        }
        
        GameManager.Instance.OnQuestCompleted();

        if (Transition)
        {
            SceneTransition.Instance.StartTransition();
            SceneTransitionController.Instance.ToggleScene(LoadSceneIndex);
            SaveSystem.Instance.ClearCheckpointDataInSave();
        }
    }

    // Oyun başladığında checkpointin aktif olup olmadığını kontrol eder.
    public void GameStart(){
        if (CheckpointMenu.ActivatedCheckpoints.Contains(this)){
            isActivated = true;

            if (itemList.Count > 0)
            {
                foreach (GameObject item in itemList)
                {
                    item.SetActive(false); 
                }
            }
        }
    }
}
