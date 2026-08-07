using System.Collections.Generic;
using UnityEngine;

public class WeaponSelect : MonoBehaviour
{
    [SerializeField] private List<Card> cards;
    [SerializeField] private List<Weapon> weapons;
    [SerializeField]private List<Card> cardsRandom;
    int random1;
    int random2;
    void Start()
    {
        random1 = Random.Range(0, cards.Count);
        random2 = Random.Range(0, cards.Count);
        while (random1 == random2)
        {
            random2 = Random.Range(0, cards.Count);
        }
        cardsRandom.Add(cards[random1]);
        cardsRandom.Add(cards[random2]);
        CardSelectionUI.Instance.Show(cardsRandom, index =>
        {
            if (index == 0)
            {
                SpawnWeapon(weapons[random1]);
            }
            else
            {
                SpawnWeapon(weapons[random2]);
            }
        });
    }
    void SpawnWeapon(Weapon weapon)
    {
        //var weaponspawned = Instantiate(weapon);
        PlayerMaster player = GameManager.Instance.GetPlayer();
        InventoryComponent inventory = player.GetComponent<InventoryComponent>();
        inventory.AddItem(weapon);
    }
}
