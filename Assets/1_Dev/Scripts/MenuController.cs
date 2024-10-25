using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject CheckpointMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckpointMenu.SetActive(!CheckpointMenu.activeSelf);
        }
    }
}
