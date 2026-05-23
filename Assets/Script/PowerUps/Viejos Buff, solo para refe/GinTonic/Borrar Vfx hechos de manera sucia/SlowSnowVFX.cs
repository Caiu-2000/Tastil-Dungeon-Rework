using UnityEngine;

/// <summary>
/// SlowSnowVFX — attach to a GameObject and assign it to your character.
/// Uses Unity's built-in Particle System (Shuriken). No extra packages needed.
/// Call Play() when the slow debuff is applied, Stop() when it ends.
/// </summary>
public class SlowSnowVFX : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("The character's root transform. Particles will orbit around it.")]
    public Transform Target;

    [Tooltip("Offset above the character's root (adjust to center on body).")]
    public float HeightOffset = 1f;

    [Header("Snowflake Shape")]
    [Range(10, 150)]
    public int MaxParticles = 60;

    [Range(0.05f, 0.5f)]
    public float ParticleSize = 0.12f;

    [Range(0.3f, 3f)]
    public float SpawnRadius = 0.7f;

    [Range(0.5f, 4f)]
    public float SpawnHeight = 2.5f;

    [Header("Motion")]
    [Range(0.1f, 3f)]
    public float FallSpeed = 0.6f;

    [Range(0f, 1f)]
    public float DriftAmount = 0.2f;

    [Range(0f, 90f)]
    public float RotationSpeed = 45f;

    [Header("Visuals")]
    public Color SnowColor = new Color(0.8f, 0.93f, 1f, 0.9f);

    [Tooltip("Optional custom snowflake texture. Leave empty for default circle.")]
    public Texture2D SnowflakeTexture;

    // Private
    ParticleSystem _ps;

    void Awake()
    {
        BuildParticleSystem();
    }

    void LateUpdate()
    {
        if (Target == null || _ps == null) return;
        transform.position = Target.position + Vector3.up * HeightOffset;
    }

    public void Play()
    {
        if (_ps == null) BuildParticleSystem();
        _ps.Play();
    }

    public void Stop(bool clearParticles = false)
    {
        if (_ps == null) return;
        _ps.Stop(true, clearParticles
            ? ParticleSystemStopBehavior.StopEmittingAndClear
            : ParticleSystemStopBehavior.StopEmitting);
    }

    public bool IsPlaying => _ps != null && _ps.isPlaying;

    void BuildParticleSystem()
    {
        _ps = gameObject.GetComponent<ParticleSystem>();
        if (_ps == null) _ps = gameObject.AddComponent<ParticleSystem>();

        // Main
        var main = _ps.main;
        main.loop = true;
        main.playOnAwake = false;
        main.maxParticles = MaxParticles;
        main.startLifetime = new ParticleSystem.MinMaxCurve(1.8f, 3.2f);
        main.startSpeed = 0f;
        main.startSize = new ParticleSystem.MinMaxCurve(ParticleSize * 0.6f, ParticleSize);
        main.startRotation = new ParticleSystem.MinMaxCurve(0f, 360f * Mathf.Deg2Rad);
        main.startColor = SnowColor;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.gravityModifier = 0f;

        // Emission
        var emission = _ps.emission;
        emission.enabled = true;
        emission.rateOverTime = MaxParticles / 2.5f;

        // Shape
        var shape = _ps.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Box;
        shape.scale = new Vector3(SpawnRadius * 2f, SpawnHeight, SpawnRadius * 2f);
        shape.position = new Vector3(0f, SpawnHeight * 0.5f, 0f);

        // Velocity — ALL axes must use the same MinMaxCurve mode.
        // Using TwoConstants for all three to avoid the runtime error.
        var velocity = _ps.velocityOverLifetime;
        velocity.enabled = true;
        velocity.space = ParticleSystemSimulationSpace.World;
        velocity.x = new ParticleSystem.MinMaxCurve(-DriftAmount, DriftAmount);
        velocity.y = new ParticleSystem.MinMaxCurve(-FallSpeed, -FallSpeed * 0.8f);
        velocity.z = new ParticleSystem.MinMaxCurve(-DriftAmount * 0.6f, DriftAmount * 0.6f);

        // Rotation over lifetime
        var rotation = _ps.rotationOverLifetime;
        rotation.enabled = true;
        rotation.z = new ParticleSystem.MinMaxCurve(
            -RotationSpeed * Mathf.Deg2Rad,
             RotationSpeed * Mathf.Deg2Rad);

        // Color: fade in then fade out
        var colorMod = _ps.colorOverLifetime;
        colorMod.enabled = true;
        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(SnowColor, 0f),
                new GradientColorKey(Color.white, 0.5f),
                new GradientColorKey(SnowColor, 1f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0f,  0f),
                new GradientAlphaKey(1f,  0.15f),
                new GradientAlphaKey(1f,  0.85f),
                new GradientAlphaKey(0f,  1f)
            }
        );
        colorMod.color = new ParticleSystem.MinMaxGradient(grad);

        // Size over lifetime
        var sizeMod = _ps.sizeOverLifetime;
        sizeMod.enabled = true;
        AnimationCurve sizeCurve = new AnimationCurve(
            new Keyframe(0f, 0.2f),
            new Keyframe(0.2f, 1f),
            new Keyframe(0.8f, 1f),
            new Keyframe(1f, 0.2f));
        sizeMod.size = new ParticleSystem.MinMaxCurve(1f, sizeCurve);

        // Renderer
        var rend = _ps.GetComponent<ParticleSystemRenderer>();
        rend.renderMode = ParticleSystemRenderMode.Billboard;
        rend.sortingOrder = 1;

        if (SnowflakeTexture != null)
        {
            Material mat = new Material(Shader.Find("Particles/Standard Unlit"));
            mat.mainTexture = SnowflakeTexture;
            mat.SetFloat("_Mode", 2);
            rend.material = mat;
        }
        else
        {
            rend.material = new Material(Shader.Find("Particles/Standard Unlit"));
        }
    }
}
