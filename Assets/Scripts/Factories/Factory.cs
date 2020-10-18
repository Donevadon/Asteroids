using GameLibrary;
using System.Numerics;
using UnityEngine;

public class Factory : MonoBehaviour,IEntityFactory
{
    public IGameEntity GetEntity(Entity entity, System.Numerics.Vector3 position, System.Numerics.Quaternion quaternion)
    {
        string path = $"Prefabs/Entity/{(Visualization.is3D ? "3D" : "2D")}/";
        return Instantiate(Resources.Load<GameObject>(path + entity.ToString()),position.Parse(),quaternion.Parse()).GetComponent<IGameEntity>();
    }
}
