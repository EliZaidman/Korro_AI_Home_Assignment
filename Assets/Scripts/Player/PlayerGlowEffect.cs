using System.Collections.Generic;
using UnityEngine;

public class PlayerGlowEffect : MonoBehaviour
{
    public Material glowMaterial;
    public float glowDuration = 2f;
    public float glowIntensity = 2f;
    private List<Renderer> renderers = new List<Renderer>();
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();
    private float timer = 0f;
    private bool isHit = false;

    void Start()
    {
        // Get all renderers in this object and its children
        renderers.AddRange(GetComponentsInChildren<Renderer>());

        // Save the original materials for each renderer
        foreach (Renderer rend in renderers)
        {
            originalMaterials[rend] = rend.materials;
        }
    }

    private void OnEnable()
    {
        PlayerHealth.OnGettingHit += PlayerHealth_OnGettingHit;
    }
    private void OnDisable()
    {
        PlayerHealth.OnGettingHit -= PlayerHealth_OnGettingHit;
    }
    private void PlayerHealth_OnGettingHit(int obj)
    {
        Hit();
    }

    public void Hit()
    {
        isHit = true;
        foreach (Renderer rend in renderers)
        {
            Material[] glowMats = new Material[rend.materials.Length];
            for (int i = 0; i < glowMats.Length; i++)
            {
                glowMats[i] = glowMaterial;
            }
            rend.materials = glowMats;
        }
        timer = glowDuration;
    }

    void Update()
    {
        if (isHit)
        {
            float glow = Mathf.Abs(Mathf.Sin(Time.time * glowIntensity));
            glowMaterial.SetColor("_EmissionColor", new Color(glow, glow, glow));
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                foreach (Renderer rend in renderers)
                {
                    rend.materials = originalMaterials[rend];
                }
                isHit = false;
            }
        }
    }
}
