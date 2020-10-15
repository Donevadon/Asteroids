using System;
using UnityEngine;

namespace GameLibrary
{
    /// <summary>
    /// Вращает объект
    /// </summary>
    public class ObjectRotation : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private float _rotation;
        /// <summary>
        /// Задать ускорение поворота от 0 до 1
        /// </summary>
        public float? Rotation
        {
            set
            {
                if (value.HasValue)
                {
                    if (value.Value <= -1) _rotation = -1;
                    else if (value.Value >= 1) _rotation = 1;
                    else _rotation = value.Value;
                }
                else throw new Exception("Не допустимо значение Null во вращении корабля");
            }
        }
        /// <summary>
        /// Скорость вращения объекта
        /// </summary>
        public float Speed { get => _speed; set => _speed = value; }
        private int Inversia(int i) 
        {
            if (i == 0) return 1;
            else return 0;
        }
        /// <summary>
        /// Вращение объекта в сторону определённого объекта или координаты
        /// </summary>
        /// <param name="target"></param>
        public void RotateForTarget(Vector2 target,Vector3 direction,bool is3D)
        {
            Vector3 vector = target - (Vector2)transform.position;
            float rotationZ = Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
            Vector3 rotation;
            if (is3D) rotation = new Vector3((direction * (rotationZ + 90)).y, -90, 90);
            else rotation = direction * -rotationZ;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), Time.deltaTime * _speed);

        }
        /// <summary>
        /// Вращение объекта с учётом заданного ускорения
        /// </summary>
        public void Rotate(Vector3 direction)
        {
            transform.Rotate(direction * _rotation * _speed);
        }
    }
}