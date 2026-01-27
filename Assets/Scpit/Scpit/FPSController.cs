using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform playerCamera;
    public float mouseSensitivity = 2f;
    public float verticalLookLimit = 80f; // จำกัดการมองขึ้น-ลง

    private float rotationX = 0f; // มุมมองขึ้น-ลง

    void Start()
    {
        // ล็อคเมาส์ไว้กลางจอ
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // รับค่าการเคลื่อนที่ของเมาส์
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // หมุนตัวละคร (ซ้าย-ขวา 360°)
        transform.Rotate(Vector3.up * mouseX);

        // หมุนกล้อง (ขึ้น-ลง)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -verticalLookLimit, verticalLookLimit);
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        // ปลดล็อคเมาส์เมื่อกด ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}