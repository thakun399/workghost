using UnityEngine;

public class GhostAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject projectilePrefab; // Prefab ของกระสุน
    public Transform shootPoint; // ตำแหน่งที่ยิงออกมา

    [Header("Shooting Settings")]
    public float shootInterval = 5f; // ยิงทุก 5 วินาที
    private float shootTimer = 0f;

    [Header("Raycast Settings")]
    public LayerMask playerLayer;
    public float detectionRange = 50f;

    [Header("Visual Feedback")]
    public LineRenderer aimLine; // เส้นเล็งมายัง Player (Optional)
    public float lineDuration = 0.1f;

    void Start()
    {
        // หา Player อัตโนมัติ
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        shootTimer = shootInterval; // ยิงครั้งแรกหลัง 5 วิ
    }

    void Update()
    {
        if (player == null) return;

        // หมุนหาผู้เล่นตลอดเวลา
        LookAtPlayer();

        // ยิง Raycast หา Player ตลอดเวลา
        CheckPlayerWithRaycast();

        // นับเวลายิงกระสุน
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            ShootProjectile();
            shootTimer = shootInterval; // รีเซ็ต
        }
    }

    void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // ไม่ให้เอียงขึ้น-ลง
        
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void CheckPlayerWithRaycast()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, directionToPlayer);
        RaycastHit hit;

        // วาดเส้น Debug
        Debug.DrawRay(transform.position, directionToPlayer * detectionRange, Color.red);

        if (Physics.Raycast(ray, out hit, detectionRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                // เจอ Player!
                Debug.Log("Ghost sees Player!");
                
                // แสดงเส้นเล็ง (ถ้ามี LineRenderer)
                if (aimLine != null)
                {
                    aimLine.enabled = true;
                    aimLine.SetPosition(0, transform.position);
                    aimLine.SetPosition(1, player.position);
                }
            }
        }
    }

    void ShootProjectile()
    {
        if (projectilePrefab == null || player == null) return;

        // สร้างกระสุน
        Vector3 spawnPos = shootPoint != null ? shootPoint.position : transform.position;
        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

        // คำนวณทิศทาง
        Vector3 direction = (player.position - spawnPos).normalized;

        // ตั้งค่าทิศทางให้กระสุน
        Projectile projectileScript = proj.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetDirection(direction);
        }

        Debug.Log("Ghost Shot Projectile!");
    }

    void OnDrawGizmosSelected()
    {
        // วาดระยะตรวจจับใน Scene View
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}