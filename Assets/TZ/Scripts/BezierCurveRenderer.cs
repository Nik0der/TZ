using UnityEngine;

public class BezierCurveRenderer : MonoBehaviour
{
    public static BezierCurveRenderer Instance;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int resolution = 20;

    private void Awake()
    {
        Instance = this;
    }

    public void DrawCurve(Vector3 start, Vector3 end)
    {
        Vector3 controlPoint = (start + end) / 2 + Vector3.up * 5f;
        lineRenderer.positionCount = resolution + 1;

        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            Vector3 point = CalculateQuadraticBezierPoint(t, start, controlPoint, end);
            lineRenderer.SetPosition(i, point);
        }
    }

    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }
}