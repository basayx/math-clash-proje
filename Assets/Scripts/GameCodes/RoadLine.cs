using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadLine : MonoBehaviour
{
    public DefenderCannonCode defenderCannon;
    public Transform spawnPos;
    public Transform endPos;
    public List<Transform> potentialTrapPoses = new List<Transform>();
}
