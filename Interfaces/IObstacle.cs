using Microsoft.Xna.Framework;

namespace SpaceShip.Interfaces
{
    public interface IObstacle : ISprite
    {
        bool IsDisposed { get; set; }
        int FailedObjects { get; set; }
        void Delete() { }
        void Dispose();
        new IObstacle Clone();

        new abstract Vector2 CurrentPos { get; set; }

        void Update();
    }
}