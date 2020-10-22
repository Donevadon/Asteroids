using GameLibrary;
using GameLibrary.EntityLibrary;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Lazer : Cartridge
{
    [SerializeField]private float distance;
    [SerializeField] private bool is3D;
    private Vector3[] linePoints = new Vector3[2];
    private LineRenderer lineRenderer;

    public override event Action<IEntity> Entity_Deaded;
    private ObjectMovement Movement { get; set; } = new ObjectMovement();
    private ObjectRotation RotationObject { get; set; } = new ObjectRotation();

    public override Sprite Image { get; }

    public override Entity Type => Entity.Lazer;

    public override System.Numerics.Vector3 Position
    {
        get => Movement.Position;
        set
        {
            Movement.SetPosition(value);
            transform.position = Movement.Position.Parse();
        }
    }
    public override System.Numerics.Vector3 Rotation
    {
        get => RotationObject.EulerAngles;
        set
        {
            RotationObject.SetRotation(value);
            transform.rotation = Quaternion.Euler(RotationObject.EulerAngles.Parse());
            Movement.Direction = transform.TransformDirection(Visualization.GetDirection()).Parse();
        }
    }

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

    private void Raycast2D()
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
        if (is3D) Raycast3D();
        else Raycast2D();
        yield return new WaitForSeconds(0.04f);
        Entity_Deaded(this);
        Destroy(gameObject);
        yield break;
    }
    private void Raycast3D()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward * distance));
        foreach (var hit in hits)
        {
            hit.transform.GetComponent<IGameEntity>()?.Dead();
        }
    }

    public override void UpdateData()
    {
        
    }

    public override void Destroy()
    {
        Destroy(gameObject);
    }
}
