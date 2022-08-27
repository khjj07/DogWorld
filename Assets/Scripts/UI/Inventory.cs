using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
public class Inventory : Singleton<Inventory>
{
    public List<Item> ItemList;
    // Start is called before the first frame update
    public async Task<bool> Add(Sprite newSprite)
    {
        foreach(var item in ItemList)
        {
            if(!item.ItemSprite)
            {
                item.Initialize(newSprite);
                return true;
            }
        }
        return false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
