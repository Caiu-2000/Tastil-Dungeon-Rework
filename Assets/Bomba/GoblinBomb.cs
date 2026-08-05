using System.Collections;
using UnityEngine;

/// <summary>
/// Bomba de goblin estilo WoW.
/// La bomba aparece YA ENCENDIDA: apenas se activa el GameObject, la mecha
/// chisporrotea en la punta y arranca el timer. Cuando el timer llega a cero,
/// explota con daño en área.
///
/// SETUP (una sola vez, en el prefab de la bomba):
/// 1. Agregá este script al GameObject raíz de la bomba (el que tiene el mesh bombaa).
/// 2. Creá un empty child llamado "FuseTip", movelo hasta la PUNTA de la mecha
///    (donde termina el tubito metálico) y arrastralo al campo "fuseTip".
/// 3. (Opcional) Asigná prefabs de explosión y sonidos. Si no asignás nada,
///    la chispa se genera sola por código y la explosión usa una esfera de debug.
/// </summary>
public class GoblinBomb : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Empty GameObject ubicado en la punta de la mecha.")]
    public Transform fuseTip;

    [Tooltip("Prefab opcional de VFX de explosión. Si está vacío se usa un flash simple generado por código.")]
    public GameObject explosionVFXPrefab;

    [Tooltip("Material opcional para las chispas (con textura). Vacío = material aditivo generado por código.")]
    public Material sparkMaterial;

    [Header("Mecha / Timer")]
    [Tooltip("Segundos desde que la bomba se enciende hasta que explota.")]
    public float fuseTime = 3.5f;

    [Tooltip("Si está activo, la bomba se enciende sola en OnEnable (el goblin la saca ya prendida).")]
    public bool igniteOnEnable = true;

    [Header("Explosión")]
    public float explosionRadius = 3f;
    public float explosionDamage = 40f;

    [Tooltip("Capas afectadas por el daño (ej: Player).")]
    public LayerMask damageLayers = ~0;

    [Tooltip("Fuerza física aplicada a rigidbodies cercanos.")]
    public float explosionForce = 500f;

    [Header("Audio (opcional)")]
    public AudioClip fuseLoopSound;   // sssss de la mecha
    public AudioClip explosionSound;

    [Header("Chispa (si se genera por código)")]
    public Color sparkColorHot = new Color(1f, 0.95f, 0.55f, 1f);
    public Color sparkColorCold = new Color(0.9f, 0.25f, 0.05f, 0f);
    public float sparkEmissionRate = 30f;

    // --- estado interno ---
    private bool isLit;
    private bool hasExploded;
    private ParticleSystem sparkPS;
    private Light flickerLight;
    private AudioSource fuseAudioSource;

    void OnEnable()
    {
        hasExploded = false;
        if (igniteOnEnable)
        {
            Ignite();
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();
        isLit = false;
    }

    /// <summary>
    /// Enciende la mecha. Llamalo desde el goblin si preferís controlar el momento
    /// exacto (por ejemplo, sincronizado con un evento de animación).
    /// </summary>
    public void Ignite()
    {
        if (isLit || hasExploded) return;
        isLit = true;

        if (fuseTip == null)
        {
            Debug.LogWarning("[GoblinBomb] No asignaste 'fuseTip'. Creo uno en el centro como fallback — movelo a la punta de la mecha.");
            GameObject tip = new GameObject("FuseTip");
            tip.transform.SetParent(transform, false);
            tip.transform.localPosition = Vector3.up * 0.5f;
            fuseTip = tip.transform;
        }

        BuildSparkVFX();
        BuildFlickerLight();
        PlayFuseSound();
        StartCoroutine(FuseCountdown());
    }

    IEnumerator FuseCountdown()
    {
        yield return new WaitForSeconds(fuseTime);
        Explode();
    }

    /// <summary>
    /// Detona inmediatamente (útil si la bomba choca contra algo al ser lanzada).
    /// </summary>
    public void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;
        isLit = false;

        // Daño en área
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, damageLayers);
        foreach (Collider hit in hits)
        {
            // Adaptá esto a tu sistema de vida. Busca un componente IDamageable
            // o un método público "TakeDamage(float)".
            hit.SendMessage("TakeDamage", explosionDamage, SendMessageOptions.DontRequireReceiver);

            Rigidbody rb = hit.attachedRigidbody;
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.5f, ForceMode.Impulse);
            }
        }

        // VFX
        if (explosionVFXPrefab != null)
        {
            Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            SpawnDebugExplosionFlash();
        }

        // Sonido
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        Destroy(gameObject);
    }

    // ---------------------------------------------------------------
    //  Chispa de la mecha (generada por código, sin assets externos)
    // ---------------------------------------------------------------
    void BuildSparkVFX()
    {
        if (sparkPS != null) { sparkPS.Play(); return; }

        GameObject psObj = new GameObject("FuseSpark_PS");
        psObj.transform.SetParent(fuseTip, false);

        sparkPS = psObj.AddComponent<ParticleSystem>();
        var main = sparkPS.main;
        main.loop = true;
        main.startLifetime = new ParticleSystem.MinMaxCurve(0.15f, 0.35f);
        main.startSpeed = new ParticleSystem.MinMaxCurve(0.6f, 1.8f);
        main.startSize = new ParticleSystem.MinMaxCurve(0.02f, 0.05f);
        main.startColor = sparkColorHot;
        main.gravityModifier = 1.2f;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.maxParticles = 200;

        var emission = sparkPS.emission;
        emission.rateOverTime = sparkEmissionRate;
        // Burst ocasional: una tanda de chispas más grande cada ~0.7s
        emission.SetBursts(new ParticleSystem.Burst[]
        {
            new ParticleSystem.Burst(0.7f, 6, 10, -1, 0.7f)
        });

        var shape = sparkPS.shape;
        shape.shapeType = ParticleSystemShapeType.Cone;
        shape.angle = 35f;
        shape.radius = 0.02f;
        shape.rotation = new Vector3(-90f, 0f, 0f);

        var col = sparkPS.colorOverLifetime;
        col.enabled = true;
        Gradient grad = new Gradient();
        grad.SetKeys(
            new[] { new GradientColorKey(sparkColorHot, 0f), new GradientColorKey(sparkColorCold, 1f) },
            new[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(0f, 1f) }
        );
        col.color = grad;

        var sol = sparkPS.sizeOverLifetime;
        sol.enabled = true;
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0f, 1f);
        curve.AddKey(1f, 0f);
        sol.size = new ParticleSystem.MinMaxCurve(1f, curve);

        var noise = sparkPS.noise;
        noise.enabled = true;
        noise.strength = 0.4f;
        noise.frequency = 2f;

        var trails = sparkPS.trails;
        trails.enabled = true;
        trails.ratio = 0.3f;
        trails.lifetime = 0.2f;
        trails.minVertexDistance = 0.02f;
        trails.widthOverTrail = new ParticleSystem.MinMaxCurve(0.5f);

        var renderer = psObj.GetComponent<ParticleSystemRenderer>();
        renderer.renderMode = ParticleSystemRenderMode.Billboard;
        renderer.material = sparkMaterial != null ? sparkMaterial : CreateAdditiveMaterial();
        renderer.trailMaterial = renderer.material;
    }

    Material CreateAdditiveMaterial()
    {
        Shader shader = Shader.Find("Particles/Standard Unlit");
        if (shader == null) shader = Shader.Find("Legacy Shaders/Particles/Additive");
        Material mat = new Material(shader);
        if (mat.HasProperty("_ColorMode")) mat.SetFloat("_ColorMode", 1);
        return mat;
    }

    void BuildFlickerLight()
    {
        if (flickerLight != null) { flickerLight.enabled = true; return; }

        GameObject lightObj = new GameObject("FuseSpark_Light");
        lightObj.transform.SetParent(fuseTip, false);
        flickerLight = lightObj.AddComponent<Light>();
        flickerLight.type = LightType.Point;
        flickerLight.color = new Color(1f, 0.65f, 0.25f);
        flickerLight.range = 1.5f;
        flickerLight.intensity = 0.6f;
        flickerLight.shadows = LightShadows.None;
    }

    void PlayFuseSound()
    {
        if (fuseLoopSound == null) return;
        fuseAudioSource = gameObject.AddComponent<AudioSource>();
        fuseAudioSource.clip = fuseLoopSound;
        fuseAudioSource.loop = true;
        fuseAudioSource.spatialBlend = 1f;
        fuseAudioSource.Play();
    }

    void Update()
    {
        if (isLit && flickerLight != null)
        {
            float n = Mathf.PerlinNoise(Time.time * 20f, 0f);
            flickerLight.intensity = Mathf.Lerp(0.4f, 1.2f, n);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.4f, 0.1f, 0.3f);
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    // Flash simple de fallback si no asignaste un prefab de explosión
    void SpawnDebugExplosionFlash()
    {
        GameObject flash = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        flash.transform.position = transform.position;
        flash.transform.localScale = Vector3.one * explosionRadius * 0.5f;
        Collider c = flash.GetComponent<Collider>();
        if (c != null) Destroy(c);
        Renderer r = flash.GetComponent<Renderer>();
        r.material.color = new Color(1f, 0.6f, 0.2f);
        Destroy(flash, 0.15f);
    }
}
