using UnityEngine;

public class PerkPlaceholder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        PerkManager.Instance.OnEnemyHitted += Hitted;
        PerkManager.Instance.OnEnemyDeath  += Kill;
    }


    public void Hitted(Enemy enemy , float dam = 0)
    {
        print("Soy una perk y golpeaste a un enemigo");
    }
    public void Kill(Enemy enemy)
    {
        print("Soy una perk y mataste a un enemigo");
    }
}
