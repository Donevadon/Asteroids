using GameLibrary;
using UnityEngine;

public class Factory : EntityFactory
{
    public override GameEntity GetEntity(Entity entity, Vector2 position, Quaternion quaternion)
    {
        string path = $"Prefabs/Entity/{(Visualization.is3D ? "3D" : "2D")}/";
        return Instantiate(Resources.Load<GameEntity>(path + entity.ToString()),position,quaternion);
    }
}
