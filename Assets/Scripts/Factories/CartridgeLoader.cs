using GameLibrary;
using GameLibrary.ShotSystem;
using UnityEngine;

public class CartridgeLoader : MonoBehaviour,ILoader
{
    public ICartridge GetCartridge(Weapon gun)
    {
        string path = $"Prefabs/Cartridge/{(Visualization.is3D ? "3D" : "2D")}/";

        switch (gun.Type)
        {
            case Weapons.BulletWearon:
                path += "Bullet";
                break;
            case Weapons.LazerWeapon:
                path += "Lazer";
                break;
        }
        return Resources.Load<Cartridge>(path);
    }

    public void InstantiateCartridge(ICartridge cartridge, System.Numerics.Vector3 position, System.Numerics.Vector3 direction)
    {
        Cartridge cartridgeEntity = (Cartridge)cartridge;
        Instantiate(cartridgeEntity, position.Parse(),Quaternion.Euler(direction.Parse()));
    }
}
