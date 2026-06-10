using UnityEngine;
using System.Collections.Generic;

// Adjuntar este script al GameObject CardSelectionUI dentro del Canvas.
// Es un Singleton accesible desde cualquier parte.
//
// Para mostrar las cartas al terminar la sala, llamar desde el RoomManager:
//     CardSelectionUI.Instance.Show(listaDeCartas);
//
// NOTA: Cuando se integren los scripts de Combat y Player al proyecto,
// reconectar GameManager, PlayerMaster y Weapon en OnCardSelected.
//
// Jerarquia esperada en el Canvas:
//   CardSelectionUI
//   └── SelectionPanel
//       └── CardsContainer
//           ├── Card_1  (con CardDisplay.cs)
//           └── Card_2  (con CardDisplay.cs)

public class CardSelectionUI : MonoBehaviour
{
    public static CardSelectionUI Instance { get; private set; }

    [Header("Panel")]
    [SerializeField] private GameObject selectionPanel;

    [Header("Slots de cartas (exactamente 2)")]
    [SerializeField] private List<CardDisplay> cardSlots = new List<CardDisplay>();

    // Cartas actuales que se estan mostrando
    private List<Card> _currentCards = new List<Card>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // El panel arranca oculto
        selectionPanel.SetActive(false);
    }

    // Llamar este metodo desde el RoomManager cuando la sala termine
    public void Show(List<Card> cardsToShow)
    {
        if (cardsToShow == null || cardsToShow.Count < 2)
        {
            Debug.LogWarning("[CardSelectionUI] Se necesitan exactamente 2 cartas para mostrar.");
            return;
        }

        _currentCards = cardsToShow;

        // Cargar cada slot con su carta y asignarle su indice para el click
        for (int i = 0; i < cardSlots.Count; i++)
        {
            if (i < cardsToShow.Count)
            {
                cardSlots[i].SetCard(cardsToShow[i]);
                cardSlots[i].cardIndex = i;
            }
        }

        selectionPanel.SetActive(true);
        Time.timeScale = 0f;    // Pausar el juego
    }

    // Llamado desde CardDisplay.OnPointerClick cuando el jugador elige una carta
    public void OnCardSelected(int index)
    {
        if (index >= _currentCards.Count) return;

        Card chosen = _currentCards[index];

        // Aplicar el efecto de la carta elegida
        // NOTA: Cuando se integren PlayerMaster y Weapon, pasar las referencias aca
        if (chosen.effect != null)
            chosen.effect.ApplyEffect();
        else
            Debug.LogWarning($"[CardSelectionUI] La carta '{chosen.name}' no tiene efecto asignado.");

        Debug.Log($"[CardSelectionUI] Carta elegida: {chosen.name}");

        Hide();
    }

    private void Hide()
    {
        selectionPanel.SetActive(false);
        Time.timeScale = 1f;    // Reanudar el juego
    }
}
