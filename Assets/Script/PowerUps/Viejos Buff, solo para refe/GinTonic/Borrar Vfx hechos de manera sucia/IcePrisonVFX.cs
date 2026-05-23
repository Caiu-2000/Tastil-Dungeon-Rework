using System.Collections;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

/// <summary>
/// IcePrisonVFX — spawns procedural ice spike meshes around the character
/// when a stun is applied. Call Play() to trigger, Stop() to shatter.
/// Attach to any GameObject; assign Target to the character root.
/// </summary>
public class IcePrisonVFX : MonoBehaviour
{
    [Header("Target")]
    public Transform Target;
    public float HeightOffset = 0f;

    [Header("Ice Spikes")]
    [Range(3, 12)]
    public int SpikeCount = 6;

    [Range(0.3f, 2f)]
    public float RingRadius = 0.85f;

    [Range(0.5f, 4f)]
    public float SpikeHeight = 2.2f;

    [Range(0.05f, 0.4f)]
    public float SpikeWidth = 0.18f;

    [Range(0f, 45f)]
    public float InwardTilt = 15f;      // Degrees spikes tilt toward center

    [Header("Rise Animation")]
    [Range(0.1f, 2f)]
    public float RiseDuration = 0.4f;

    [Range(0f, 0.3f)]
    public float SpikeStagger = 0.06f;  // Delay between each spike rising

    [Header("Shatter")]
    [Range(0.05f, 0.5f)]
    public float ShatterDuration = 0.25f;

    [Range(3, 20)]
    public int ShardCount = 8;          // Shards per spike on shatter

    [Range(1f, 8f)]
    public float ShardForce = 4f;

    [Header("Visuals")]
    public Color IceColor = new Color(0.55f, 0.85f, 1f, 0.82f);
    public Color IceEdgeColor = new Color(0.8f, 0.95f, 1f, 1f);

    [Tooltip("Optional material. Leave empty to use generated transparent material.")]
    public Material IceMaterial;

    // ── Private ──────────────────────────────────────────────────────────────

    GameObject[] _spikes;
    Material _mat;
    bool _active = false;

    // ── Unity lifecycle ──────────────────────────────────────────────────────

    void Awake()
    {
        _mat = IceMaterial != null ? IceMaterial : BuildIceMaterial();
    }

    void LateUpdate()
    {
        if (!_active || Target == null) return;
        transform.position = Target.position + Vector3.up * HeightOffset;
    }

    // ── Public API ───────────────────────────────────────────────────────────

    public void Play()
    {
        if (_active) return;
        _active = true;
        transform.position = Target != null
            ? Target.position + Vector3.up * HeightOffset
            : transform.position;
        StartCoroutine(SpawnSpikes());
    }

    public void Stop()
    {
        if (!_active) return;
        StartCoroutine(Shatter());
    }

    // ── Spike spawning ────────────────────────────────────────────────────────

    IEnumerator SpawnSpikes()
    {
        _spikes = new GameObject[SpikeCount];

        for (int i = 0; i < SpikeCount; i++)
        {
            float angle = (360f / SpikeCount) * i * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle));

            GameObject spike = new GameObject($"IceSpike_{i}");
            spike.transform.SetParent(transform);
            spike.transform.localPosition = dir * RingRadius;

