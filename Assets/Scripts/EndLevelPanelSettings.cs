using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndLevelPanelSettings : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public TextMeshProUGUI tMPro;
    public float timer = 1;
    public int sec;
    int score;
    private void Start()
    {
        canvasGroup = GameObject.FindGameObjectWithTag("EndPanel").GetComponent<CanvasGroup>();
    }
    private void Update()
    {
        //Level'in sonuna ulaşında bool true'ya dönüyor ve içeri giriliyor.
        if (GameObject.FindGameObjectWithTag("FinalTrigger").GetComponent<FinalTrigger>().finalPanelTrigger)
        {
            //Ana raf hIndexte tutulmadığı için extra raf almadığımızda hIindex 0 oluyor. Sıfırken puan hesaplanamadığı için
            //(Puan = Raf sayısı*kıyafet sayısı) puanı sadece kıyafetlere eşitledim. 
            if (GameObject.FindGameObjectWithTag("Trigger").GetComponent<ShelfParent>().hIndex == 0)
            {
                score = GameObject.FindGameObjectWithTag("Trigger").GetComponent<ShelfParent>().numberOfClothes;
            }
            else
            {
                score = GameObject.FindGameObjectWithTag("Trigger").GetComponent<ShelfParent>().numberOfClothes * GameObject.FindGameObjectWithTag("Trigger").GetComponent<ShelfParent>().hIndex;
            }
            //Puanı sıfırdan başlayıp puana kadar getiren görsel bir detay.
            if (sec < score)
            {
                timer += Time.deltaTime * 20;
                sec = (int)(timer % 3600);
            }
            tMPro.text = "Score: " + sec;
            canvasGroup.alpha += Time.deltaTime * 10;
        }
    }
}
