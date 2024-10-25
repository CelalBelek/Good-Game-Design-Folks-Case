using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Vector3 Position;
    public int Health;
    public List<Item> Inventory = new List<Item>();
    public List<Checkpoint> ActivatedCheckpoints = new List<Checkpoint>();
    public int StoryProgress; // Hikâye ilerlemesi için bir örnek değişken
}