using SharpDX;

namespace PongClone;

public interface IPlacebleObject
{
    bool Active { get; set; }

    bool IsConvex { get; }

    Vector2 Position { get; }

    Vector2 Velocity { get; set; }

    Vector2[] Vertices { get; }

    void Update();
}