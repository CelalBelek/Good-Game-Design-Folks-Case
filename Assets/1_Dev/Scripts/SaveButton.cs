using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : MonoBehaviour
{
    private void Start() {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(SaveGame);
    }

    void SaveGame()
    {
        GameManager.Instance.OnQuestCompleted();
    }
}
