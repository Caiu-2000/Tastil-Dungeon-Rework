using System.Collections;

using UnityEngine;

public class ItemsHand : MonoBehaviour
{

    private Item _equipedItem;
    private bool _hotbarCd = false;
    [SerializeField] Transform ItemPosition;
    public Animator _animator;

    bool ParryReady = true;

    public delegate void ParryCdUpdated(float cD);

    public ParryCdUpdated OnParryUpdated = delegate { };

    [SerializeField]
    private float CdCooldown = 0.5f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        GameManager.Instance.InputHandler.OnUseItemPressed += TryToUse;
        GameManager.Instance.InputHandler.OnParryPressed +=  ParryPressed;


    }


    public void ChangeItem(Item _newItem)
    {
        if (_hotbarCd) { return; }
        StartCoroutine(HotBarCd());
        if (_equipedItem != null)
        {
            _animator.SetTrigger("ChangeItem");
            _equipedItem.transform.parent = null;
            _equipedItem.transform.position = new Vector3(0, 1000, 0);
            _equipedItem._isPicked = false;
            _equipedItem = null;
        }
        else
        {
            _animator.SetTrigger("TakeItem");
        }
        if (_newItem)
        {
            _newItem.SetHand(this);
            _newItem.transform.SetParent(ItemPosition, false);
            _newItem.transform.position = ItemPosition.position;
            _newItem.transform.rotation = ItemPosition.rotation;
            _newItem._isPicked = true;
            _equipedItem = _newItem;

            _animator.SetBool("Trowable", (_newItem.GetItemType() == Item.ItemType.Trowable));
            

        }
    }

    public Item GetItem()
    {
        return _equipedItem;
    }
    private IEnumerator HotBarCd()
    {
        _hotbarCd = true;
        yield return new WaitForSeconds(0.1f);
        _hotbarCd = false;
    }



    public void SetAnimationTrigger(string trigger)
    {




        _animator.SetTrigger(trigger);
    }

    public void TryToUse()
    {
        if (_equipedItem != null)
        {
            _equipedItem.Use();
        }

    }



    public void ParryPressed()
    {
        if (!ParryReady) { return; }
        StartCoroutine(ProcessParry());

    }
    

    IEnumerator ProcessParry()
    {
        _animator.SetTrigger("Parry");
        ParryReady = false;
        float elapsedtime = 0;
        bool ParriedSomething = false;
        OnParryUpdated?.Invoke(0.0f);
        while (true)
        {
            Vector3 AttackPos = GameManager.Instance.GetPlayer().GetLookDretirection() * 0.5f + GameManager.Instance.Player.transform.position + new Vector3(0, 0.5f, 0);

            Collider[] collisions = Physics.OverlapSphere(AttackPos, 1.0f);


            foreach (Collider collider in collisions) {
    
                IParryable parried;

                if (collider.gameObject.TryGetComponent(out parried))
                {
                    
                    parried.Parry();
                    BuffManager.Instance.TriggerOnParry();
                    ParriedSomething = true;
                }
            }
            
            elapsedtime += Time.deltaTime;
            if (elapsedtime > 0.25f /*Esta es la ventana de parry*/) break;
            yield return null;
        }
        if (ParriedSomething)
        {
            print("Parreo");
            OnParryUpdated?.Invoke(1);
            ParryReady = true;
        }
        else
        {
            elapsedtime = 0;
            while (true)
            {

                elapsedtime += Time.deltaTime;
                print("No parreo y paso  " + Time.deltaTime);
                OnParryUpdated?.Invoke( elapsedtime / CdCooldown );
                if (elapsedtime > CdCooldown) { ParryReady = true; break; }

               yield return null;
                
            }
        }


      
    }

    private void OnDrawGizmos()
    {
        return;
        Vector3 AttackPos = GameManager.Instance.GetPlayer().GetLookDretirection() * 0.5f + GameManager.Instance.Player.transform.position + new Vector3(0, 0.5f, 0);

        Gizmos.DrawWireSphere(AttackPos, 1.0f);
    }
}
