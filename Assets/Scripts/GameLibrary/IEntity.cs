using System;
using System.Numerics;

namespace GameLibrary
{
    public interface IEntity
    {
        Vector3 Position { get; set; }
        Vector3 Rotation { get; set; }
        Entity Type { get; }
        event Action<IEntity> Entity_Deaded;
        void UpdateData();
        void Destroy();
    }
}
