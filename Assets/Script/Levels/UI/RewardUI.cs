using UnityEngine;

public class RewardUI : MonoBehaviour
{
    [SerializeField] GameObject reward;
    public void Show()
    {
        reward.SetActive(true);
    }
}
