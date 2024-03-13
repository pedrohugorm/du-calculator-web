﻿using System.Numerics;
using DUCalculator.Web.Domain.LiveTrace;

namespace DUCalculator.Web.Domain.HexGrid;

public class HexGridGenerator : IHexGridGenerator
{
    public IHexGridGenerator.Result GenerateGrid(IHexGridGenerator.Settings settings)
    {
        // var startPointStr = "::pos{0,0,1058320,1238750,-3489237}";
        // var endPointStr = "::pos{0,0,1058320,1238750,-89237}";
        // var hexSize = 3.35f; // in SU
        // var numRings = 3;

        var hexSize = settings.HexSizeSu / 0.000005f;
        var startPoint = ParsePositionString(settings.StartPosition);
        var nominalPoint = ParsePositionString(settings.EndPosition);
        var dVec = nominalPoint - startPoint;
        var hexCenters3D = GenerateHexGrid3D(startPoint, nominalPoint, hexSize, settings.NumRings);

        var gridPositions = new List<IHexGridGenerator.WaypointLine>();

        var points = hexCenters3D.Count;
        for (var i = 0; i < points; i++)
        {
            var center = hexCenters3D[i];
            var nCenter = center + dVec;
            var waypointNumber = i + 1;

            if (i % 2 == 0)
            {
                gridPositions.Add(
                    new IHexGridGenerator.WaypointLine(
                        new IHexGridGenerator.Waypoint($"A{waypointNumber}", center.Vector3ToPosition()),
                        new IHexGridGenerator.Waypoint($"B{waypointNumber}", nCenter.Vector3ToPosition())
                    ).Reversed(settings.ReverseOrder)
                );
            }
            else
            {
                gridPositions.Add(
                    new IHexGridGenerator.WaypointLine(
                        new IHexGridGenerator.Waypoint($"B{waypointNumber}", nCenter.Vector3ToPosition()),
                        new IHexGridGenerator.Waypoint($"A{waypointNumber}", center.Vector3ToPosition())
                    ).Reversed(settings.ReverseOrder)
                );
            }
        }

        if (settings.ReverseOrder)
        {
            gridPositions.Reverse();
        }

        return new IHexGridGenerator.Result(
            gridPositions,
            0.000005 * VectorLength(dVec),
            points,
            (VectorLength(dVec) * points + hexSize * points) * 0.000005,
            0.000005 * VectorLength(dVec) * points,
            points * hexSize * 0.000005
        );
    }

    private double DegreesToRadians(double degrees)
    {
        return degrees * (Math.PI / 180.0);
    }

    private Vector3 HexToCartesian(Vector3 center, float size, float angle, float z = 0)
    {
        var x = center.X + size * (float)Math.Cos(DegreesToRadians(angle));
        var y = center.Y + size * (float)Math.Sin(DegreesToRadians(angle));
        return new Vector3(x, y, z);
    }

    private Vector3 ProjectOntoPlane(Vector3 point, Vector3 planeNormal, Vector3 planeOrigin)
    {
        var v = point - planeOrigin;
        var d = Vector3.Dot(v, planeNormal);
        var projectedPoint = point - d * planeNormal;
        return projectedPoint;
    }

    private List<Vector3> GenerateHexRing(Vector3 center, float size, int numHexagons, float z = 0)
    {
        float angle = 0;
        var hexOuterPoints = new List<Vector3>();

        for (var i = 0; i < numHexagons; i++)
        {
            hexOuterPoints.Add(HexToCartesian(center, size, angle, z));
            angle += 360.0f / numHexagons;
        }

        return hexOuterPoints;
    }

    private List<Vector3> GenerateHexGrid3D(Vector3 startPoint, Vector3 nominalPoint, float hexSize, int numRings)
    {
        var hexCenters3D = new List<Vector3> { startPoint };

        var planeNormal = Vector3.Normalize(nominalPoint - startPoint);

        for (var ring = 1; ring <= numRings; ring++)
        {
            var numHexagons = 6 * ring;
            for (var i = 0; i < numHexagons; i++)
            {
                var angle = i * 360.0f / numHexagons;
                var center = HexToCartesian(startPoint, hexSize * ring, angle, ring * nominalPoint.Z);
                var projectedCenter = ProjectOntoPlane(center, planeNormal, startPoint);
                hexCenters3D.Add(projectedCenter);
            }
        }

        return hexCenters3D;
    }

    private Vector3 ParsePositionString(string positionStr)
    {
        return positionStr.PositionToVector3();
    }

    private float VectorLength(Vector3 vector)
    {
        return vector.Length();
    }
}