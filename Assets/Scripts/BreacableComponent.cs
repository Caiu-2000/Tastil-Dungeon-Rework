using System.Collections.Generic;
using UnityEngine;

public class BreacableComponent : MonoBehaviour
{
    [SerializeField] List<Item> DropableItems;

    public void Breack()
    {
        if (DropableItems.Count > 0)
        {
            foreach (var item in DropableItems)
            {
                Item instance = Instantiate(item);
                instance.transform.position = this.transform.position + new Vector3(Random.Range(0,1) , 0 , Random.Range(0,1));
            }
            
        }
        Destroy(gameObject);
    }
}
