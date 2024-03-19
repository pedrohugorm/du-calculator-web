using DUCalculator.Web.Domain.LiveTrace;

namespace DUCalculator.Web.Domain.HexGrid;

using System;
using System.Numerics;

public class HexGridGenerator : IHexGridGenerator
{
    public IHexGridGenerator.Result GenerateGrid(IHexGridGenerator.Settings settings)
    {
        // Define variables
        var sPoint = settings.StartPosition;
        var ePoint = settings.EndPosition;
        float hexSize = settings.NumRings;
        var numRings = settings.NumRings;

        // Check if start and end points are different
        if (sPoint == ePoint)
        {
            Console.WriteLine("Start and end points must be different");
            return IHexGridGenerator.Result.Null();
        }
        else if (!sPoint.Contains("::pos{0,0,") || !ePoint.Contains("::pos{0,0,"))
        {
            Console.WriteLine("Invalid position formats (must use space coordinates only, not planet coordinates i.e. ::pos{0,0,...)");
            return IHexGridGenerator.Result.Null();
        }

        // Calculate translation vector
        hexSize /= 0.000005f;
        Vector3 startPoint = Vector3.Zero;
        Vector3 nominalPoint = new Vector3(0, 0, 1);
        var hexCenters3D = GenerateHexGrid3D(startPoint, nominalPoint, hexSize, numRings)
            .ToArray();

        Vector3[] originalPoints = new Vector3[hexCenters3D.Length];
        for (int i = 0; i < hexCenters3D.Count(); i++)
        {
            originalPoints[i] = hexCenters3D[i];
        }

        Vector3 SP = sPoint.PositionToVector3();
        Vector3 NP = ePoint.PositionToVector3();

        Vector3 originalPoint = originalPoints[0];
        Vector3 targetPoint = NP;

        Vector3 translationVector = CalculateTranslationVector(originalPoint, targetPoint);
        Vector3 originalVector = new Vector3(0, 0, 1);

        Vector3 targetVector = NP - SP;
        Matrix4x4 rotationMatrix = CalculateRotationMatrix(originalVector, targetVector);

        Vector3[] translatedAndRotatedPoints = Translate(Rotate(originalPoints, rotationMatrix), translationVector);

        var waypointLines = new List<IHexGridGenerator.WaypointLine>();
        
        // Output results
        float tDist = 0;
        float scanDist = 0;
        float repositionDist = 0;
        Vector3 curPos = Vector3.Zero;
        for (int i = originalPoints.Length - 1; i >= 0; i--)
        {
            Vector3 sPointVec = translatedAndRotatedPoints[i];
            Vector3 ePointVec = translatedAndRotatedPoints[i] + targetVector;
            string sPos = $"::pos{{0,0,{sPointVec.X},{sPointVec.Y},{sPointVec.Z}}}";
            string ePos = $"::pos{{0,0,{ePointVec.X},{ePointVec.Y},{ePointVec.Z}}}";
            
            tDist += Vector3.Distance(ePointVec, sPointVec);
            scanDist += Vector3.Distance(ePointVec, sPointVec);
            if (i % 2 == 0)
            {
                waypointLines.Add(
                    new IHexGridGenerator.WaypointLine(
                        new IHexGridGenerator.Waypoint($"A{i}", sPos, "A"),
                        new IHexGridGenerator.Waypoint($"B{i}", ePos, "B")
                    )
                );
                
                Console.WriteLine($"{i}A = '{sPos}',");
                Console.WriteLine($"{i}B = '{ePos}',");
                
                if (curPos != Vector3.Zero)
                {
                    tDist += Vector3.Distance(sPointVec, curPos);
                    repositionDist += Vector3.Distance(sPointVec, curPos);
                }
                
                curPos = ePointVec;
            }
            else
            {
                waypointLines.Add(
                    new IHexGridGenerator.WaypointLine(
                        new IHexGridGenerator.Waypoint($"B{i}", ePos, "B"),
                        new IHexGridGenerator.Waypoint($"A{i}", sPos, "A")
                    )
                );
                
                Console.WriteLine($"{i}B = '{ePos}',");
                Console.WriteLine($"{i}A = '{sPos}',");
                if (curPos != Vector3.Zero)
                {
                    tDist += Vector3.Distance(ePointVec, curPos);
                    repositionDist += Vector3.Distance(ePointVec, curPos);
                }
                curPos = sPointVec;
            }
        }

        Console.WriteLine($"\nTotal Distance: {tDist * 0.000005f:F2}su\nScanning Distance: {scanDist * 0.000005f:F2}su\nRepositioning Distance: {repositionDist * 0.000005f:F2}su");

        if (settings.ReverseOrder)
        {
            waypointLines = waypointLines
                .Select(x => x.Reversed(true))
                .ToList();
            waypointLines.Reverse();
        }
        
        return new IHexGridGenerator.Result(
            waypointLines,
            default,
            default,
            default,
            default,
            default
        );
    }
    
