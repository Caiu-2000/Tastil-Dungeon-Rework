using System.Collections;
using UnityEngine;

/// <summary>
/// Componente para el GOBLIN: saca la bomba ya encendida y la lanza al objetivo.
/// Estilo WoW: el enemigo levanta la bomba prendida (chispas visibles en la mano)
/// y la arroja tras un pequeño delay de "wind-up".
///
/// SETUP:
/// 1. Agregá este script al GameObject del goblin.
/// 2. Asigná en "bombPrefab" el prefab de la bomba (que ya tiene GoblinBomb.cs).
/// 3. Asigná en "handSocket" el transform de la mano del goblin
///    (el hueso de la mano derecha, o un empty child de ese hueso).
/// 4. Llamá a ThrowBombAt(target) desde tu AI, o activá "autoThrowDemo"
///    para probarlo lanzando cada X segundos a un target fijo.
///
/// Si tu goblin tiene Animator, podés disparar la animación de lanzar y
/// llamar a estos métodos desde Animation Events:
///   - Evento "DrawBomb"  -> al frame donde la mano queda arriba
///   - Evento "ReleaseBomb" -> al frame donde suelta la bomba
/// </summary>
public class GoblinBombThrower : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject bombPrefab;
    [Tooltip("Hueso o empty en la mano del goblin donde aparece la bomba.")]
    public Transform handSocket;

    [Header("Lanzamiento")]
    [Tooltip("Tiempo que el goblin sostiene la bomba encendida antes de tirarla.")]
    public float windUpTime = 0.8f;
    [Tooltip("Altura del arco de la parábola del lanzamiento.")]
    public float throwArcHeight = 2.5f;
    [Tooltip("Duración del vuelo de la bomba hasta el objetivo.")]
    public float throwDuration = 0.9f;

    [Header("Demo / testing")]
    [Tooltip("Si está activo, lanza automáticamente al 'demoTarget' cada 'demoInterval' segundos.")]
    public bool autoThrowDemo = false;
    public Transform demoTarget;
    public float demoInterval = 5f;

    private GameObject currentBomb;
    private bool isThrowing;

    void Start()
    {
        if (autoThrowDemo && demoTarget != null)
        {
            StartCoroutine(DemoLoop());
        }
    }

    IEnumerator DemoLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(demoInterval);
            ThrowBombAt(demoTarget.position);
        }
    }

    /// <summary>
    /// Secuencia completa: saca la bomba encendida, espera el wind-up y la lanza.
    /// Llamá a esto desde tu AI cuando el goblin decida atacar.
    /// </summary>
    public void ThrowBombAt(Vector3 targetPosition)
    {
        if (isThrowing) return;
        StartCoroutine(ThrowSequence(targetPosition));
    }

    IEnumerator ThrowSequence(Vector3 targetPosition)
    {
        isThrowing = true;

        DrawBomb();
        yield return new WaitForSeconds(windUpTime);
        ReleaseBomb(targetPosition);

        isThrowing = false;
    }

    /// <summary>
    /// Instancia la bomba YA ENCENDIDA en la mano del goblin.
    /// (GoblinBomb tiene igniteOnEnable = true, así que las chispas
    /// arrancan en el primer frame, con la bomba todavía en la mano.)
    /// También lo podés llamar desde un Animation Event.
    /// </summary>
    public void DrawBomb()
    {
        if (bombPrefab == null || handSocket == null)
        {
            Debug.LogWarning("[GoblinBombThrower] Falta asignar bombPrefab o handSocket.");
            return;
        }

        currentBomb = Instantiate(bombPrefab, handSocket.position, handSocket.rotation, handSocket);
        currentBomb.transform.localPosition = Vector3.zero;
        currentBomb.transform.localRotation = Quaternion.identity;
    }

    /// <summary>
    /// Suelta la bomba y la manda en parábola hacia el target.
    /// También lo podés llamar desde un Animation Event (sin parámetro usa el forward del goblin).
    /// </summary>
    public void ReleaseBomb(Vector3 targetPosition)
    {
        if (currentBomb == null) return;

        currentBomb.transform.SetParent(null, true);
        StartCoroutine(ArcFlight(currentBomb, targetPosition));
        currentBomb = null;
    }

    // Vuelo en parábola simple (sin física, control total del arco y el tiempo).
    IEnumerator ArcFlight(GameObject bomb, Vector3 target)
    {
        Vector3 start = bomb.transform.position;
        float t = 0f;

        while (t < 1f && bomb != null)
        {
            t += Time.deltaTime / throwDuration;
            float clamped = Mathf.Clamp01(t);

            Vector3 flat = Vector3.Lerp(start, target, clamped);
            float arc = throwArcHeight * 4f * clamped * (1f - clamped); // parábola
            bomb.transform.position = flat + Vector3.up * arc;

            // Rotación de tumbo mientras vuela, típico de bomba lanzada
            bomb.transform.Rotate(Vector3.right * 360f * Time.deltaTime, Space.Self);

            yield return null;
        }

        // Al aterrizar: explota de inmediato o dejá que el fuse termine solo.
        // Acá la hacemos explotar al impactar, estilo WoW.
        if (bomb != null)
        {
            GoblinBomb gb = bomb.GetComponent<GoblinBomb>();
            if (gb != null) gb.Explode();
        }
    }
}
