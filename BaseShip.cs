using Microsoft.Xna.Framework;

namespace SpaceShip
{
    class BaseShip : Ship
    {
        public static readonly string name = "Main Ship";

        public BaseShip(Ship ship) : base(ship)
        {
        }

        public BaseShip(int moveSpeed, float damage, Vector2 bounds, Vector2 currentPos, Vector2 startPosition) : base(moveSpeed, damage, bounds, currentPos, startPosition)
        {

        }

        public override ISprite Clone()
        {
            return new BaseShip(this);
        }
    }
}
