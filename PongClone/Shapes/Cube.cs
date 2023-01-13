using SharpDX;

namespace PongClone.Shapes
{
    public class Square : IPlacebleObject
    {
        public bool Active { get { return _active; } set { if (value) Enable(); else Disable(); } }

        private bool _active = false;

        public bool IsConvex { get; } = true;

        public Vector2 Position { get; private set; }
        private Vector2 _prevPosition;

        public Vector2 Velocity { get; set; }

        public float Width { get; private set; }
        private float _prevWidth;
        public float Height { get; private set; }
        private float _prevHeight;

        public Vector2[] Vertices { get; private set; }

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
            if (Position == _prevPosition && Width == _prevWidth && Height == _prevHeight)
                return;

            Vertices[0] = new Vector2(-Width / 2, -Height / 2);
            Vertices[1] = new Vector2(Width / 2, -Height / 2);
            Vertices[2] = new Vector2(Width / 2, Height / 2);
            Vertices[3] = new Vector2(-Width / 2, Height / 2);
        }
    }
}
