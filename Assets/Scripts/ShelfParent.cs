using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShelfParent : MonoBehaviour
{
    GameObject mainShelf;
    GameObject shelfParent;
    GameObject trigger;
    public int hIndex;
    Rigidbody rb;
    GameObject canvas;
    CanvasGroup canvasGroup;
    bool gameOver;
    public int clothesIndex;
    public int newShelf;
    public int checkValue = 10;
    public int count;
    public int numberOfClothes;

    private void Start()
    {
        mainShelf = GameObject.FindGameObjectWithTag("MainShelf");
        shelfParent = GameObject.FindGameObjectWithTag("ShelfBox");
        trigger = GameObject.FindGameObjectWithTag("Trigger");
        rb = GameObject.FindGameObjectWithTag("MainShelf").GetComponent<Rigidbody>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        canvasGroup = GameObject.FindGameObjectWithTag("RestartPanel").GetComponent<CanvasGroup>();
        newShelf = -1;
        clothesIndex = 1;
        count = 0;
        numberOfClothes = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CollectableShelf")
        {
            //Rafların boylarını hIndexte tuttum.
            hIndex++;
            //Toplanma işleminin gerçekleşmesi için fonsksiyonları çağırıyorum.
            other.gameObject.GetComponent<CollectableShelf>().Collect();
            other.gameObject.GetComponent<CollectableShelf>().changeIndex(hIndex);
            //Objeyi Child yaptım.
            other.gameObject.transform.parent = shelfParent.transform;
            //Objenin artık "CollectableShelf" tagına ihtiyacı olmadığı için kaldırdım
            other.gameObject.tag = "Untagged";

            //Ana rafın Y ekseninde konumunu bir arttırıyorum. Çünkü yeni gelen raflar aşağıya konumlanacaklar.
            mainShelf.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y + 1, rb.transform.position.z);

            //Trigger'ın konumu da yükseliyor. Eğer yükselirse toplanması gereken nesnelerle collider'i çarpışmaz. Hep altta kalması için yükseldiği kadar alçaltıyorum.
            trigger.transform.localPosition = new Vector3(0, -hIndex, 0);
        }
        else if (other.gameObject.tag == "Obstacle")
        {
            hIndex--;
            if (count > 0)
            {
                //Count değeri kıyafetlerin yerleşeceği rafların index'ini veriyor
                count--;
            }
            for (int i = 0; i < shelfParent.transform.childCount; i++)
            {
                if (i == shelfParent.transform.childCount - 1)
                {
                    clothesIndex -= shelfParent.transform.GetChild(i).childCount;
                    //Düşen rafın içindeki kıyafet kadar kıyafeti azaltıyor
                    numberOfClothes -= shelfParent.transform.GetChild(i).childCount - 1;
                    shelfParent.transform.GetChild(i).parent = null;
                    trigger.transform.localPosition = new Vector3(0, -hIndex, 0);
                }
            }
            if (hIndex >= 0)
            {
                //Ana rafın transform.position.y'si bir azaltılıyor
                mainShelf.transform.position = new Vector3(rb.transform.position.x - 1, rb.transform.position.y - 1, rb.transform.position.z);
            }
            else
            {
                //Kaybedildiğinde burası çalışıyor
                //Canvas'ı açıyor
                canvas.SetActive(true);
                canvasGroup.alpha = 1;
                //Script'i kapatıyorum.
                mainShelf.GetComponent<ShelfController>().enabled = false;
            }
            //Çarptığı engele göre engelin colliderini kapatıyorum.
            if (other.GetComponent<BoxCollider>() == null)
            {
                other.GetComponent<CapsuleCollider>().enabled = false;
            }
            else
            {
                other.GetComponent<BoxCollider>().enabled = false;

            }
        }

        if (other.gameObject.tag == "Clothes")
        {
            //Kıyafet sayısı 10'a tam bölünüyorsa yeni kıyafetlerin yerleşmesi gereken rafın index'ini gösteren değer arttırılıyor.
            if (clothesIndex % 10 == 0)
            {
                newShelf++;
            }
        }
    }
    void RestartLevel()
    {
        //Yeniden başlamak için canvas'ı açıyor
        canvas.SetActive(true);
        canvasGroup.alpha = 1;
    }
}
