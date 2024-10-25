using UnityEngine;

public interface ICollectible
{
    void Collect(Transform otherTransform); // Toplanma davranışını tanımlar
    Transform ItemTransform { get; } 
}
