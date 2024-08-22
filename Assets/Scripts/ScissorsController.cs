using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsController : MonoBehaviour
{
    GameObject shelfParent;
    public GameManager gameManager;
    ClothesController cc;
    Vector3 target;
    //Sadece Z eksenini kullanması yettiği için float bir değer verip vector'e ekliyorum.
    public float ZPos;
    Vector3 firstPos;
    public float speed;
    float timer = 1f;
    int sec;
    bool enter;
    public bool returnBool;
    int next;
    int i = 0;

    private void Start()
    {
        shelfParent = GameObject.FindGameObjectWithTag("ShelfBox");
        enter = false;
        next = 0;
        firstPos = transform.position;
        returnBool = false;
    }
    private void Update()
    {
        //Makası belirlenen z eksenine gönderip getirme
        if (returnBool)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstPos, Time.deltaTime * speed);
            if (transform.position == firstPos)
            {
                returnBool = false;
            }
        }
        else if (!returnBool)
        {
            target = new Vector3(transform.position.x, transform.position.y, ZPos);
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            if (transform.position == target)
            {
                returnBool = true;
            }
        }
        //Trigger içeri girince kıyafetleri düşürüp ardından yok eden blok
        if (enter)
        {
            timer += Time.deltaTime * 7;
            sec = (int)(timer % 60);

            if (sec > next)
            {
                next++;
                if (sec == 11)
                {
                    timer = 0;
                    sec = 0;
                    next = 0;
                    i++;
                }
                GameObject c = shelfParent.transform.GetChild(i).GetChild(1).gameObject;
                //Düşecek objeyi parent'tan çıkarıyorum.
                c.transform.parent = null;
                //Objenin düşmesi için isKinematiğini false yaptım.
                c.GetComponent<Rigidbody>().isKinematic = false;
                //Hemen yok olmasını engellemek için
                StartCoroutine(WaitToFall(c, 3));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trigger")
        {
            //Update'e girebilmek için
            enter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Trigger")
        {
            //Çıkışta değerleri sıfırlıyor
            enter = false;
            timer = 0;
            sec = 0;
            next = 0;
            i = 0;
        }
    }

    IEnumerator WaitToFall(GameObject c, float sec)
    {
        //Objeleri yok etmek için düşüp ekrandan kaybolmasını bekliyor
        yield return new WaitForSeconds(sec);
        Destroy(c);
    }
}

