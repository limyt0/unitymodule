//구체 표현 제대로 따라가는 지 Test필요
public class FollowSphereSurface : MonoBehaviour
{
    public GameObject sphere;
    private Vector3 surfaceNormal;
    private Vector3 surfaceTangent;
    
    void Start()
    {
        // Get the surface normal and tangent at the initial position
        surfaceNormal = GetSurfaceNormal(transform.position);
        surfaceTangent = GetSurfaceTangent(transform.position);
    }
    
    void Update()
    {
        // Calculate the new position and rotation
        Vector3 newPosition = sphere.transform.position + surfaceNormal * radius;
        Vector3 newForward = surfaceTangent;
        transform.position = newPosition;
        transform.rotation = Quaternion.LookRotation(newForward, surfaceNormal);
        
        // Update the surface normal and tangent for the new position
        surfaceNormal = GetSurfaceNormal(newPosition);
        surfaceTangent = GetSurfaceTangent(newPosition);
    }
    
    private Vector3 GetSurfaceNormal(Vector3 position)
    {
        // Calculate the surface normal using the sphere's Mesh Collider
        RaycastHit hit;
        if (Physics.Raycast(position, -position.normalized, out hit))
        {
            return hit.normal;
        }
        else
        {
            return position.normalized;
        }
    }
    
    private Vector3 GetSurfaceTangent(Vector3 position)
    {
      // Calculate the surface tangent using the sphere's Mesh Collider
      RaycastHit hit;
      if (Physics.Raycast(position, -position.normalized, out hit))
      {
          Vector3 normal = hit.normal;
          Vector3 tangent = Vector3.Cross(normal, position.normalized).normalized;
          return tangent;
      }
      else
      {
          // If the raycast doesn't hit anything, return the world up vector as the tangent
          return Vector3.up;
      }
    }
}
