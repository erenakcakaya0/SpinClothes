using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTrigger : MonoBehaviour
{
    GameObject shelfParent;
    GameObject mainShelf;
    CameraSettings cameraObject;
    GameObject mainShelfClothes;
    public GameObject clothes;
    Transform end;
    GameObject canvas;
    GameObject restartPanel;
    bool camBool;
    public Vector3 distance;
    public bool finalPanelTrigger;
    public int score;

    private void Start()
    {
        shelfParent = GameObject.FindGameObjectWithTag("ShelfBox");
        mainShelfClothes = GameObject.FindGameObjectWithTag("MainShelfClothes");
        mainShelf = GameObject.FindGameObjectWithTag("MainShelf");
        end = GameObject.FindGameObjectWithTag("End").transform;
        cameraObject = GameObject.FindGameObjectWithTag("CameraObject").GetComponent<CameraSettings>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        restartPanel = GameObject.FindGameObjectWithTag("RestartPanel");

        camBool = false;
        finalPanelTrigger = false;
    }
    private void Update()
    {
        if (camBool)
        {
            //Oyunun sonuna ulaşıldığında kameranın konumunu değiştirdim.
            cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, distance, Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trigger")
        {
            //Ana rafın hareket etmesini önlemek için script'i kapattım.
            mainShelf.GetComponent<ShelfController>().enabled = false;
            int cIndex = GameObject.FindGameObjectWithTag("Trigger").GetComponent<ShelfParent>().numberOfClothes;

            //Puanı burada son trigger'a değdiğinde hesaplattım.
            score = cIndex * GameObject.FindGameObjectWithTag("Trigger").GetComponent<ShelfParent>().hIndex;

            //Child sayısının değişip for'u etkilememesi için başka bir değişkene atadım.
            int trigger = shelfParent.transform.childCount;

            //Döngüde kullandığım değişkenler
            int j = 0;
            float p1 = 0;
            float p2 = 0;
            bool left = true;

            //Camera ayarları
            camBool = true;
            cameraObject.target = null;

            //EndLevelPanel.cs'deki update'nin çalışması için gerekli bool
            finalPanelTrigger = true;

            canvas.SetActive(true);
            restartPanel.SetActive(false);

            //Child'ları (rafları) almak için döngü
            for (int i = 1; i < trigger; i++)
            {
                Transform child = shelfParent.transform.GetChild(shelfParent.transform.childCount - 1);
                child.GetComponent<Rigidbody>().isKinematic = true;
                child.parent = null;
                child.position = new Vector3(-8.3f + j, 0.61f, 0);
                j -= 2;

                //Child'ların (rafların) içindeki child'ları (kıyafetleri) döndürdüm.
                for (int y = 1; y < child.childCount; y++)
                {
                    Destroy(child.GetChild(y).gameObject);
                    //Sağa sola sırayla Instantiate olmaları için
                    if (left)
                    {
                        Vector3 newPos = new Vector3(-7.7f + p1, 0.7f, -1.7f);
                        Instantiate(clothes, newPos, Quaternion.identity);
                        p1 -= 0.5f;
                        left = false;
                    }
                    else
                    {
                        Vector3 newPos = new Vector3(-7.7f + p2, 0.7f, 1.7f);
                        Instantiate(clothes, newPos, Quaternion.identity);
                        left = true;
                        p2 -= 0.5f;
                    }

                }
            }
            //Ana rafı ayrı konrol ediyorum
            if (shelfParent.transform.childCount == 1)
            {
                mainShelf.GetComponent<Rigidbody>().isKinematic = true;
                mainShelf.transform.position = new Vector3(-8.3f + j, 0.61f, 0);
                for (int y = 1; y < mainShelfClothes.transform.childCount; y++)
                {
                    Destroy(mainShelfClothes.transform.GetChild(y).gameObject);
                    if (left)
                    {
                        Vector3 newPos = new Vector3(-7.7f + p1, 0.7f, -1.7f);
                        Instantiate(clothes, newPos, Quaternion.identity);
                        left = false;
                        p1 -= 0.5f;
                    }
                    else
                    {
                        Vector3 newPos = new Vector3(-7.7f + p2, 0.7f, 1.7f);
                        Instantiate(clothes, newPos, Quaternion.identity);
                        left = true;
                        p2 -= 0.5f;
                    }
                }
            }
        }
    }
}
