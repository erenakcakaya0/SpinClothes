using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private Vector3 distance;
    private float speed = 1f;
    void Update()
    {
        //Bölüm bitiminde kameranın target'ını null'a eşitliyorum. Hata almamak için if bloğu ile kontrol ettirdim.
        if (target != null)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, target.transform.position + distance, speed);
        }

    }

}
