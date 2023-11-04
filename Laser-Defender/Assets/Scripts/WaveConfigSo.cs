using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSo : ScriptableObject
{
    [SerializeField] private Transform pathPrefab;
    [SerializeField] private float shipSpeed = 5f;

    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWaypoints()
    {
        return pathPrefab.Cast<Transform>().ToList();
    }
    
    public float GetShipSpeed()
    {
        return shipSpeed;
    }
}
