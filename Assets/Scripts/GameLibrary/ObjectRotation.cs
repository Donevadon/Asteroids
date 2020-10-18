using System;
using System.Numerics;

namespace GameLibrary
{
    /// <summary>
    /// Вращает объект
    /// </summary>
    public class ObjectRotation
    {
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
        /// Вращение объекта в сторону определённого объекта или координаты
        /// </summary>
        /// <param name="target"></param>
        public Vector3 RotateForTarget(Vector3 position,Vector3 target,Vector3 direction,bool is3D)
        {
            Vector3 vector = target - position;
            float rotationZ = (float)(Math.Atan2(vector.X, vector.Y) * (360 / (Math.PI * 2)));
            Vector3 rotation;
            if (is3D) rotation = new Vector3((direction * (rotationZ + 90)).Y, -90, 90);
            else rotation = direction * -rotationZ;
            return rotation;
        }
        /// <summary>
        /// Вращение объекта с учётом заданного ускорения
        /// </summary>
        public Vector3 GetRotateDirection(Vector3 direction)
        {
            return direction * _rotation;
        }
    }
}