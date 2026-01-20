using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float speed = 10f;
    public int damage = 1;
    public float lifetime = 5f; // ทำลายตัวเองหลัง 5 วิ

    private Vector3 direction;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // เคลื่อนที่ไปตามทิศทาง
        transform.position += direction * speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
        
        // หมุน Projectile ไปทางที่ยิง (ถ้าต้องการ)
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // ถ้าเป็นกระสุนที่สะท้อนแล้ว ไม่ทำร้าย Player
        if (gameObject.tag == "ReflectedProjectile")
        {
            // โดนผีเท่านั้น
            if (other.CompareTag("Ghost"))
            {
                // ทำให้ผีตาย (ต้องมี Script GhostHealth)
                GhostHealth ghostHealth = other.GetComponent<GhostHealth>();
                if (ghostHealth != null)
                {
                    ghostHealth.TakeDamage(damage);
                }
                Destroy(gameObject);
            }

            // โดนกำแพง
            if (other.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // กระสุนปกติ - โดน Player
            if (other.CompareTag("Player"))
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
                Destroy(gameObject);
            }

            // โดนกำแพง
            if (other.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }
        }
    }
}