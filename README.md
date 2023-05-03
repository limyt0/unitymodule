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
