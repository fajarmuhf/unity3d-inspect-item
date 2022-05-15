using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    //Basically checks if the player is interacting with the item that is on layer "Interactable"
    Vector3 Initial_position;
    Quaternion Rot;
    [SerializeField]
    public LayerMask layerMask;

    public PlayerMove move;
    public PlayerRotate rotate;
    public Transform InspectZone;

    public static GameObject currentObj, manager;
    private Rigidbody rb;

    bool isInspecting = false;
    private void Start()
    {
        manager = GameObject.Find("PhysicsManager");
    }
    void Update()
    {
        RaycastHit info;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out info, 20f,layerMask))
        {
            if (Input.GetKeyDown(KeyCode.E) && !isInspecting)
            {
                StopAllCoroutines();
                currentObj = info.collider.gameObject;
                rb = currentObj.GetComponent<Rigidbody>();
                Initial_position = info.collider.transform.position;
                Rot = Quaternion.Euler(info.collider.transform.localEulerAngles);
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
                move.enabled = false;
                rotate.enabled = false;
                StartCoroutine(MoveToTarget(currentObj, InspectZone.position, 0.8f));
                isInspecting = true;
                manager.GetComponent<ItemTransforms>().enabled = true;
            }
        }
        if (isInspecting && Input.GetKeyDown(KeyCode.Q))
        {
            StopAllCoroutines();
            manager.GetComponent<ItemTransforms>().enabled = false;
            currentObj.transform.rotation = Quaternion.Euler(Rot.eulerAngles);
            StartCoroutine(MoveToTarget(currentObj, Initial_position, Time.deltaTime * 100f));
            isInspecting = false;
            move.enabled = true;
            rotate.enabled = true;
            if (rb != null)
            {
                StartCoroutine(TogglePhysics(rb, true, 5f));
            }
        }
    }
    IEnumerator MoveToTarget(GameObject MovedObj, Vector3 Target, float Speed)
    {
        while (MovedObj.transform.position != Target)
        {
            MovedObj.transform.position = Vector3.MoveTowards(MovedObj.transform.position, Target, Time.deltaTime * Speed);
            yield return null;
        }
    }
    IEnumerator TogglePhysics(Rigidbody rb, bool value, float TimeWait)
    {
        yield return new WaitForSeconds(TimeWait);
        rb.isKinematic = !value;
    }

}