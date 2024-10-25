using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    private string filePath;

#region lifeCycle
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

        filePath = Application.persistentDataPath + "/saveData.json";
    }
#endregion

    public void SaveGame()
    {
        if (GameManager.Instance.Reset)
        {
            ResetGameData();
            return;
        }

        PlayerData data = new PlayerData();
        data.Position = PlayerController.Instance.transform.position;
        data.Health = PlayerController.Instance.Health;
        data.Inventory = InventoryController.Instance.Inventory;
        data.ActivatedCheckpoints = CheckpointMenu.ActivatedCheckpoints;
        data.StoryProgress = GameManager.Instance.StoryProgress;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
    }

    public void LoadGame()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            PlayerController.Instance.PlayerPosition = data.Position;
            PlayerController.Instance.Health = data.Health;
            InventoryController.Instance.Inventory = data.Inventory;
            GameManager.Instance.StoryProgress = data.StoryProgress;
            CheckpointMenu.ActivatedCheckpoints = data.ActivatedCheckpoints;
        }
    }


    // Yeni eklenen: Verileri sıfırlayan fonksiyon
    public void ResetGameData()
    {
        // 1. Kaydedilmiş dosyayı sil
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        // 2. Oyundaki verileri sıfırla
        PlayerController.Instance.PlayerPosition = Vector3.zero; // Varsayılan pozisyon
        PlayerController.Instance.Health = 100; // Varsayılan sağlık
        InventoryController.Instance.Inventory.Clear(); // Envanteri temizle
        CheckpointMenu.ActivatedCheckpoints.Clear(); // Checkpoint'leri temizle
        GameManager.Instance.StoryProgress = 0; // Hikaye ilerlemesini sıfırla

        Debug.Log("Oyun verileri sıfırlandı.");
    }

    public int LoadHealth()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            return data.Health;
        }
        else
        {
            return -1; // Dosya bulunamadıysa, -1 gibi bir hata değeri dönebiliriz.
        }
    }

      // Checkpoint'leri temizleyen yeni fonksiyon
    public void ClearCheckpointDataInSave()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            // Checkpoint verisini temizle
            data.ActivatedCheckpoints.Clear();

            // Güncellenmiş veriyi JSON olarak tekrar kaydet
            json = JsonUtility.ToJson(data);
            File.WriteAllText(filePath, json);

            // Hafızadaki checkpoint listesini de temizle
            CheckpointMenu.ActivatedCheckpoints.Clear();
            Debug.Log("Kayıtlı checkpoint verileri silindi.");
        }
        else
        {
            Debug.LogError("Kayıt dosyası bulunamadı.");
        }
    }
}
