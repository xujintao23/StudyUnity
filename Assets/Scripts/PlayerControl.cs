using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    [SerializeField] private float speed;
    void Start()
    {
        
    }


    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        
        transform.Translate(new Vector3(h,0,v) * (speed * Time.deltaTime));
        
    }
}
