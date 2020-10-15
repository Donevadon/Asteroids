using UnityEngine;

namespace GameLibrary.ShotSystem
{
    /// <summary>
    /// Загрузчик снарядов в орудие
    /// </summary>
    public abstract class Loader : MonoBehaviour
    {
        /// <summary>
        /// Получить снаряд к определённому орудию
        /// </summary>
        /// <param name="gun"></param>
        /// <returns></returns>
        public abstract Cartridge GetCartridge(Weapon gun);
    }
}
