using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SpawnStructure
{
    public Sprite sprite;
    public Transform data;
    public SpawnStructure(Sprite newSprite, Transform newdata)
    {
        sprite = newSprite;
        data = newdata;
    }

}


public class InitialSpawner : MonoBehaviour
{
    public List<SpawnStructure> spawnStructures = new List<SpawnStructure>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(var i in spawnStructures)
        {
            var instance = ItemManager.instance.MakeInstance(i.sprite);
            instance.transform.position = i.data.position;
            instance.GetComponent<IngameItem>().State = ObjectState.Fall;
        }
    }
}
