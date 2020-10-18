using System;
using System.Numerics;

namespace GameLibrary
{
    /// <summary>
    /// Задаёт движение объекту
    /// </summary>
    public class ObjectMovement : ObjectScene
    {
        private float _acceleration;
        /// <summary>
        /// Задать ускорение от 0 до 1
        /// </summary>
        public float? Acceleration
        { 
            set 
            {
                if (value.HasValue)
                {
                    if (value.Value <= 0) _acceleration = 0;
                    else if (value.Value >= 1) _acceleration = 1;
                    else _acceleration = value.Value;
                }
                else throw new Exception("Не допустимо значение Null в ускорении корабля");
            } 
        }
        /// <summary>
        /// Движение объекта
        /// </summary>
        public Vector3 Move(Vector3 position,Vector3 direction)
        {
            position = CheckBorder(position);
            return position + (direction * _acceleration);
        }
    }
}