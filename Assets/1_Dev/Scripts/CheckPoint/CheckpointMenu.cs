using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Checkpoint menüsü
public class CheckpointMenu : MonoBehaviour
{
    public static List<Checkpoint> ActivatedCheckpoints = new List<Checkpoint>(); // Aktif checkpoint'lerin listesi.
    
    [SerializeField] private GameObject buttonPrefab; 
    [SerializeField] private Transform contentParent; 
    
    #region lifecycle
   private void OnEnable()
    {
        GameStateEvents.OnCehckpoint += Cehckpoint;
        GameStateEvents.OnDie += Die;
    }

    private void OnDisable() {
        GameStateEvents.OnCehckpoint -= Cehckpoint;
        GameStateEvents.OnDie -= Die;
    }
    #endregion
    private void Cehckpoint()
    {
        // Önce mevcut butonları temizle
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Checkpoint checkpoint in ActivatedCheckpoints){
            GameObject newButton = Instantiate(buttonPrefab, contentParent);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = "Checkpoint";
            newButton.GetComponent<Button>().onClick.AddListener(() => TeleportToCheckpoint(checkpoint.transform.position));
        }
    }

    private void Die()
    {
        int count = ActivatedCheckpoints.Count - 1;

        DOVirtual.DelayedCall(0.2f, () => 
        {
            TeleportToCheckpoint(ActivatedCheckpoints[count].transform.position);
        });
    }

    // Checkpoint'e git
    private void TeleportToCheckpoint(Vector3 position)
    {
        GameObject player = PlayerController.Instance.gameObject;
        player.transform.position = position;
        PlayerController.Instance.PlayerPosition = position;
    }
}
