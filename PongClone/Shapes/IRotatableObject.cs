using SharpDX;

namespace PongClone;

public interface IRotatibleObject
{
    float Rotation { get; }

    Vector2[] Normals { get; }

    void Rotate();
}
