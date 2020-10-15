using UnityEngine;

namespace GameLibrary
{
    /// <summary>
    /// Объект на сцене
    /// </summary>
    public class ObjectScene : MonoBehaviour
    {
        /// <summary>
        /// Границы сцены
        /// </summary>
        [SerializeField] protected Vector2 _borders = new Vector2(9,5);
        /// <summary>
        /// Отследить касание границы сцены,для переноса объекта
        /// </summary>
        protected void CheckBorder()
        {
            if (transform.position.x > _borders.x)
                transform.position = new Vector3(-_borders.x, transform.position.y, transform.position.z);
            else if(transform.position.x < -_borders.x)
                transform.position = new Vector3(_borders.x, transform.position.y, transform.position.z);
            if (transform.position.y > _borders.y)
                transform.position = new Vector3(transform.position.x, -_borders.y, transform.position.z);
            else if(transform.position.y < -_borders.y)
                transform.position = new Vector3(transform.position.x, _borders.y, transform.position.z);
        }

        protected virtual void FixedUpdate()
        {
            CheckBorder();
        }
    }
}
