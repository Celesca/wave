using UnityEngine;

public class mainCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;//���С���ͧ��繼�����

    void Update()
    {
            transform.position = new Vector3(//���˹觡��ͧ
            player.position.x +  offset.x,
            offset.y,
            offset.z);
    }
}
