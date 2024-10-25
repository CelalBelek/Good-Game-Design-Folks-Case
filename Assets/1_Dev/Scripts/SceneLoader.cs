using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Sahne değiştirme metodu.
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex); // Scene indexine göre yeni sahneyi yükler.
    }
}
