# MOdule 예정 목록

1. 이동관련

2. terrain, decal

3. panel

4. handle? event? callback delegate?

5.catmullrom 

standard decal

6. ray 원하는 위치 연습

7. textproMesh 연습

8. linerender or projector or decal로 다각형 그리기+UI에서도 그려보기

9. 페이징

10. List<Vector3> pathlist; =>경로 리스트
nowpos 현재 위치 정보 실시간으로 계속 삽입.( 데이터 간격 마음대로 조절 가능)
float dataterm = 0.1f => 값 바꾸면 데이터 간격 마음대로 조절 가능.
경로위를 따라 가장 가까운 곳에 가도록 계산하는 방법?

11. file 불러오기, 쓰기 json 불러오기 쓰기 등등

12. sub camera texture로 화면 회전

13. 등등

Gamemanager Scene전환, instance 


따라가는 UI가 화면 벗어나면 1번만 호출되게?(setactive를 한번씩만??)_


라그랑주 보간

```cs
public static double LagrangeInterpolation(double[] x, double[] y, double xi)
{
    double result = 0;
    for (int i = 0; i < x.Length; i++)
    {
        double term = y[i];
        for (int j = 0; j < x.Length; j++)
        {
            if (i != j)
            {
                term *= (xi - x[j]) / (x[i] - x[j]);
            }
        }
        result += term;
    }
    return result;
}
```


cubic spline 보간법
```cs
using System.Numerics;

double[] x = { 1, 2, 3, 4, 5 };
double[] y = { 2, 3, 1, 5, 4 };
double[] xi = { 2.5, 3.5 };

// Create cubic spline interpolator
CubicSpline spline = new CubicSpline();
spline.Interpolate(x, y);

// Interpolate new values
double[] yi = spline.Interpolate(xi);

// Print interpolated values
Console.WriteLine(string.Join(", ", yi));  // Output: 1.45, 4.05
```

메모리상으론 catmullrom이 더 좋은듯..?


//구체 위에 생성하는 방법: 다른 방법 찾아봐야 할 듯...
var dir = transform.position - sphere.transform.position; 
var q = Quaternion.LookRotation(dir, Vector3.up);
transform.rotation = q;
transform.Rotate(new Vector3(-90, 0, 180));
