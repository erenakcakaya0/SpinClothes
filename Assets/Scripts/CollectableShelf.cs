using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableShelf : MonoBehaviour
{
    bool isCollect;
    int index;
    private void Start()
    {
        //Başlangıçta bu nesne toplanmamış.
        isCollect = false;
    }
    private void Update()
    {
        //Nesne ana raf (Shelf Parent adındaki kontrol ettiğimiz rafın Child'ı) ile çarpıştığında isCollect true değer alıyor.
        if (isCollect)
        {
            if (transform.parent != null)
            {
                //Index'e göre objeyi konumlandırıyor.
                transform.localPosition = new Vector3(0, -index, 0);
            }
        }
    }

    //Çağırıldığında isCollect true değerini alması için
    public void Collect()
    {
        isCollect = true;
    }

    //Rafın index'ini ayarlıyor 
    public void changeIndex(int index)
    {
        this.index = index;
    }
}
