using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class TestCards : MonoBehaviour
{
    [SerializeField] private List<Card> testCards = new List<Card>();

    void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            CardSelectionUI.Instance.Show(testCards);
        }
    }
}