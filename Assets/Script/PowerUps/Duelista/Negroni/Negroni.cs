using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class Negroni : Item
{
    [SerializeField] BuffData data;
    [SerializeField] BuffManager manager = BuffManager.Instance;
    [SerializeField] Image imagen;
    MeleWeapon weapon;
    PlayerMaster player;
    bool isActive;
    private void Start()
    {
        manager = BuffManager.Instance;
        player = GameManager.Instance.GetPlayer();  
        imagen = player.transform.Find("PlayerUI/BuffSelector").GetComponent<Image>();
    }
    private void Update()
    {
        if (imagen.IsActive() && isActive)
        {
            if (Keyboard.current.tKey.wasPressedThisFrame)
                WeaponBuff();
            if (Keyboard.current.yKey.wasPressedThisFrame)
                BodyBuff();
        }

    }
    public override void Use()
    {
        player = gameObject.transform.root.GetComponent<PlayerMaster>();
        weapon = player.GetComponentInChildren<MeleWeapon>();
        imagen.gameObject.SetActive(true);
        isActive = true;
    }
    void WeaponBuff()
    {
        BuffManager.Instance.AddBuffOnCriticalHit(new NegroniWeaponBuff(data));
        imagen.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void BodyBuff()
    {
        BuffManager.Instance.AddBuffOnHit(new NegroniBodyBuff(data, weapon));
        imagen.gameObject.SetActive(false);
        Destroy(gameObject);
    }
    private IEnumerator DrinkTime()
    {
        _hand.SetAnimationTrigger("Drink");
        yield return new WaitForSeconds(_useTime);
        if (_ReplaceItem)
        {
            Item Replace = Instantiate(_ReplaceItem);
            _inventory.ReplaceItem(this, Replace);


        }

        Destroy(gameObject);
    }
}
