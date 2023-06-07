// 다각형을 이루는 지 체크하는 class
//꼭지점 이외에 겹치는 부분이 있으면 다각형을 이루지 않음

public class PolygonChecker : MonoBehaviour
{
    public List<Vector3> posList; // 다각형을 이룰 좌표들의 리스트

    void Start()
    {
        bool isPolygon = CheckPolygon(posList);
        Debug.Log("Is Polygon: " + isPolygon);
    }

    bool CheckPolygon(List<Vector3> points)
    {
        if (points.Count < 3)
        {
            // 최소 3개의 점이 필요하므로 다각형을 이룰 수 없음
            return false;
        }

        // 마지막 점과 첫 번째 점을 연결하는 선분 확인
        if (AreSegmentsIntersecting(points[points.Count - 1], points[0], points))
        {
            return false;
        }

        // 인접한 점 쌍의 선분들이 겹치는지 확인
        for (int i = 0; i < points.Count - 1; i++)
        {
            if (AreSegmentsIntersecting(points[i], points[i + 1], points))
            {
                return false;
            }
        }

        // 모든 조건을 만족하면 다각형을 이룰 수 있음
        return true;
    }

    bool AreSegmentsIntersecting(Vector3 p1, Vector3 p2, List<Vector3> points)
    {
        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector3 p3 = points[i];
            Vector3 p4 = points[i + 1];

            if (p1 == p3 || p1 == p4 || p2 == p3 || p2 == p4)
            {
                // 점이 겹치는 경우는 제외
                continue;
            }

            if (Intersect(p1, p2, p3, p4))
            {
                // 두 선분이 겹치는 경우
                return true;
            }
        }

        return false;
    }

    bool Intersect(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        float ccw1 = CCW(p1, p2, p3);
        float ccw2 = CCW(p1, p2, p4);
        float ccw3 = CCW(p3, p4, p1);
        float ccw4 = CCW(p3, p4, p2);

        return (ccw1 * ccw2 < 0) && (ccw3 * ccw4 < 0);
    }

    float CCW(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return (p2.x - p1.x) * (p3.y - p1.y) - (p3.x - p1.x) * (p2.y - p1.y);
    }
}
