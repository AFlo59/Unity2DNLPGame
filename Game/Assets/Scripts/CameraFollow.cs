using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float timeOffset;
    public Vector3 posOffset;

    private Vector3 velocity;
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.transform.position.x + posOffset.x, player.transform.position.y + posOffset.y, posOffset.z), ref velocity, timeOffset);
    }
}
