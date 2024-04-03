using UnityEngine;

public class mainCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;//ระยะการมองเห็นผู้เล่น

    void Update()
    {
            transform.position = new Vector3(//ตำแหน่งกล้อง
            player.position.x +  offset.x,
            offset.y,
            offset.z);
    }
}
