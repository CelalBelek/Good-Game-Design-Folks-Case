using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;
    public List<Item> Inventory = new List<Item>();
    public List<Item> CurrentInventory = new List<Item>();

    [SerializeField] private GameObject ItemPrefab;
    [SerializeField] private Transform playerTransform;

#region lifeCycle
    private void Awake() {
        if (Instance == null)
            Instance = this;
    }

    private void OnEnable() {
        GameStateEvents.OnGamePlay += GamePlay;
        GameStateEvents.OnDie += Die;
    }

    private void OnDisable() {
        GameStateEvents.OnGamePlay -= GamePlay;
        GameStateEvents.OnDie -= Die;
    }
#endregion

    public void InventoryAdd(Item item){
        Inventory.Add(item);
    }

    public void CurrentInventoryAdd(Item item){
        CurrentInventory.Add(item);
    }

    public List<Item> CurrentInventoryRemove(){
        List<Item> temp = new List<Item>();
        foreach (var item in CurrentInventory)
        {
            temp.Add(item);
        }
        CurrentInventory.Clear();

        return temp;
    }

    public void InventorySave(){
        if (CurrentInventory.Count <= 0)
            return;

        foreach (var item in CurrentInventory)
        {
            Inventory.Add(item);
        }
        
        CurrentInventory.Clear();
    }

    public void GamePlay(){
        if (Inventory.Count <= 0)
            return;

        playerTransform = PlayerController.Instance.transform;

        foreach (var item in Inventory)
        {
            GameObject newItem = Instantiate(ItemPrefab, transform);
            newItem.GetComponent<ICollectible>().Collect(playerTransform);
        }
    }

    private void Die(){
        foreach (var item in CurrentInventory)
        {
            item.GetComponent<Item>().Drop();
        }
        CurrentInventory.Clear();
    }
}
