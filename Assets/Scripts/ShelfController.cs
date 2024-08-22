using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float turningSpeed;
    [SerializeField] private bool isClicked;
    [SerializeField] private float lerpValue;
    private Rigidbody shelfRb;
    private Camera cam;
    private Transform mainShelf;
    private void Start()
    {
        shelfRb = this.GetComponent<Rigidbody>();
        cam = Camera.main;
    }
    private void FixedUpdate()
    {
        //Ekrana tıklanmadığı zaman hareket etmemesi için true false döndürüyor
        isClicked = (Input.GetMouseButton(0) ? true : false);
        if (isClicked)
        {
            if (Input.GetButton("Fire1"))
            {
                Movement();
            }
            //Ileri hareket
            shelfRb.velocity = Vector3.left * speed * Time.fixedDeltaTime;
        }
    }
    private void Movement()
    {
        //Telefon ekranı için kullanılabilecek türden dönüşler
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit, 5000))
        {
            transform.position = Vector3.Lerp(shelfRb.transform.position, new Vector3(shelfRb.transform.position.x, shelfRb.transform.position.y, rayHit.point.z), lerpValue * Time.fixedDeltaTime);
        }
        this.transform.Rotate(this.transform.rotation.x, this.transform.rotation.y + turningSpeed * Time.fixedDeltaTime, this.transform.rotation.z);
    }
}
