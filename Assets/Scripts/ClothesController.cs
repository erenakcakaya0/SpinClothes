using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesController : MonoBehaviour
{
    GameObject mainShelfClothes;
    GameObject mainShelf;
    public GameObject clothes;
    private void Start()
    {
        mainShelfClothes = GameObject.FindGameObjectWithTag("MainShelfClothes");
        mainShelf = GameObject.FindGameObjectWithTag("MainShelfClothes");
        //Kıyafetlere random renkler verdim.
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        gameObject.GetComponent<Renderer>().material.color = color;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trigger")
        {
            //Child'lar sürekli değiştiği için değişkenleri start'ta alamadım.
            int amount = GameObject.FindGameObjectWithTag("Trigger").GetComponent<ShelfParent>().clothesIndex;
            if (GameObject.FindGameObjectWithTag("ShelfBox").transform.GetChild(GameObject.FindGameObjectWithTag("Trigger").GetComponent<ShelfParent>().count) != null)
            {
                if (GameObject.FindGameObjectWithTag("ShelfBox").transform.GetChild(GameObject.FindGameObjectWithTag("Trigger").GetComponent<ShelfParent>().count).childCount < 11)
                {
                    CreateClothes(amount, GameObject.FindGameObjectWithTag("ShelfBox").transform.GetChild(GameObject.FindGameObjectWithTag("Trigger").GetComponent<ShelfParent>().count));
                }
                if (GameObject.FindGameObjectWithTag("ShelfBox").transform.GetChild(GameObject.FindGameObjectWithTag("Trigger").GetComponent<ShelfParent>().count).childCount == 11)
                {
                    GameObject.FindGameObjectWithTag("Trigger").GetComponent<ShelfParent>().count++;
                }
            }
        }
    }

    public void CreateClothes(int amountValue, Transform objectTransform)
    {
        //Kıyafet objesini yok ettim.
        Destroy(this.gameObject);
        float radius = 8f;
        //Masanın kenarlarında daire biçiminde objeleri create ettim.
        Vector3 newpos = new Vector3(Mathf.Cos((GameObject.FindGameObjectWithTag("Trigger").GetComponent<ShelfParent>().clothesIndex * Mathf.PI * 2f) / 10) * radius, 0.3f, Mathf.Sin((amountValue * Mathf.PI * 2f) / 10) * radius);
        GameObject child = Instantiate(clothes);
        child.transform.parent = objectTransform;
        child.transform.localPosition = newpos;
        GameObject.FindGameObjectWithTag("Trigger").GetComponent<ShelfParent>().clothesIndex++;
        GameObject.FindGameObjectWithTag("Trigger").GetComponent<ShelfParent>().numberOfClothes++;
    }
}
