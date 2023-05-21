using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Aero_jet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x >= 200f) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
