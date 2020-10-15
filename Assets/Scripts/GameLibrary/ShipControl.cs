using UnityEngine;

namespace GameLibrary
{
    /// <summary>
    /// Управление кораблём
    /// </summary>
    public class ShipControl : MonoBehaviour
    {
        private ObjectMovement movement;
        private ObjectRotation rotation;
        private WeaponManager gunManager;
        /// <summary>
        /// Задаёт ускорение движению корабля
        /// Используется для передвижения
        /// Необходим компонент ShipMovement
        /// </summary>
        public float MoveAcceleration { set => movement.Acceleration = value; }
        /// <summary>
        /// Задаёт ускорение вращению корабля
        /// Использается для поворотов
        /// Необходим компонент ShipRotation
        /// </summary>
        public float RotateAcceleration { set => rotation.Rotation = value; }

        private void Awake()
        {
            movement = GetComponent<ObjectMovement>(); //ToDO: обработать отсутствие компонентов
            rotation = GetComponent<ObjectRotation>();
            gunManager = GetComponent<WeaponManager>();
        }
        /// <summary>
        /// Направить корабль в сторону цели
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public void RotateForTarget(Vector3 target,Vector3 direction,bool is3D)
        {
            rotation.RotateForTarget(target,direction,is3D);
        }
        /// <summary>
        /// Выстрелить из указанного оружия в указанную сторону
        /// Необходим компонент ShipGun и Loader и GunManager
        /// </summary>
        /// <param name="weapon"></param>
        /// <param name="direction"></param>
        public void Shoot(Weapons weapon,Vector3 direction)
        {
            gunManager.Shoot(weapon,direction);
        }
        /// <summary>
        /// Перемещать корабль в указанную сторону
        /// Необходим компонент ShipMovement
        /// </summary>
        /// <param name="direction"></param>
        public void Move(Vector3 direction)
        {
            movement.Move(direction);
        }
        /// <summary>
        /// Повернуть корабль в указанную сторону
        /// Необходим компонент ShipRotation
        /// </summary>
        /// <param name="rotateVector"></param>
        public void Rotate(Vector3 rotateVector)
        {
            rotation.Rotate(rotateVector);
        }
    }
}
