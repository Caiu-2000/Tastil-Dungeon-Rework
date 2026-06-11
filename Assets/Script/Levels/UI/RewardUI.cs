using UnityEngine;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour
{
    [SerializeField] GameObject reward;

    public bool IsVisible() => reward.activeSelf;

    public void Show()
    {
        reward.SetActive(true);
    }
}
