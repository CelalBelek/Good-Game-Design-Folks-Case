using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset; 

    void Update()
    {
        // Kamerayı karakterin pozisyonuna sabitle, offset ile ayarla.
        transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, offset.z);
    }
}
