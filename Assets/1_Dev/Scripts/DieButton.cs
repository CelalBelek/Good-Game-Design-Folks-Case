using UnityEngine;

public class DieButton : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

     private void Start() {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Die);
    }

    void Die()
    {
        GameManager.Instance.OnQuestCompleted();
    }
}
