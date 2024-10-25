using TMPro;
using UnityEngine;
public class HealthReductionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
#region LifeCycle
    private void OnEnable() 
    {
        GameStateEvents.OnGamePlay += HealthWrite;
    }
    private void OnDisable() {
        GameStateEvents.OnGamePlay -= HealthWrite;
    }
    private void Start() {
        HealthWrite();
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Health);
    }
#endregion
    void Health()
    {
        PlayerController.Instance.Health -= 10;
        HealthWrite();
    }

    private void HealthWrite()
    {
        healthText.text = "Player Health: " + PlayerController.Instance.GetHealth().ToString();
    }
}