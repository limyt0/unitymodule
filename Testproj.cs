//using OSGeo.GDAL;
//using OSGeo.OSR;
using OSGeo;
using OSGeo.OGR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using OSGeo.GDAL;
//using OSGeo.OSR;

public class Testproj : MonoBehaviour
{

    void Start()
    {
        test3();
    }

    private void test3()
    {
        // 변환 할 EPSG:32652 좌표
        double[] inputPoint = new double[] { 325221.80219674, 4173705.53746845 };

        // 좌표 변환할 좌표 시스템 정의
        SpatialReference src = new SpatialReference("");
        src.ImportFromEPSG(32652); // 입력 좌표 시스템 (EPSG:32652)

        SpatialReference dst = new SpatialReference("");
        dst.ImportFromEPSG(4326); // 출력 좌표 시스템 (EPSG:4326)

        double[] outputPoint = new double[2];
        using (CoordinateTransformation transformation = new CoordinateTransformation(src, dst))
        {
            transformation.TransformPoint(inputPoint);
        }

        Debug.Log($"InputPoint : {inputPoint[0]} / {inputPoint[1]}");

        //결과 값
        //InputPoint : 37.693926037 / 127.017611391999
    }

    void test1()
    {

        //Gdal.AllRegister();
        // Source EPSG 코드
        Debug.Log("test 1");
        string sourceEPSG = "EPSG:32652"; // WGS 84

        // Target EPSG 코드
        string targetEPSG = "EPSG:4326"; // Web Mercator

        // 좌표계 객체 생성
        Debug.Log("test 1");
        SpatialReference sourceRef = new SpatialReference("");
        Debug.Log("test 2");
        sourceRef.ImportFromEPSG(GetEPSGCode(sourceEPSG));

        SpatialReference targetRef = new SpatialReference("");
        targetRef.ImportFromEPSG(GetEPSGCode(targetEPSG));

        // 좌표계 변환 객체 생성
        CoordinateTransformation transformation = new CoordinateTransformation(sourceRef, targetRef);

        // 변환할 좌표
        double[] point = new double[] { 313724, 4160816, 0 }; // 경도, 위도, 고도

        // 좌표 변환
        transformation.TransformPoint(point);

        // 변환된 좌표 출력
        Debug.Log("변환된 좌표: " + point[0] + ", " + point[1] + ", " + point[2]);
    }

    int GetEPSGCode(string epsg)
    {
        int code;
        int.TryParse(epsg.Replace("EPSG:", ""), out code);
        return code;
    }


    private void Teste2()
    {
        Debug.Log("test2");
        try
        {
            
            SpatialReference src = new SpatialReference("");
            src.ImportFromProj4("+proj=latlong +datum=WGS84 +no_defs");
            Debug.Log("SOURCE IsGeographic:" + src.IsGeographic() + " IsProjected:" + src.IsProjected());
            SpatialReference dst = new SpatialReference("");
            dst.ImportFromProj4("+proj=somerc +lat_0=47.14439372222222 +lon_0=19.04857177777778 +x_0=650000 +y_0=200000 +ellps=GRS67 +units=m +no_defs");
            Debug.Log("DEST IsGeographic:" + dst.IsGeographic() + " IsProjected:" + dst.IsProjected());
            
            CoordinateTransformation ct = new CoordinateTransformation(src, dst);
            double[] p = new double[3];
            p[0] = 19; p[1] = 47; p[2] = 0;
            ct.TransformPoint(p);
            Debug.Log("x:" + p[0] + " y:" + p[1] + " z:" + p[2]);
            ct.TransformPoint(p, 19.2, 47.5, 0);
            Debug.Log("x:" + p[0] + " y:" + p[1] + " z:" + p[2]);
        }
        catch (Exception e)
        {
            Debug.Log("Error occurred: " + e.Message);
            //System.Environment.Exit(-1);
        }
    }
}
