using System.Diagnostics;
using System.Numerics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal"); // "Horizontal" comes from input manager
        float vertical = Input.GetAxis("Vertical");

        //same as this.gameObject.transform. arguments are x, y, and z
        this.gameObject.transform.Translate(horizontal * moveSpeed * Time.deltaTime,
            vertical * moveSpeed * 2 * Time.deltaTime
            , 0); // delta time of update method called last time

        
        bool fire1 = Input.GetButtonDown("Fire1");
        if (fire1)
        {
            UnityEngine.Debug.Log("Fire pressed");
        }

    }
}