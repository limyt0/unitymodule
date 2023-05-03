# MOdule 예정 목록

1. 이동관련

2. terrain, decal

3. panel

4. handle? event? callback delegate?

5.catmullrom 

standard decal



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
