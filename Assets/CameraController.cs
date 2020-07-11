using System.Collections;
using System.Collections.Generic;
using CellContents;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController player;

    // Update is called once per frame
    void Update()
    {
        var position = player.transform.position - player.baseLocalPosition;
        transform.position = new Vector3(position.x, position.y, -10f);
    }
}
