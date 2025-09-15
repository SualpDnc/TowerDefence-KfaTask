using UnityEngine;
using UnityEngine.UI;

public class PerformanceHUD : MonoBehaviour
{
    public Text fpsText;
    public Text countsText;

    float deltaTime = 0f;

    void Update()
    {
        // FPS hesap
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        if (fpsText != null)
            fpsText.text = $"FPS: {fps:0}";

        // aktif animator ve düşman sayısı
        var animators = FindObjectsOfType<Animator>();
        var enemies = FindObjectsOfType<Enemy>();
        if (countsText != null)
            countsText.text = $"Animators: {animators.Length} | Enemies: {enemies.Length}";
    }
}