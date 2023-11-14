public class Method{

    /// <summary>
    /// 동서남북 중에 가장 가까운 방향으로 틀기
    /// </summary>
    private Quaternion CheckCloserDir(Vector3 dirForward, Vector3 dirUp)
    {
        Quaternion result = this.transform.rotation;
        if (Quaternion.LookRotation(dirForward, dirUp) != Quaternion.identity)
        {
            Vector3[] dirs = { north, east, west, south };
            float miniAngle = float.MaxValue;
            Vector3 closedir = Vector3.zero;
            foreach (Vector3 dir in dirs)
            {
                float angle = Vector3.Angle(dir, dirForward);
                if (angle < miniAngle) {
                    miniAngle = angle;
                    closedir = dir;
                }
            }
            result = Quaternion.LookRotation(closedir, dirUp);
        }
        return result;
    }
  
}

