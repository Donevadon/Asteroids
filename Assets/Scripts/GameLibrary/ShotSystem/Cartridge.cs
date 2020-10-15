using UnityEngine;

namespace GameLibrary.ShotSystem
{
    /// <summary>
    /// Оружейный снаряд
    /// </summary>
    public abstract class Cartridge : MonoBehaviour
    {
        /// <summary>
        /// Изображение снаряда
        /// </summary>
        public abstract Sprite Image { get; }
    }
}
