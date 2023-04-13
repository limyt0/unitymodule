using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//중력방향 바뀌면서 구체 위를 

public class GravChangeTest : MonoBehaviour
{
    public GameObject _globe;
    private Vector2 moveDir;
    public float moveSpeed = 4f;
    Vector3 defaultGrav;

    void Start()
    {
        if (_globe == null) _globe = GameObject.Find("Globe"); 
    }

    private void OnEnable()
    {
        if (_globe == null) _globe = GameObject.Find("Globe");
        defaultGrav = Physics.gravity;
        var dir = (transform.position - _globe.transform.position).normalized * (-9.81f);
        Physics.gravity = dir;
    }

    private void OnDisable()
    {
        Physics.gravity = defaultGrav;   
    }
   

    private void FixedUpdate()
    {
        GoBackMoving();
        LRMoving();
        NormalRot();
        Debug.Log("garv: " + Physics.gravity);
    }

    private void NormalRot()
    {
        if (moveDir.y != 0) {
            var dir = (transform.position - _globe.transform.position).normalized* (-9.81f);
            Debug.Log("dir: " + dir);
            Physics.gravity = dir;   
        }
    }

    private void GoBackMoving()
    {
        if (moveDir.y != 0)
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed * (moveDir.y);
        }
    }

    private void LRMoving()
    {
        if (_globe != null)
        {
            var dir = this.transform.position - _globe.transform.position;
            transform.RotateAround(transform.position, dir, 0.5f * moveDir.x);
        }
        else if (moveDir.x != 0) StartCoroutine(Rotatetions(0.005f, moveDir.y * moveDir.x));
    }

    private IEnumerator Rotatetions(float duration, float angle)
    {
        float startRotation = this.transform.localEulerAngles.y;
        float endRotation = startRotation + angle;

        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;

            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yRotation,
            transform.localEulerAngles.z);


            yield return null;
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("onmove");
        Vector2 input = context.ReadValue<Vector2>();
        if (input != null)
        {
            moveDir = input;
        }
    }
}