            // Tilt inward toward character center
            Vector3 lookDir = (-dir + Vector3.up * Mathf.Tan(InwardTilt * Mathf.Deg2Rad)).normalized;
            spike.transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up)
                                     * Quaternion.Euler(90f, 0f, 0f);

            MeshFilter mf = spike.AddComponent<MeshFilter>();
            MeshRenderer mr = spike.AddComponent<MeshRenderer>();
            mf.mesh = BuildSpikeMesh();
            mr.material = _mat;

            _spikes[i] = spike;

            // Animate rise from ground
            StartCoroutine(RiseSpike(spike, SpikeStagger * i));

            yield return null;
        }
    }

    IEnumerator RiseSpike(GameObject spike, float delay)
    {
        yield return new WaitForSeconds(delay);

        Vector3 finalScale = new Vector3(SpikeWidth, SpikeHeight, SpikeWidth);
        spike.transform.localScale = new Vector3(SpikeWidth, 0f, SpikeWidth);

        float t = 0f;
        while (t < RiseDuration)
        {
            t += Time.deltaTime;
            float p = Mathf.SmoothStep(0f, 1f, t / RiseDuration);
            spike.transform.localScale = Vector3.Lerp(
                new Vector3(SpikeWidth, 0f, SpikeWidth), finalScale, p);
            yield return null;
        }
        spike.transform.localScale = finalScale;
    }

    // ── Shatter ───────────────────────────────────────────────────────────────

    IEnumerator Shatter()
    {
        _active = false;

        if (_spikes == null) yield break;

        foreach (var spike in _spikes)
        {
            if (spike == null) continue;
            SpawnShards(spike.transform.position);
            Destroy(spike);
        }

        _spikes = null;
        yield return null;
    }

    void SpawnShards(Vector3 origin)
    {
        for (int i = 0; i < ShardCount; i++)
        {
            GameObject shard = new GameObject("IceShard");
            shard.transform.position = origin + Random.insideUnitSphere * 0.15f;

            MeshFilter mf = shard.AddComponent<MeshFilter>();
            MeshRenderer mr = shard.AddComponent<MeshRenderer>();
            Rigidbody rb = shard.AddComponent<Rigidbody>();

            mf.mesh = BuildShardMesh();
            mr.material = _mat;

            rb.linearVelocity = (Random.insideUnitSphere.normalized + Vector3.up * 0.5f).normalized
                               * ShardForce * Random.Range(0.5f, 1.5f);
            rb.angularVelocity = Random.insideUnitSphere * 8f;
            rb.useGravity = true;

            Destroy(shard, ShatterDuration + 0.8f);
            StartCoroutine(FadeShard(mr, ShatterDuration));
        }
    }

    IEnumerator FadeShard(MeshRenderer mr, float duration)
    {
        float t = 0f;
        Color c = IceColor;
        while (t < duration)
        {
            t += Time.deltaTime;
            if (mr == null) yield break;
            c.a = Mathf.Lerp(IceColor.a, 0f, t / duration);
            mr.material.color = c;
            yield return null;
        }
    }

    // ── Mesh builders ─────────────────────────────────────────────────────────

    /// <summary>Elongated diamond/spike — 8 vertices, faceted.</summary>
    Mesh BuildSpikeMesh()
    {
        Mesh mesh = new Mesh();
        mesh.name = "IceSpike";

        // A spike: tip at top (0,1,0), base at (0,0,0), 4-sided prism
        Vector3 tip = new Vector3(0f, 1f, 0f);
        Vector3 b0 = new Vector3(0.5f, 0f, 0.5f);
        Vector3 b1 = new Vector3(-0.5f, 0f, 0.5f);
        Vector3 b2 = new Vector3(-0.5f, 0f, -0.5f);
        Vector3 b3 = new Vector3(0.5f, 0f, -0.5f);
        Vector3 base_ = new Vector3(0f, -0.1f, 0f);  // tiny base cap

        mesh.vertices = new Vector3[] {
            tip, b0, b1,      // face 0
            tip, b1, b2,      // face 1
            tip, b2, b3,      // face 2
            tip, b3, b0,      // face 3
            base_, b1, b0,    // base cap 0
            base_, b2, b1,    // base cap 1
            base_, b3, b2,    // base cap 2
            base_, b0, b3,    // base cap 3
        };

        int[] tris = new int[mesh.vertices.Length];
        for (int i = 0; i < tris.Length; i++) tris[i] = i;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }

    /// <summary>Tiny irregular shard for shatter effect.</summary>
    Mesh BuildShardMesh()
    {
        Mesh mesh = new Mesh();
        mesh.name = "IceShard";

        float s = Random.Range(0.06f, 0.14f);
        Vector3 t0 = new Vector3(0f, s, 0f);
        Vector3 t1 = new Vector3(-s * 0.8f, -s * 0.3f, s * 0.4f);
        Vector3 t2 = new Vector3(s * 0.8f, -s * 0.3f, s * 0.4f);
        Vector3 t3 = new Vector3(0f, -s * 0.6f, -s * 0.8f);

        mesh.vertices = new Vector3[] { t0, t1, t2, t0, t2, t3, t0, t3, t1, t1, t3, t2 };
        int[] tris = new int[12];
        for (int i = 0; i < 12; i++) tris[i] = i;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }

    // ── Material ──────────────────────────────────────────────────────────────

    Material BuildIceMaterial()
    {
        // Try URP first, fall back to standard
        Shader shader = Shader.Find("Universal Render Pipeline/Lit")
                     ?? Shader.Find("Standard");

        Material mat = new Material(shader);
        mat.color = IceColor;

        // Enable transparency
        mat.SetFloat("_Mode", 3f);                       // Standard: Transparent
        mat.SetFloat("_Surface", 1f);                       // URP: Transparent
        mat.SetFloat("_Blend", 0f);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;

        // Ice look: high smoothness + slight metallic
        mat.SetFloat("_Smoothness", 0.9f);
        mat.SetFloat("_Metallic", 0.2f);
        mat.SetColor("_EmissionColor", IceEdgeColor * 0.3f);
        mat.EnableKeyword("_EMISSION");

        return mat;
    }
}
