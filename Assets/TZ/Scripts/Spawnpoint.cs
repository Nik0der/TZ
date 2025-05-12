using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    public Transform spawnPoint;

    private void Update()
    {
        if (transform.position.y < -10f) 
        {
            transform.position = spawnPoint.position;
        }
    }
}
