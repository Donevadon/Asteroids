using GameLibrary;
using GameLibrary.ShotSystem;
using UnityEngine;

public class CartridgeLoader : Loader
{
    public override Cartridge GetCartridge(Weapon gun)
    {
        string path = $"Prefabs/Cartridge/{(Visualization.is3D ? "3D" : "2D")}/";

        switch (gun.type)
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
}
