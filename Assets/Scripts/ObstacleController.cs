using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    //[SerializeField] Vector3 target;
    [SerializeField] float speed = 1;
    bool returnBool;
    Vector3 target;
    //Sadece Y eksenini kullanması yettiği için float bir değer verip vector'e ekliyorum.
    public float YPos;
    Vector3 firstPos;


    private void Start()
    {
        firstPos = transform.position;
        returnBool = false;

    }
    private void Update()
    {
        //Duvar engelini yukarı aşağı hareket ettiriyor.
        if (returnBool)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstPos, Time.deltaTime * speed);
            if (transform.position == firstPos)
            {
                StartCoroutine(WaitDownstairs());
            }
        }
        else if (!returnBool)
        {
            target = new Vector3(transform.position.x, YPos, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            if (transform.position == target)
            {
                StartCoroutine(WaitUpstairs());
            }
        }
    }
    IEnumerator WaitDownstairs()
    {
        //Minik bir bekleme ekliyor.
        yield return new WaitForSeconds(0.6f);
        returnBool = false;

    }
    IEnumerator WaitUpstairs()
    {
        //Minik bir bekleme ekliyor.
        yield return new WaitForSeconds(0.6f);
        returnBool = true;
    }
}
