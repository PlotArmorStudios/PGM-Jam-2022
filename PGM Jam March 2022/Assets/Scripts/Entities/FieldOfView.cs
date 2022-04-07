using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstructionMask;
    [SerializeField] private float _meshResolution;

    [Range(0, 360)] public float Angle;
    public float Radius;
    public Player Player;
    public bool CanSeePlayer;

    private Entity _entity;

    
    private void Start()
    {
        _entity = GetComponent<Entity>();
        Player = _entity.PlayerTarget;
        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
        DrawFieldOfView();
    }

    private void DrawFieldOfView()
    {
        int rayCount = Mathf.RoundToInt(Angle * _meshResolution);
        float stepAngleSize = Angle / rayCount;
        List<Vector3> viewPoints = new List<Vector3>();
        
        for (int i = 0; i < rayCount; i++)
        {
            float angle = transform.eulerAngles.y - Angle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.Point);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = Vector3.zero;
    }

    private ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 direction = DirecionFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, Radius, _obstructionMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + direction * Radius, Radius, globalAngle);
        }
        
    }
    public struct ViewCastInfo
    {
        public bool Hit;
        public Vector3 Point;
        public float Distance;
        public float Angle;

        public ViewCastInfo(bool hit, Vector3 point, float distance, float angle)
        {
            Hit = hit;
            Point = point;
            Distance = distance;
            Angle = angle;
        }
    }
    
    public Vector3 DirecionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;

        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, Radius, _targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < Angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructionMask))
                    CanSeePlayer = true;
                else
                    CanSeePlayer = false;
            }
            else
            {
                CanSeePlayer = false;
            }
        }
        else if (CanSeePlayer)
            CanSeePlayer = false;
    }
}