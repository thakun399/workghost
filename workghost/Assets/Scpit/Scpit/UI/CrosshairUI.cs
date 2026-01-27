using UnityEngine;
using UnityEngine.UI;

public class CrosshairUI : MonoBehaviour
{
    [Header("Crosshair Settings")]
    public Image crosshairImage;
    public Color normalColor = Color.white;
    public Color parryColor = Color.cyan; // สีเมื่อ Parry
    public float parryColorDuration = 0.3f;

    private ParrySystem parrySystem;

    void Start()
    {
        parrySystem = FindObjectOfType<ParrySystem>();
    }

    void Update()
    {
        if (parrySystem != null && crosshairImage != null)
        {
            // เปลี่ยนสี Crosshair เมื่อ Parry
            if (parrySystem.IsParrying())
            {
                crosshairImage.color = parryColor;
            }
            else
            {
                crosshairImage.color = normalColor;
            }
        }
    }
}