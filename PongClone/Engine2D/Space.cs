using PongClone.Shapes;
using SharpDX;
using System;
using System.Collections.Generic;

namespace PongClone.Engine2D;

public class Space
{
    public readonly List<Square> Objects = new();

    public readonly Vector2? Constraints;

    public Vector2 ConstantForce;

    public float Friction;

    public Space(Vector2? constraints, Vector2 constantForce, float friction)
    {
        Constraints = constraints;
        ConstantForce = constantForce;
        Friction = friction;
    }

    public void Update(TimeSpan deltaTime)
    {
        foreach (var obj in Objects)
        {
            if (obj.Velocity == Vector2.Zero)
                continue;


        }
    }

    public bool RayCast(this Vector2 start, Vector2 direction, float distance)
    {
        Vector2 end = start + direction * distance;

        foreach (var obj in Objects)
        {
            if (obj.Position.X - obj.Width / 2 < end.X && obj.Position.X + obj.Width / 2 < start.X && obj.Position.Y - obj.Width / 2 < end.Y && obj.Position.Y + obj.Width / 2 < start.Y)
                return true;
        }
    }

    public static bool IsPointInsideBox(Vector2 point, Box box)
    {
        float distX = point.X - box.Position.X;
        float distY = point.Y - box.Position.Y;
        return distX < box.Width / 2 && distX > -box.Width / 2 && distY < box.Height / 2 && distY > -box.Height / 2;
    }
}
