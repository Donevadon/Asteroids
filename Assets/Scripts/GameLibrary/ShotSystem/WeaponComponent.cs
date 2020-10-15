using UnityEngine;

namespace GameLibrary.ShotSystem
{
    public class WeaponComponent : Weapon
    {
        public override void Launch(Vector3 direction)
        {
            if(CountCartridge > 0 || CountCartridge < 0)
            Instantiate(Cartridge, transform.position, transform.rotation * Quaternion.Euler(direction));
            RechargeTime = timeToRecharge;
            if (CountCartridge > 0) CountCartridge--;
        }

        private void Update()
        {
            AddCartridge();
            Recharge();
        }
    }
}