using System.Numerics;
using DUCalculator.Web.Domain.HexGrid;

namespace DUCalculator.Web.Tests;

public class HexRingGeneratorTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Should_Generate_Hex_Grid()
    {
        var assertVectorHashSet = new HashSet<Vector3>
        {
            new(0f, 0f, 0f),
            new(0, -3, 1.7320508f),
            new(0, 0, 3.4641016f),
            new(0, 3, 1.7320508f),
            new(0, 3, -1.7320508f),
            new(0, 0, -3.4641016f),
            new(0, -3, -1.7320508f),
            new(0, -3, 1.7320508f),
            new(0, -6, 3.4641016f),
            new(0, -3, 5.196152f),
            new(0, 0, 6.928203f),
            new(0, 3, 5.196152f),
            new(0, 6, 3.4641016f),
            new(0, 6, 0),
            new(0, 6, -3.4641016f),
            new(0, 3, -5.196152f),
            new(0, 0, -6.928203f),
            new(0, -3, -5.196152f),
            new(0, -6, -3.4641016f),
            new(0, -6, 0),
            new(0, -6, 3.4641016f),
            new(0, -9, 5.196152f),
            new(0, -6, 6.928203f),
            new(0, -3, 8.660254f),
            new(0, 0, 10.392304f),
            new(0, 3, 8.660254f),
            new(0, 6, 6.9282026f),
            new(0, 9, 5.196152f),
            new(0, 9, 1.7320507f),
            new(0, 9, -1.7320509f),
            new(0, 9, -5.196152f),
            new(0, 6, -6.928203f),
            new(0, 3, -8.660254f),
            new(0, 0, -10.392304f),
            new(0, -3, -8.660254f),
            new(0, -6, -6.9282026f),
            new(0, -9, -5.196152f),
            new(0, -9, -1.7320507f),
            new(0, -9, 1.7320509f),
            new(0, -9, 5.196152f)
        };

        var hexPositions = HexRingGenerator.GenerateRings(
            new HexRingGenerator.HexRingOptions
            {
                RingCount = 3
            }
        );

        foreach (var hp in hexPositions)
        {
            Assert.That(assertVectorHashSet, Does.Contain(hp));
        }
    }
}