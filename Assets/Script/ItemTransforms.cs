using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTransforms : MonoBehaviour
{
    public float Speed = 100f;

    //Controls the way the selected object behaves
    private void Update()
    {
        float x = Input.GetAxis("Mouse X") * Speed;
        float y = Input.GetAxis("Mouse Y") * Speed;
        ItemInteraction.currentObj.transform.Rotate(-Vector3.up * x, Space.World);
        ItemInteraction.currentObj.transform.Rotate(-Vector3.forward * y, Space.World);
    }
}