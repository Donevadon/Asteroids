using System.Numerics;

namespace GameLibrary
{
    /// <summary>
    /// Управление кораблём
    /// </summary>
    public class ShipControl
    {
        public ObjectMovement Movement { get; } = new ObjectMovement();
        public ObjectRotation Rotation { get; } = new ObjectRotation();
        public WeaponManager GunManager { get; } = new WeaponManager();
        /// <summary>
        /// Задаёт ускорение движению корабля
        /// Используется для передвижения
        /// </summary>
        public float MoveAcceleration { set => Movement.Acceleration = value; }
        /// <summary>
        /// Задаёт ускорение вращению корабля
        /// Использается для поворотов
        /// </summary>
        public float RotateAcceleration { set => Rotation.Rotation = value; }
        /// <summary>
        /// Направить корабль в сторону цели
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public Vector3 RotateForTarget(Vector3 position,Vector3 target, Vector3 direction, bool is3D) => Rotation.RotateForTarget(position,target, direction, is3D);
        /// <summary>
        /// Выстрелить из указанного оружия в указанную сторону
        /// </summary>
        /// <param name="weapon"></param>
        /// <param name="direction"></param>
        public void Shoot(Weapons weapon,Vector3 position, Vector3 direction) => GunManager.Shoot(weapon,position,direction);
        /// <summary>
        /// Перемещать корабль в указанную сторону
        /// </summary>
        /// <param name="direction"></param>
        public Vector3 Move(Vector3 position,Vector3 direction) => Movement.Move(position,direction);
        /// <summary>
        /// Повернуть корабль в указанную сторону
        /// </summary>
        /// <param name="rotateVector"></param>
        public Vector3 GetRotateDirection(Vector3 rotateVector)
        {
            return Rotation.GetRotateDirection(rotateVector);
        }
        /// <summary>
        /// Перезарядить все оружия. Использовать в цикле
        /// </summary>
        /// <param name="Time"></param>
        public void GunsRechargeAndAddCartridge(float Time)
        {
            foreach(var gun in GunManager.ShipGuns)
            {
                gun.Recharge(Time);
                gun.AddCartridge();
            }
        }
    }
}
