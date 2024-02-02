using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuskratController : MonoBehaviour
{
    Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x != 0)
            transform.Rotate(Vector3.up * x*2);
        if (y != 0)
        {
            transform.Translate(Vector3.forward * y * 0.1f);
            _anim.SetBool("isWalking", true);
        }
        else
        {
            _anim.SetBool("isWalking", false);
        }
    }
}
