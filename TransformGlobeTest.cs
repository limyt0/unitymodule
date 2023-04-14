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

    private Vector3 prevPos;
    private Quaternion prevQua;
    private int saveInt = 0;

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
        FramedataSave();
    }

    //이전 프레임 데이터 저장
    private void FramedataSave()
    {
        /*saveInt += 1;
        if (prevPos == Vector3.zero) prevPos = transform.position;
        if (prevQua == Quaternion.identity) prevQua = transform.rotation;
        if (saveInt % 2 == 0) {
            prevPos = transform.position;
            prevQua = transform.rotation;
        }*/
        prevPos = transform.position;
        prevQua = transform.rotation;
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
        if (moveDir.y != 0)
        {
            var defaultPos = transform.position;
            var defaultQua = transform.rotation;

            var dir = _globe.transform.position - Camera.main.transform.position;

            var origin = transform.position + dir.normalized * (-100f);

            if (Physics.Raycast(origin, dir, out RaycastHit hit, float.PositiveInfinity, layerMask))
            {
                if (hit.normal != transform.up)
                {
                    transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                    transform.position = hit.point;
                }
            }

            //방향에 따라 vector 부호 바뀌게 함.
            int signs = (moveDir.y > 0) ? 1 : -1;

            if (Physics.Raycast(transform.position, transform.forward * signs, out RaycastHit hit2, transform.lossyScale.y))
            {
                //옆면의 평면에 hit normal을 정사영 시켜야함. 안 그러면 옆으로 튕겨나간 각도로 계산됨.
                Vector3 hitProj = VectorProjection(_globe.transform.position - transform.position, transform.forward * signs, hit2.normal);

                float ang = 90 - VectorAngles(transform.forward * signs, hitProj);
                ang = Mathf.Abs(ang);
                Debug.DrawRay(transform.position, transform.forward * signs, Color.green, 3f);
                Debug.DrawRay(hit2.point, hitProj, Color.red, 3f);
                Debug.DrawRay(hit2.point, hit2.normal, Color.yellow, 3f);

                if (ang > 75)
                {
                    if (prevPos == Vector3.zero && prevQua == Quaternion.identity)
                    {
                        transform.position = defaultPos;
                        transform.rotation = defaultQua;
                    }
                    else
                    {
                        transform.position = prevPos;
                        transform.rotation = prevQua;
                    }

                }
            }



        }
    }

    private float VectorAngles(Vector3 v1, Vector3 v2)
    {
        float dot = Vector3.Dot(v1.normalized, v2.normalized);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        return angle;
    }

    //v1, v2로 span한 평면에 대한 v3의 정사영 벡터
    private Vector3 VectorProjection(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        Vector3 normal = Vector3.Cross(v1, v2).normalized;  // 평면의 법선 벡터 계산
        Vector3 pointOnPlane = v1;  // 평면상의 임의의 점 선택
        Vector3 toPoint = v3 - pointOnPlane;  // 점 v3와 임의의 점을 지나는 직선을 연장한 점 P 계산
        float distance = Vector3.Dot(toPoint, normal);  // P와 평면 사이의 거리 계산
        Vector3 projectedVector = v3 - distance * normal;  // v3의 정사영된 벡터값인 v4 계산
        return projectedVector;
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
