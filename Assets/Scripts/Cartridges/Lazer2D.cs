using GameLibrary;
using GameLibrary.ShotSystem;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Lazer2D : Cartridge
{
    [SerializeField]private float distance;
    private Vector3[] linePoints = new Vector3[2];
    private LineRenderer lineRenderer;

    public override Sprite Image { get; }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        StartCoroutine(Attack());
    }

    private void DrawLine(Vector3 vector)
    {
        linePoints[0] = transform.position;
        linePoints[1] = transform.TransformDirection(vector) * distance;
        lineRenderer.SetPositions(linePoints);
    }

    private void Raycast()
    {
        RaycastHit2D[] hits2D = Physics2D.RaycastAll(transform.position, transform.TransformDirection(Vector3.up * distance));
        foreach (var hit in hits2D)
        {
            hit.transform.GetComponent<IGameEntity>()?.Dead();
        }
    }

    private IEnumerator Attack()
    {
        DrawLine(Visualization.GetDirection());
        Raycast();
        yield return new WaitForSeconds(0.04f);
        Destroy(gameObject);
        yield break;
    }
}
