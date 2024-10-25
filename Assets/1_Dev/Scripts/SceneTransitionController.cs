using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour
{
    public static SceneTransitionController Instance;
#region LifeCycle
    private void Awake()
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
#endregion
    // İki sahne arasında geçiş yap
    public void ToggleScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
