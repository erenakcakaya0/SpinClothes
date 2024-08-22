using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool startBool;
    CanvasGroup canvas;
    [SerializeField] private Slider slider;
    [SerializeField] float sliderValue;
    [SerializeField] float sliderSpeed;
    bool decrease;
    bool pressed;
    GameObject startPanel;
    private void Start()
    {
        startBool = false;
        slider.maxValue = 1;
        slider.minValue = 0;
        decrease = false;
        pressed = false;
        canvas = GameObject.FindGameObjectWithTag("StartPanel").GetComponent<CanvasGroup>();
        startPanel = GameObject.FindGameObjectWithTag("Canvas");
    }
    void Update()
    {
        //Başlangıçta buraya girerek oyuncuya nasıl oynayacağını gösteren Canvası gösteriyor.
        if (!startBool)
        {
            if (Input.GetMouseButton(0))
            {
                pressed = true;
            }
            BeforeStarting();
            if (pressed)
            {
                Play();
            }
        }
    }

    public void Play()
    {
        StartCoroutine(WaitToAlpha(1));
        //Canvasın alfasını yavaş sayılabilecek bir hızda düşürüyor
        canvas.alpha -= Time.deltaTime * 10;
    }

    IEnumerator WaitToAlpha(int sec)
    {
        //Alfa sıfır olduktan sonra canvas'ı kapatıyor
        yield return new WaitForSeconds(sec);
        startBool = true;
        if (canvas.alpha == 0)
        {
            startPanel.SetActive(false);
        }
    }
    public void BeforeStarting()
    {
        //Slider ile oyuncunun yapması gereken hareket gösteriliyor
        if (sliderValue <= 1.1f && !decrease)
        {
            sliderValue += Time.deltaTime * sliderSpeed;
            slider.value = sliderValue;
            if (sliderValue >= 1)
            {
                decrease = true;
            }
        }
        else if (decrease)
        {
            sliderValue -= Time.deltaTime * sliderSpeed;
            slider.value = sliderValue;
            if (sliderValue <= 0)
            {
                decrease = false;
            }
        }
    }
}
