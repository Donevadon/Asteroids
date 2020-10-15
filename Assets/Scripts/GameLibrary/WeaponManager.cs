using GameLibrary.ShotSystem;
using System.Collections.Generic;
using UnityEngine;

namespace GameLibrary
{
    /// <summary>
    /// Управляет вооружением
    /// </summary>
    class WeaponManager : MonoBehaviour
    {
        private List<Weapon> shipGuns = new List<Weapon>();
        private Weapon selectGun;
        /// <summary>
        /// Выстрелить из указанного оружия в указанном направлении
        /// </summary>
        /// <param name="gun"></param>
        /// <param name="direction"></param>
        public void Shoot(Weapons gun,Vector3 direction)
        {
            if (selectGun?.type != gun) selectGun = FindGun(gun);
            if (selectGun?.RechargeTime <= 0)
            {
                selectGun.Launch(direction);
            }
        }
        private void Awake()
        {
            InstallWeapons();
        }
        /// <summary>
        /// Установить оружие
        /// </summary>
        private void InstallWeapons()
        {
            foreach(Weapon gun in transform.GetComponents<Weapon>())
            {
                gun.CountCartridge_Updated += UpdateTextHandler;
                shipGuns.Add(gun);
            }
        }
        /// <summary>
        /// Найти оружие по типу
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        private Weapon FindGun(Weapons weapon)
        {
            foreach(var gun in shipGuns)
            {
                if (gun.type == weapon) return gun;
            }
            throw new System.Exception("Данное оружие отсутствует");
        }
        /// <summary>
        /// Обработчик обновления количества снарядов в орудиях
        /// </summary>
        /// <param name="count"></param>
        /// <param name="index"></param>
        private void UpdateTextHandler(int count,Weapons type)
        {
        }
    }
}
