using System;
using System.Numerics;

namespace GameLibrary.EntityLibrary
{
    /// <summary>
    /// Управление кораблём
    /// </summary>
    public class ShipControl
    {
        public ObjectMovement Movement { get; }
        public ObjectRotation Rotation { get; }
        public WeaponManager GunManager { get; }
        /// <summary>
        /// Даёт управление с возможностью стрельбы
        /// </summary>
        /// <param name="moveDirection"></param>
        /// <param name="Object_Move"></param>
        /// <param name="rotateDirection"></param>
        /// <param name="Object_Rotate"></param>
        public ShipControl(Vector3 moveDirection,ref Action Object_Move,Vector3 rotateDirection,ref Action Object_Rotate)
        {
            Movement = new ObjectMovement(moveDirection,
                ref Object_Move);
            Rotation = new ObjectRotation(rotateDirection,
                ref Object_Rotate);
            GunManager = new WeaponManager((x) => Movement.Position_Updated += x,
                (y) => Rotation.Euler_Updated += y);
        }
        /// <summary>
        /// Даёт управление без возможности стрельбы и только слежением за целью
        /// </summary>
        /// <param name="moveDirection"></param>
        /// <param name="Object_Move"></param>
        /// <param name="rotateDirection"></param>
        /// <param name="Object_Rotate"></param>
        public ShipControl(Vector3 moveDirection, ref Action Object_Move,Vector3 rotateDirection, ref Action<Vector3,bool> Object_Rotate)
        {
            Movement = new ObjectMovement(moveDirection, 
                ref Object_Move);
            Rotation = new ObjectRotation(rotateDirection, 
                ref Object_Rotate,
                (x) => Movement.Position_Updated += x);
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
