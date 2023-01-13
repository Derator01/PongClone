using SharpDX;
using System;

namespace PongClone.Shapes;

public class ComplexPlacableObject : IRotatibleObject
{
    public bool Active { get { return _active; } set { if (value) Enable(); else Disable(); } }
    private bool _active = false;

    public bool IsConvex { get; } = true;

    public Vector2 Position { get; private set; }
    private Vector2 _prevPosition;

    public float Width { get; private set; }
    private float _prevWidth;
    public float Height { get; private set; }
    private float _prevHeight;
    public float Rotation { get; private set; }
    private float _prevRotation;

    public Vector2[] Vertices { get; private set; }
    public Vector2[] Normals { get; private set; }

    public ComplexPlacableObject(Vector2 position, float width, float height, float rotation)
    {
        Position = position;
        Width = width;
        Height = height;
        Rotation = rotation;
    }

    private void Enable()
    {
        if (_active)
            return;
        Update();


        _active = true;
    }

    private void Disable()
    {
        if (!_active)
            return;

        _active = false;
    }

    public void Update()
    {
        if (!_active)
            return;
        if (Position == _prevPosition && Rotation == _prevRotation && Width == _prevWidth && Height == _prevHeight)
            return;

        float cos = (float)Math.Cos(Rotation);
        float sin = (float)Math.Sin(Rotation);

        Vertices[0] = new Vector2(-Width / 2, -Height / 2);
        Vertices[1] = new Vector2(Width / 2, -Height / 2);
        Vertices[2] = new Vector2(Width / 2, Height / 2);
        Vertices[3] = new Vector2(-Width / 2, Height / 2);

        for (int i = 0; i < Vertices.Length; i++)
        {
            Vector2 vertex = Vertices[i];
            Vertices[i] = new Vector2(vertex.X * cos - vertex.Y * sin, vertex.X * sin + vertex.Y * cos);
            Vertices[i] += Position;
        }

        Normals[0] = new Vector2(-sin, -cos);
        Normals[1] = new Vector2(cos, -sin);
        Normals[2] = new Vector2(sin, cos);
        Normals[3] = new Vector2(-cos, sin);
    }

    bool CheckCollision(Polygon polygon1, Polygon polygon2)
    {
        List<Vector2> polygon1Vertices = new List<Vector2>(polygon1.Vertices);
        List<Vector2> polygon2Vertices = new List<Vector2>(polygon2.Vertices);
        List<Vector2> intersection = new List<Vector2>();

        // Perform the Sutherland-Hodgman algorithm for each edge of the first polygon
        for (int i = 0; i < polygon1Vertices.Count; i++)
        {
            Vector2 a = polygon1Vertices[i];
            Vector2 b = polygon1Vertices[(i + 1) % polygon1Vertices.Count];
            List<Vector2> inputList = new List<Vector2>(polygon2Vertices);
            polygon2Vertices.Clear();

            Vector2 s = inputList[inputList.Count - 1];
            for (int j = 0; j < inputList.Count; j++)
            {
                Vector2 e = inputList[j];
                if (IsInside(a, b, e))
                {
                    if (!IsInside(a, b, s))
                    {
                        Vector2 intersectionPoint = GetIntersection(a, b, s, e);
                        intersection.Add(intersectionPoint);
                        polygon2Vertices.Add(intersectionPoint);
                    }
                    polygon2Vertices.Add(e);
                }
                else if (IsInside(a, b, s))
                {
                    Vector2 intersectionPoint = GetIntersection(a, b, s, e);
                    intersection.Add(intersectionPoint);
                    polygon2Vertices.Add(intersectionPoint);
                }
                s = e;
            }
        }

        // Check if the intersection is not empty
        if (intersection.Count > 0)
        {
            // The polygons are colliding
            return true;
        }
        else
        {
            // The polygons are not colliding
            return false;
        }
    }

    bool IsInside(Vector2 a, Vector2 b, Vector2 c)
    {
        // Determine if a point is inside the square
        return (a.X - c.X) * (b.Y - c.Y) > (a.Y - c.Y) * (b.X - c.X);
    }

    Vector2 GetIntersection(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
    {
        // Get the intersection point of two lines
        float denominator = (c.Y - d.Y) * (a.X - b.X) - (c.X - d.X) * (a.Y - b.Y);
        float numerator1 = (c.X - d.X) * (a.Y - c.Y) - (c.Y - d.Y) * (a.X - c.X);
        float numerator2 = (a.X - b.X) * (a.Y - c.Y) - (a.Y - b.Y) * (a.X - c.X);
        float ua = numerator1 / denominator;
        return new Vector2(a.X + ua * (b.X - a.X), a.Y + ua * (b.Y - a.Y));
    }

    public void Lerp()
    {
        throw new NotImplementedException();
    }

    public void Rotate()
    {
        throw new NotImplementedException();
    }
}