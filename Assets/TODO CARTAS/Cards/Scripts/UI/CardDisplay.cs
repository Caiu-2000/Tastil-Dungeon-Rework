using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class CardDisplay : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Datos")]
    public Card card;

    [Header("UI - Contenido")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image artworkImage;

    [Header("UI - Icono inferior izquierdo (BuffTarget)")]
    public Image buffTargetIcon;
    public Sprite personajeSprite;
    public Sprite armaSprite;

    [Header("UI - Icono inferior derecho (Arquetipo)")]
    public Image archetypeIcon;
    public Sprite tankSprite;
    public Sprite berserkerSprite;
    public Sprite duelistaSprite;
    public Sprite piromanoSprite;

    [Header("Hover Effect")]
    public float hoverHeight = 20f;
    public float hoverSpeed = 8f;

    [Header("Glow Effect")]
    public Image hoverGlow;
    public float glowMaxAlpha = 0.3f;
    public float glowSpeed = 5f;
    public float pulseSpeed = 2f;       // velocidad del pulso
    public float pulseAmount = 0.08f;   // cuanto varia el brillo al pulsar

    [HideInInspector] public int cardIndex;

    private Vector3 _originalPosition;
    private Coroutine _hoverCoroutine;
    private Coroutine _glowCoroutine;
    private Coroutine _pulseCoroutine;
    private bool _isHovered = false;

    void Start()
    {
        _originalPosition = transform.localPosition;

        if (hoverGlow != null)
        {
            Color c = hoverGlow.color;
            c.a = 0f;
            hoverGlow.color = c;
        }

        UpdateDisplay();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CardSelectionUI.Instance.OnCardSelected(cardIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovered = true;

        if (_hoverCoroutine != null) StopCoroutine(_hoverCoroutine);
        _hoverCoroutine = StartCoroutine(MoveCard(_originalPosition + new Vector3(0, hoverHeight, 0)));

        if (_glowCoroutine != null) StopCoroutine(_glowCoroutine);
        _glowCoroutine = StartCoroutine(FadeGlow(glowMaxAlpha));

        if (_pulseCoroutine != null) StopCoroutine(_pulseCoroutine);
        _pulseCoroutine = StartCoroutine(PulseGlow());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovered = false;

        if (_hoverCoroutine != null) StopCoroutine(_hoverCoroutine);
        _hoverCoroutine = StartCoroutine(MoveCard(_originalPosition));

        if (_pulseCoroutine != null) StopCoroutine(_pulseCoroutine);

        if (_glowCoroutine != null) StopCoroutine(_glowCoroutine);
        _glowCoroutine = StartCoroutine(FadeGlow(0f));
    }

    private IEnumerator MoveCard(Vector3 target)
    {
        while (Vector3.Distance(transform.localPosition, target) > 0.1f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.unscaledDeltaTime * hoverSpeed);
            yield return null;
        }
        transform.localPosition = target;
    }

    private IEnumerator FadeGlow(float targetAlpha)
    {
        if (hoverGlow == null) yield break;

        Color c = hoverGlow.color;

        while (Mathf.Abs(c.a - targetAlpha) > 0.01f)
        {
            c.a = Mathf.Lerp(c.a, targetAlpha, Time.unscaledDeltaTime * glowSpeed);
            hoverGlow.color = c;
            yield return null;
        }

        c.a = targetAlpha;
        hoverGlow.color = c;
    }

    private IEnumerator PulseGlow()
    {
        if (hoverGlow == null) yield break;

        // Esperar que el fade inicial termine
        yield return new WaitForSecondsRealtime(0.3f);

        while (_isHovered)
        {
            // Sube
            float timer = 0f;
            while (timer < 1f && _isHovered)
            {
                timer += Time.unscaledDeltaTime * pulseSpeed;
                Color c = hoverGlow.color;
                c.a = glowMaxAlpha + Mathf.Sin(timer * Mathf.PI) * pulseAmount;
                hoverGlow.color = c;
                yield return null;
            }
        }
    }

    public void UpdateDisplay()
    {
        if (card == null) return;

        nameText.text = card.name;
        descriptionText.text = card.description;
        artworkImage.sprite = card.artwork;

        AssignBuffTargetIcon();
        AssignArchetypeIcon();
    }

    public void SetCard(Card newCard)
    {
        card = newCard;
        UpdateDisplay();
    }

    private void AssignBuffTargetIcon()
    {
        buffTargetIcon.enabled = true;

        switch (card.buffTarget)
        {
            case BuffTarget.Personaje:
                buffTargetIcon.sprite = personajeSprite;
                break;
            case BuffTarget.Arma:
                buffTargetIcon.sprite = armaSprite;
                break;
        }
    }

    private void AssignArchetypeIcon()
    {
        archetypeIcon.enabled = true;

        switch (card.archetype)
        {
            case Archetype.Tank:
                archetypeIcon.sprite = tankSprite;
                break;
            case Archetype.Berserker:
                archetypeIcon.sprite = berserkerSprite;
                break;
            case Archetype.Duelista:
                archetypeIcon.sprite = duelistaSprite;
                break;
            case Archetype.Piromano:
                archetypeIcon.sprite = piromanoSprite;
                break;
            case Archetype.Criomante:
                archetypeIcon.enabled = false;
                break;
        }
    }
}
