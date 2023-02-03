using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class NotEngine : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private GameObject globe;
    [SerializeField] private GameObject front;
    [SerializeField] private GameObject back;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;
    private Vector3 zeroPos;
    private Vector2 moveDir;

    private Vector3 frontHitPos;
    private Vector3 backHitpos;
    private Vector3 leftHitpos;
    private Vector3 rightHitpos;

    private GameObject tmpFrontOBJ;
    private GameObject tmpBackOBJ;
    private GameObject tmpLeftOBJ;
    private GameObject tmpRightOBJ;

    private Vector3 dir1;
    private int layermask;
    private float dist = 10f;

    private float speed = 3f;

    private Quaternion nextRotFB;
    private Quaternion nextRotLR;
    private bool isLRMoving = false;


    //test용
    GameObject test_dir2;
    GameObject test_dir3;
    GameObject test_cross;
    GameObject test_cross2;

    private void Awake()
    {
        globe = GameObject.Find("Globe");
        zeroPos = globe.transform.position;
        tmpFrontOBJ = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tmpBackOBJ = GameObject.CreatePrimitive(PrimitiveType.Cube);
        /*tmpLeftOBJ = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tmpRightOBJ = GameObject.CreatePrimitive(PrimitiveType.Cube);*/
        
        dir1 = (transform.position - zeroPos) / globe.transform.lossyScale.x;

        layermask = 1 << LayerMask.NameToLayer("Globe");


        //test vector3
        test_dir2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        test_dir2.name = "dir2";
        test_dir3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        test_dir3.name = "dir3";
        test_cross = GameObject.CreatePrimitive(PrimitiveType.Cube);
        test_cross.name = "cross";
        test_cross2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        test_cross2.name = "cross2";

    }

    void Update()
    {
        
        GoBackMoving();
        LRMoving();
        CheckPos();

    }

    private void CheckPos()
    {
        if (Physics.Raycast(front.transform.position+dir1, zeroPos - front.transform.position, out RaycastHit hit1, dist, layermask))
        {
            //Debug.Log("front: " + front.transform.position+", dir: "+dir+ ", zeroPos: "+ zeroPos+", name: "+hit1.transform.name);
           
           frontHitPos = hit1.point;
           tmpFrontOBJ.transform.position = new Vector3(hit1.point.x, hit1.point.y, hit1.point.z);

        }
        if (Physics.Raycast(back.transform.position+dir1, zeroPos - back.transform.position, out RaycastHit hit2, dist, layermask))
        {
            backHitpos = hit2.point;
            tmpBackOBJ.transform.position = new Vector3(hit2.point.x, hit2.point.y, hit2.point.z);
        }
        if (Physics.Raycast(left.transform.position + dir1, zeroPos - left.transform.position, out RaycastHit hit3, dist, layermask))
        {
            leftHitpos = hit3.point;
            //tmpLeftOBJ.transform.position = new Vector3(hit3.point.x, hit3.point.y, hit3.point.z);
        }
        if (Physics.Raycast(right.transform.position + dir1, zeroPos - right.transform.position, out RaycastHit hit4, dist, layermask))
        {
            rightHitpos = hit4.point;
            //tmpRightOBJ.transform.position = new Vector3(hit4.point.x, hit4.point.y, hit4.point.z);
        }

        //Vector3 dir2 =hit1.point - hit2.point;
        //Vector3 dir3 = hit3.point - hit4.point;
        Vector3 dir2 = hit1.point - hit2.point;
        Vector3 dir3 =  hit4.point- hit3.point;
        Vector3 cross = Vector3.Cross(dir2.normalized, dir3.normalized); 

        nextRotFB = Quaternion.LookRotation(dir2);

        //테스트용 임시 코드
        test_dir2.transform.position = dir2;
        test_dir3.transform.position = dir3;
        test_cross.transform.position = cross;
        
        //nextRotLR = Quaternion.LookRotation(dir3);

        transform.rotation = nextRotFB;

        //transform.localRotation *= 
        //transform.rotation = Quaternion.LookRotation(frontHitPos, backHitpos - frontHitPos);
        //transform.LookAt(frontHitPos, backHitpos - frontHitPos);
        //transform.position = (hit1.point + hit2.point) / 2;
    }

    private void GoBackMoving()
    {
        if (moveDir.y != 0)
        {
            /*if (globe != null)
				transform.position += transform.forward * Time.deltaTime * moveSpeed * (globe.transform.lossyScale.x) * (moveDir.y);
			else */
            Vector3 tmppos = transform.position + (transform.forward * Time.deltaTime * moveSpeed * (moveDir.y));
            //transform.position += transform.forward * Time.deltaTime * moveSpeed * (moveDir.y);
            if (Physics.Raycast(tmppos, zeroPos - tmppos, out RaycastHit hit, dist, layermask)) {
                transform.position = hit.point;
            }
        }
    }

    private void LRMoving()
    {
        if (globe != null)
        {
            //var dir = this.transform.position - globe.transform.position;
            //transform.RotateAround(transform.position, dir, 0.5f * moveDir.x);
            //transform.RotateAround(transform.position, transform.up, 0.5f * moveDir.x);

            //transform.Rotate(transform.up, 0.5f * moveDir.x, Space.World);
            if (moveDir.x != 0)
            {
                isLRMoving = true;
                var dir = this.transform.position - globe.transform.position;
                transform.Rotate(dir, 0.5f * moveDir.x, Space.World);
            }
            else {
                isLRMoving = false;
            }
        }
        else if (moveDir.x != 0) StartCoroutine(Rotatetions(0.005f, moveDir.y * moveDir.x));
    }
    private void OnMove(InputValue value)
    {

        Vector2 input = value.Get<Vector2>();                 // 입력 받은 값을 가져오기

        //Debug.Log("x: " + input.x + ", y:" + input.y);
        if (input != null)
        {
            moveDir = input;

            //transform.position += transform.forward * Time.deltaTime * movementSpeed;
            //moveDirection = new Vector3(input.x, 0f, input.y);
            //Debug.Log($"SEND_MESSAGE : {input.magnitude}");
        }
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
            /*transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yRotation,
			transform.localEulerAngles.z);*/
            //transform.Rotate(transform.forward, yRotation);


            yield return null;
        }
    }

}
