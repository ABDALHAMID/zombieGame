using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Transform door;
    public Vector3 doorNextPosition;
    public Vector3 doorInistialPosition;
    public LayerMask PlayerLayer;

    public Vector3 checkBoxPosition, checkBoxSize;

    private float x, y, z;
    public float openSpeed;
    // Start is called before the first frame update
    void Start()
    {
        doorInistialPosition = door.localPosition;
        x = openSpeed * doorNextPosition.x / Mathf.Abs(doorNextPosition.x);
        if (doorNextPosition.x == 0) x = 0;
        y = openSpeed * doorNextPosition.y / Mathf.Abs(doorNextPosition.y);
        if (doorNextPosition.y == 0) y = 0;
        z = openSpeed * doorNextPosition.z / Mathf.Abs(doorNextPosition.z);
        if (doorNextPosition.z == 0) z = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.CheckBox(checkBoxPosition + transform.localPosition, checkBoxSize, Quaternion.identity, PlayerLayer))
        {
            if (Mathf.Abs(door.localPosition.x) < Mathf.Abs(doorNextPosition.x)) door.Translate(new Vector3(x * Time.fixedDeltaTime, 0, 0));
            if (Mathf.Abs(door.localPosition.y) < Mathf.Abs(doorNextPosition.y)) door.Translate(new Vector3(0, y * Time.fixedDeltaTime, 0));
            if (Mathf.Abs(door.localPosition.z) < Mathf.Abs(doorNextPosition.z)) door.Translate(new Vector3(0, 0, z * Time.fixedDeltaTime));
        }
        else
        {
            if (door.localPosition.x > Mathf.Abs(doorInistialPosition.x)) door.Translate(new Vector3(-x * Time.fixedDeltaTime, 0, 0));
            if (door.localPosition.y > Mathf.Abs(doorInistialPosition.y)) door.Translate(new Vector3(0, -y * Time.fixedDeltaTime, 0));
            if (door.localPosition.z > Mathf.Abs(doorInistialPosition.z)) door.Translate(new Vector3(0, 0, -z * Time.fixedDeltaTime));
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(checkBoxPosition + transform.localPosition, checkBoxSize);
    }
}
