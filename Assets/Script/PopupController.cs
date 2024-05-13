using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupController : MonoBehaviour
{
    public GameObject popup; // ตัวแปร GameObject สำหรับ popup
    private bool isPlayerNearby = false; // ตัวแปรเก็บสถานะว่าผู้เล่นอยู่ใกล้ popup หรือไม่

    // เมื่อมีการชนกับ Collider ของตัวละคร
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ตรวจสอบว่าเป็นตัวละครหรือไม่
        {
            isPlayerNearby = true; // ตั้งค่าให้ผู้เล่นอยู่ใกล้ popup
            popup.SetActive(true); // เปิด popup
        }
    }

    // เมื่อไม่มีการชนกับ Collider ของตัวละคร
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ตรวจสอบว่าเป็นตัวละครหรือไม่
        {
            isPlayerNearby = false; // ตั้งค่าให้ผู้เล่นไม่อยู่ใกล้ popup อีกต่อไป
            popup.SetActive(false); // ปิด popup
        }
    }

    // เมื่อมีการอัพเดตทุกๆ frame
    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Space)) // ตรวจสอบว่าผู้เล่นอยู่ใกล้ popup และกดปุ่ม Space หรือไม่
        {
            SceneManager.LoadScene(2);
        }
    }

}
