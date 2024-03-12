//유티니에서 시계/반시계 체크하는 예시
//인터넷 예제들 보면 다 xz 평면에서만 체크하는데 모든 면 다 체크해줘야 하는 듯.
public class CCWClass : MonoBehaviour
{
    private bool CCW(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float result = 0;
        //xz평면기준
        Vector2 xz1 = new Vector2(p2.x - p1.x, p2.z - p1.z);
        Vector2 xz2 = new Vector2(p3.x - p1.x, p3.z - p1.z);

        //xy평면기준
        Vector2 xy1 = new Vector2(p2.x - p1.x, p2.y - p1.y);
        Vector2 xy2 = new Vector2(p3.x - p1.x, p3.y - p1.y);

        //yz평면기준
        Vector2 yz1 = new Vector2(p2.y - p1.y, p2.z - p1.z);
        Vector2 yz2 = new Vector2(p3.y - p1.y, p3.z - p1.z);

        //평면마다 겹쳐 보이는 지 체크. 체크 안 하면 0 - 0과 같은 식이 나와서 엉뚱하게 나옴.
        if (xz1.x * xz2.y != 0 || xz1.x * xz2.y != 0) //xz 평면에서 안 겹쳐 보일 때
        {
            result = xz1.x * xz2.y - xz1.y * xz2.x;
        }
        else if (xy1.x * xy2.y != 0 || xy1.y * xy2.x != 0) //xy 평면에서 안 겹쳐 보일 때
        {
            result = xy1.x * xy2.y - xy1.y * xy2.x;
        }
        else if (yz1.x * yz2.y != 0 || yz1.y * yz2.x != 0) //yz 평면에서 안 겹쳐 보일 때
        {
            result = yz1.x * yz2.y - yz1.y * yz2.x;
        }//모든 평면에서 겹쳐보이면 default인 0으로 하면 됨.

        // result < 0 반시계방향
        return result < 0 ? true : false;
    }
}
