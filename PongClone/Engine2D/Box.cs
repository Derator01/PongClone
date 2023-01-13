using PongClone.Shapes;
using SharpDX;

namespace PongClone.Engine2D;

public struct Box
{
    public Vector2 Position;

    public float Height;
    public float Width;

    public Box(Vector2 position, float height, float width)
    {
        Position = position;

        Height = height;
        Width = width;
    }
    public Box(Square box)
    {
        Position = box.Position;

        Height = box.Height;
        Width = box.Width;
    }
}