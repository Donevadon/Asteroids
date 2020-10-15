using System;
using UnityEngine;

namespace GameLibrary
{
    /// <summary>
    /// Задаёт движение объекту
    /// </summary>
    public class ObjectMovement : ObjectScene
    {
        [SerializeField] private float _speed;
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
        /// Скорость движения объекта
        /// </summary>
        public float Speed { get => _speed; set => _speed = value; }
        /// <summary>
        /// Движение объекта
        /// </summary>
        public void Move(Vector3 direction)
        {
            transform.Translate(direction * _acceleration * _speed * Time.deltaTime);
        }
    }
}