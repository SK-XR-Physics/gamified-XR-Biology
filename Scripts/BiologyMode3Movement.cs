using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiologyMode3Movement : MonoBehaviour
{
    public FixedJoystickController moveSource;
    [SerializeField]
    private float _moveSpeed;

    public Rigidbody playerObject;
    public Transform playerObjToRotate;

    [SerializeField]
    private float _rotSpeed;

    public Animator anim;

    private void Update()
    {
        if (moveSource.Direction != Vector3.zero)
            anim.SetBool("isMoving", true);
        else
            anim.SetBool("isMoving", false);
    }

    private void FixedUpdate()
    {
        playerObject.velocity = (moveSource.Direction) * _moveSpeed;


        if (moveSource.Direction != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(Vector3.up, moveSource.Direction);

            playerObjToRotate.rotation = Quaternion.RotateTowards(playerObjToRotate.transform.rotation, toRotate, _rotSpeed * Time.deltaTime);
        }
    }
}