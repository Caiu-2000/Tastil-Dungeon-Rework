using UnityEngine;
using System.Collections.Generic;

public class CardSelectionUI : MonoBehaviour
{
    public static CardSelectionUI Instance { get; private set; }

    [Header("Panel")]
    [SerializeField] private GameObject selectionPanel;

    [Header("Slots de cartas (exactamente 2)")]
    [SerializeField] private List<CardDisplay> cardSlots = new List<CardDisplay>();

    private System.Action<int> _onSelected;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        selectionPanel.SetActive(false);
    }

    public void Show(List<Card> cardsToShow, System.Action<int> onSelected)
    {
        if (cardsToShow == null || cardsToShow.Count < 2)
        {
            Debug.LogWarning("[CardSelectionUI] Se necesitan exactamente 2 cartas.");
            return;
        }

        _onSelected = onSelected;

        for (int i = 0; i < cardSlots.Count; i++)
        {
            if (i < cardsToShow.Count)
            {
                cardSlots[i].SetCard(cardsToShow[i]);
                cardSlots[i].cardIndex = i;
            }
        }

        selectionPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnCardSelected(int index)
    {
        Hide();
        _onSelected?.Invoke(index);
    }

    private void Hide()
    {
        selectionPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}