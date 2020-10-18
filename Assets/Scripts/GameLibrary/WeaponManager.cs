using GameLibrary.ShotSystem;
using System.Numerics;

namespace GameLibrary
{
    /// <summary>
    /// Управляет вооружением
    /// </summary>
    public class WeaponManager
    {
        private Weapon selectGun;
        public Weapon[] ShipGuns { get; set; }
        /// <summary>
        /// Выстрелить из указанного оружия в указанном направлении
        /// </summary>
        /// <param name="gun"></param>
        /// <param name="direction"></param>
        public void Shoot(Weapons gun,Vector3 position,Vector3 direction)
        {
            if (selectGun?.Type != gun) selectGun = FindGun(gun);
            if (selectGun?.isRecharged == false)
            {
                selectGun.Launch(position,direction);
            }
        }
        /// <summary>
        /// Найти оружие по типу
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        private Weapon FindGun(Weapons weapon)
        {
            foreach(var gun in ShipGuns)
            {
                if (gun.Type == weapon) return gun;
            }
            throw new System.Exception("Данное оружие отсутствует");
        }
    }
}
