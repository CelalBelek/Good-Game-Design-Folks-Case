using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateButton : MonoBehaviour
{
    [SerializeField] GameStateEnum gameState;

    private Button myButton;

    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(ChangeGameState);
    }

    public void ChangeGameState()
    {
        GameManager.Instance.ChangeGameState(gameState);
    }
}
