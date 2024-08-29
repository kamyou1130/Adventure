using UnityEngine;

public class PlayerStartPosition : MonoBehaviour
{
    public Vector3 startPosition;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = startPosition;
        }
        else
        {
            Debug.LogWarning("Player object with 'Player' tag not found in the scene.");
        }
    }
}
