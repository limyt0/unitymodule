using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TransformGlobeTest : MonoBehaviour
{
    public GameObject _globe;
    private Vector2 moveDir;
    public float moveSpeed = 4f;
    int layerMask;
    public float maxDegreesPerSecond = 90f;
    void Start()
    {
        if (_globe == null) _globe = GameObject.Find("Globe");
    }
    private void OnEnable()
    {
        layerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");
        layerMask = ~layerMask;
        if (_globe == null) _globe = GameObject.Find("Globe");

    }

    void Update()
    {
        GoBackMoving();
        LRMoving();
        NormalCheck();
    }

    private void GoBackMoving()
    {
        if (moveDir.y != 0)
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed * (moveDir.y);
        }
    }

    private void NormalCheck()
    {
        if (moveDir.y != 0) {
            Debug.Log("각도0:" + VectorAngles(transform.forward, _globe.transform.position - transform.position));
            var defaultPos = transform.position;
            var defaultQua = transform.rotation;

            var dir = _globe.transform.position - Camera.main.transform.position;
            //_globe.transform.position - transform.position;
            var origin = transform.position + dir.normalized * (-100f);
            //nG.transform.position = new Vector3(newPos.x, newPos.y, (float)(newPos.z - ChangeM(maxMeter)));
            //if (Physics.Raycast(nG.transform.position, zeroPoint - Camera.main.transform.position, out RaycastHit hit))
            if (Physics.Raycast(origin, dir, out RaycastHit hit, float.PositiveInfinity, layerMask))
            {
                if (hit.normal != transform.up)
                {   
                    transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                       
                    transform.position = hit.point;
                }
                /*if (hit.normal != transform.up)
                {
                    Quaternion currentRotation = transform.rotation;
                    Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

                    float maxDegreesDelta = maxDegreesPerSecond * Time.deltaTime;
                    transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, maxDegreesDelta);

                    transform.position = hit.point;
                }*/
               
            }
            float ang = VectorAngles(transform.forward, _globe.transform.position - transform.position);

/*
            float distDefault = Vector3.Distance(defaultPos, _globe.transform.position);
            float distAfter = Vector3.Distance(transform.position, _globe.transform.position);

            bool isCanMoving = true;
            if (distDefault >= distAfter) {//높은곳에서 낮은곳으로는 떨어져도 됨.
                isCanMoving = true;
            } else if(distAfter > transform.lossyScale.y) {
                isCanMoving = false;
             //높은곳 올라갈 때 자기보다 큰 곳 못 올라가야함.
            }
                */


            if ( ang < 45 || ang > 135) 
            {
                Debug.Log("각도1: " + ang);
                transform.position = defaultPos;
                transform.rotation = defaultQua;
                Debug.Log("각도3:" + VectorAngles(transform.forward, _globe.transform.position - transform.position));
            }
            else {
                Debug.Log("각도2: " + ang);
            }
            
        }
    }

    private float VectorAngles(Vector3 v1, Vector3 v2)
    {
        float dot = Vector3.Dot(v1.normalized, v2.normalized);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        return angle;
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
        //Debug.Log("onmove");
        Vector2 input = context.ReadValue<Vector2>();
        if (input != null)
        {
            moveDir = input;
        }
    }
}
