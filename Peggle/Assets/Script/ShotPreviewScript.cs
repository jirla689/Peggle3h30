using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(LineRenderer))]
public class ShotPreviewScript : MonoBehaviour
{
    [Header("Preview")]
    public Transform firePoint;
    public LineRenderer lineRenderer;
    public int maxReflections = 3;
    public float maxDistance = 50f;
    public LayerMask collisionMask;

    void Update()
    {
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);

        Vector3 direction = MousePos - transform.position;
        DrawParabolicTrajectory(firePoint.position, direction*1.5f);
    }
    void DrawParabolicTrajectory(Vector2 origin, Vector2 velocity)
    {
        var points = new List<Vector3>();
        Vector2 pos = origin;
        Vector2 vel = velocity;
        float timeStep = 0.05f;
        int maxSteps = 60;

        for (int i = 0; i < maxSteps; i++)
        {
            points.Add(pos);

            // Simule un pas physique
            vel += Physics2D.gravity * timeStep;
            Vector2 nextPos = pos + vel * timeStep;

            // Vérifie collision entre deux points
            RaycastHit2D hit = Physics2D.Raycast(pos, (nextPos - pos).normalized,
                                                  Vector2.Distance(pos, nextPos), collisionMask);
            if (hit.collider != null)
            {
                points.Add(hit.point); // s'arrête à l'impact
                break;
            }

            pos = nextPos;
        }

        lineRenderer.positionCount = points.Count;
        for (int i = 0; i < points.Count; i++)
            lineRenderer.SetPosition(i, points[i]);
    }
}
