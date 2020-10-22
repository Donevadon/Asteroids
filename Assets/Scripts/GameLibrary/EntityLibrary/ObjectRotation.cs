using System;
using System.Numerics;

namespace GameLibrary.EntityLibrary
{
    /// <summary>
    /// Вращает объект
    /// </summary>
    public class ObjectRotation
    {
        private float _rotation;

        public Vector3 EulerAngles { get;private set; }
        public Vector3 Direction { get; set; }
        private Vector3 Position { get; set; }
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
        public float Speed { get; set; }

        public event Action<Vector3> Euler_Updated;
        /// <summary>
        /// Обозначение объекта для самостоятельного вращения
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="Object_Rotate"></param>
        public ObjectRotation(Vector3 direction,ref Action Object_Rotate)
        {
            Direction = direction;
            Object_Rotate += Rotate;
        }
        /// <summary>
        /// Обозначение угла поворота у объекта, самостоятельно вращаться не способен
        /// </summary>
        public ObjectRotation()
        {

        }
        /// <summary>
        /// Вращение объекта для слежения за целью
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="Object_Rotate"></param>
        /// <param name="UpdatePosition_Delegate"></param>
        public ObjectRotation(Vector3 direction, 
            ref Action<Vector3,bool> Object_Rotate,
            Action<Action<Vector3>> UpdatePosition_Delegate,
            Action<Action<Vector3>> UpdateDirection_Delegate)
        {
            Direction = direction;
            Object_Rotate += RotateForTarget;
            UpdatePosition_Delegate(UpdatePosition);
            UpdateDirection_Delegate(UpdateDirection);
        }
        private void UpdateDirection(Vector3 direction) 
        {
            Direction = direction;
        }
        private void UpdatePosition(Vector3 position) 
        {
            Position = position;
        }
        public void SetRotation(Vector3 euler)
        {
            EulerAngles = euler;
        }
        /// <summary>
        /// Вращение объекта в сторону определённого объекта или координаты
        /// </summary>
        /// <param name="target"></param>
        public void RotateForTarget(Vector3 target,bool is3D)
        {
            Vector3 vector = target + (Direction * 10) - Position;
            float rotationZ = (float)(Math.Atan2(vector.X, vector.Y) * (360 / (Math.PI * 2)));
            if (is3D) EulerAngles = new Vector3(-rotationZ - 90, -90, 90);
            else EulerAngles = new Vector3(0,0,-rotationZ);
            InvokeUpdateEvent();
        }
        /// <summary>
        /// Вращение объекта с учётом заданного ускорения
        /// </summary>
        private void Rotate()
        {
            EulerAngles += Direction * _rotation * Speed;
            InvokeUpdateEvent();
        }

        private void InvokeUpdateEvent()
        {
            Euler_Updated?.Invoke(EulerAngles);
        }
    }
}