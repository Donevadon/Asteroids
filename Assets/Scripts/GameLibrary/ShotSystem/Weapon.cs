using System;
using UnityEngine;

namespace GameLibrary.ShotSystem
{
    /// <summary>
    /// Оружие устанавливаемое с помощью GunManager
    /// </summary>
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField]private float chargeSpeed;
        [SerializeField]private int countCartridge;
        private Loader loader;
        private Cartridge cartridge;
        public Weapons type;
        [SerializeField]protected float timeToRecharge;
        /// <summary>
        /// Скорость заполнения прогресса создания снаряда
        /// </summary>
        protected float ChargeSpeed { get => chargeSpeed; set => chargeSpeed = value; }
        /// <summary>
        /// Прогресс создания нового снаряда от 0 до 100
        /// </summary>
        protected float charge { get; set; }
        /// <summary>
        /// Получить используемый снаряд
        /// </summary>
        protected Cartridge Cartridge 
        { 
            get
            {
                if (cartridge is null) cartridge = loader.GetCartridge(this);
                return cartridge;
            } 
        }
        /// <summary>
        /// Событие обновления данных о количестве снарядов
        /// </summary>
        public virtual event Action<int,Weapons> CountCartridge_Updated;
        /// <summary>
        /// Возвращает оставшееся времени перезарядки
        /// </summary>
        public float RechargeTime { get; protected set; }
        /// <summary>
        /// Записывает и возвращает количество доступных снарядов
        /// </summary>
        public int CountCartridge { get => countCartridge; protected set => countCartridge = value; }
        /// <summary>
        /// Изображение используемого снаряда
        /// </summary>
        public Sprite ImageCartridge => Cartridge.Image;
        /// <summary>
        /// Запустить снаряд
        /// </summary>
        /// <returns></returns>
        public abstract void Launch(Vector3 direction);
        /// <summary>
        /// Добавить снаряд за определённое количество времени
        /// </summary>
        public virtual void AddCartridge() 
        {
            if (charge >= 100)
            {
                CountCartridge_Updated?.Invoke(++CountCartridge, type);
                charge = 0;
            }
            else if(charge >= 0) charge += ChargeSpeed;
            print(charge);
        }
        /// <summary>
        /// Перезарядить оружие
        /// </summary>
        public void Recharge()
        {
            if (RechargeTime > 0) RechargeTime -= Time.deltaTime; 
        }
        private void Awake()
        {
            loader = GetComponent<Loader>() ?? throw new Exception("Отсутствует загрузчик патронов");
        }
    }
}