    public static Vector3 CalculateTranslationVector(Vector3 originalPoint, Vector3 targetPoint)
    {
        return targetPoint - originalPoint;
    }

    public static Matrix4x4 CalculateRotationMatrix(Vector3 originalVector, Vector3 targetVector)
    {
        originalVector = Vector3.Normalize(originalVector);
        targetVector = Vector3.Normalize(targetVector);

        Vector3 crossProduct = Vector3.Cross(originalVector, targetVector);
        float dotProduct = Vector3.Dot(originalVector, targetVector);

        Matrix4x4 skewSymmetricMatrix = new Matrix4x4(
            0, -crossProduct.Z, crossProduct.Y, 0,
            crossProduct.Z, 0, -crossProduct.X, 0,
            -crossProduct.Y, crossProduct.X, 0, 0,
            0, 0, 0, 1
        );

        Matrix4x4 rotationMatrix = Matrix4x4.Identity + skewSymmetricMatrix + 
                                   Matrix4x4.Multiply(skewSymmetricMatrix, skewSymmetricMatrix) * 
                                   ((1 - dotProduct) / crossProduct.LengthSquared());
        
        return rotationMatrix;
    }

    public static Vector3[] Translate(Vector3[] coordinates, Vector3 translationVector)
    {
        Vector3[] translatedCoordinates = new Vector3[coordinates.Length];
        for (int i = 0; i < coordinates.Length; i++)
        {
            translatedCoordinates[i] = coordinates[i] + translationVector;
        }
        return translatedCoordinates;
    }

    public static Vector3[] Rotate(Vector3[] coordinates, Matrix4x4 rotationMatrix)
    {
        Vector3[] rotatedCoordinates = new Vector3[coordinates.Length];
        for (int i = 0; i < coordinates.Length; i++)
        {
            rotatedCoordinates[i] = Vector3.Transform(coordinates[i], rotationMatrix);
        }
        return rotatedCoordinates;
    }

    public Vector3 HexToCartesian(Vector3 center, float size, float angle, float z = 0)
    {
        float x = center.X + size * (float)Math.Cos(DegreesToRadians(angle));
        float y = center.Y + size * (float)Math.Sin(DegreesToRadians(angle));
        return new Vector3(x, y, z);
    }

    public Vector3 ProjectOntoPlane(Vector3 point, Vector3 planeNormal, Vector3 planeOrigin)
    {
        Vector3 v = point - planeOrigin;
        float d = Vector3.Dot(v, planeNormal);
        return point - d * planeNormal;
    }

    public Vector3[] GenerateHexRing(Vector3 center, float size, int numHexagons, float z = 0)
    {
        float angle = 0;
        Vector3[] hexOuterPoints = new Vector3[numHexagons];

        for (int i = 0; i < numHexagons; i++)
        {
            hexOuterPoints[i] = HexToCartesian(center, size, angle, z);
            angle += 360f / numHexagons;
        }

        return hexOuterPoints;
    }

    public IEnumerable<Vector3> GenerateHexGrid3D(Vector3 startPoint, Vector3 nominalPoint, float hexSize, int numRings)
    {
        var hexCenters3D = new List<Vector3>
        {
            startPoint
        };

        Vector3 planeNormal = Vector3.Normalize(nominalPoint - startPoint);

        for (int ring = 1; ring <= numRings; ring++)
        {
            int numHexagons = 6 * ring;
            for (int i = 0; i < numHexagons; i++)
            {
                float angle = i * 360f / numHexagons;
                Vector3 center = HexToCartesian(startPoint, hexSize * ring, angle);
                Vector3 projectedCenter = ProjectOntoPlane(center, planeNormal, startPoint);
                hexCenters3D.Add(projectedCenter);
            }
        }

        return hexCenters3D;
    }
    
    private double DegreesToRadians(double degrees)
    {
        return degrees * (Math.PI / 180.0);
    }
}