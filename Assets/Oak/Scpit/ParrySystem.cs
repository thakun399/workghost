using UnityEngine;

public class ParrySystem : MonoBehaviour
{
    [Header("Parry Settings")]
    public GameObject parryCollider; // Sphere หรือ Cube ที่จะเป็น Collider
    public float parryDuration = 0.3f; // Parry เปิด 0.3 วินาที
    public float parryCooldown = 0.5f; // คูลดาวน์ 0.5 วินาที
    
    [Header("Reflection Settings")]
    public Transform playerCamera; // ใช้เล็งทิศทางสะท้อน
    public float reflectSpeed = 15f; // ความเร็วขี้สะท้อน

    [Header("Visual Feedback")]
    public ParticleSystem parryEffect; // Effect เมื่อ Parry สำเร็จ
    public AudioClip parrySound; // เสียง Parry

    private bool canParry = true;
    private bool isParrying = false;
    private AudioSource audioSource;

    void Start()
    {
        // ปิด Collider ตอนเริ่มเกม
        if (parryCollider != null)
        {
            parryCollider.SetActive(false);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // กด Spacebar หรือ Click ซ้ายเพื่อ Parry
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && canParry)
        {
            StartParry();
        }
    }

    void StartParry()
    {
        isParrying = true;
        canParry = false;

        Debug.Log("PARRY ACTIVATED!");

        // เปิด Collider
        if (parryCollider != null)
        {
            parryCollider.SetActive(true);
        }

        // เล่น Effect
        if (parryEffect != null)
        {
            parryEffect.Play();
        }

        // เล่นเสียง
        if (audioSource != null && parrySound != null)
        {
            audioSource.PlayOneShot(parrySound);
        }

        // ปิด Collider หลังจาก parryDuration
        Invoke(nameof(EndParry), parryDuration);
    }

    void EndParry()
    {
        isParrying = false;

        // ปิด Collider
        if (parryCollider != null)
        {
            parryCollider.SetActive(false);
        }

        Debug.Log("Parry Ended");

        // เปิดคูลดาวน์
        Invoke(nameof(ResetCooldown), parryCooldown);
    }

    void ResetCooldown()
    {
        canParry = true;
        Debug.Log("Parry Ready!");
    }

    // เมื่อ Projectile โดน Parry Collider
    void OnTriggerEnter(Collider other)
    {
        if (!isParrying) return;

        // ตรวจสอบว่าเป็น Projectile มั้ย
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile != null)
        {
            Debug.Log("PARRY SUCCESS! Reflecting projectile!");

            // คำนวณทิศทางสะท้อนตาม Crosshair (กล้อง)
            Vector3 reflectDirection = playerCamera.forward;

            // เปลี่ยนทิศทางและความเร็วของ Projectile
            projectile.SetDirection(reflectDirection);
            projectile.speed = reflectSpeed;

            // เปลี่ยน Tag เป็น "ReflectedProjectile" เพื่อไม่ให้ทำร้าย Player
            other.gameObject.tag = "ReflectedProjectile";

            // Effect พิเศษ (ถ้ามี)
            if (parryEffect != null)
            {
                parryEffect.Play();
            }
        }
    }

    // สำหรับ Debug - แสดงสถานะใน Inspector
    public bool IsParrying() { return isParrying; }
    public bool CanParry() { return canParry; }
}