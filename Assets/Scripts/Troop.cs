using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop : MonoBehaviour
{
    public Tile.PlayerNumber playerIndex;
    public Vector3 targetPos;
    public Transform targetPosObject;
    public float speed = 5f;
    private void Start()
    {
        targetPos = transform.position;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
        targetPosObject.position = targetPos;
    }
}
