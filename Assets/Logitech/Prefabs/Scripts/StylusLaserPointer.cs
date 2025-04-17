using UnityEngine;

public class StylusLaserPointer : MonoBehaviour
{
    public Transform pointerOrigin;     // The tip of the stylus
    public LineRenderer lineRenderer;   // The laser beam visual
    public float maxDistance = 10f;     // Max laser length

    void Update()
    {
        if (pointerOrigin == null || lineRenderer == null)
            return;

        Ray ray = new Ray(pointerOrigin.position, pointerOrigin.forward);

        // Start of laser
        lineRenderer.SetPosition(0, ray.origin);

        // Raycast to detect what the laser hits
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            // Laser ends at hit point
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // Laser ends at max distance
            lineRenderer.SetPosition(1, ray.origin + ray.direction * maxDistance);
        }
    }
}
