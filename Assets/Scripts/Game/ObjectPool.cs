using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public GameObject item;
    public int size;

    private List<GameObject> objectPool;
   
    void Start()
    {
        objectPool = new List<GameObject>();
        for (int i = 0; i < size; i++)
        {
            GameObject obj = (GameObject)Instantiate(item);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }

    public GameObject GetItem()
    {
        foreach (GameObject i in objectPool)
        {
            if (!i.activeInHierarchy)
            {
                i.SetActive(true);
                return i;
            }
        }
        return null;
    }

    public void DisableItem(GameObject item)
    {
        item.SetActive(false);
    }
}
