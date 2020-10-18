using GameLibrary;
using UnityEngine;

public class Visualization : MonoBehaviour
{
    public static bool is3D = true;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    public static Vector3 GetDirection()
    {
        return is3D ? Vector3.forward : Vector3.up;
    }

    public static Vector3 GetRandomEuler()
    {
        return is3D ? new Vector3(Random.Range(0, 360), 90, -90) : new Vector3(0, 0, Random.Range(0, 360));
    }

    public static Vector3 GetRotateVector()
    {
        return is3D ? Vector3.up : -Vector3.forward;
    }

    public static Vector3 GetEuler(Vector3 rotation)
    {
        return is3D ? new Vector3(rotation.z - 90, -90, 90) : new Vector3(0,0,rotation.x + 90);
    }

    public void ChangeVisualization()
    {
        is3D = !is3D;
        gameManager.ReloadEntity();
    }
}

