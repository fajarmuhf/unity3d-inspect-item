using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    public float speed = 5;
    [SerializeField]
    public float smoothInputSpeed = .2f;

    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity; 
    private Rigidbody currentRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        currentRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 input = new Vector2(horizontal,vertical);

        currentInputVector = Vector2.SmoothDamp(currentInputVector,input,ref smoothInputVelocity,smoothInputSpeed);

        Vector3 move = new Vector3(currentInputVector.x,0.0f,currentInputVector.y);

        move = transform.right * move.x + transform.forward *move.z;

        currentRigidbody.position += move *speed*Time.deltaTime;
    }
}
