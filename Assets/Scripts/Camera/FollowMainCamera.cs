using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMainCamera : MonoBehaviour
{
    private Camera _mainCamera;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;   
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        RaycastHit hit;
        Ray ray = _mainCamera.ScreenPointToRay(screenCenter);
        if(Physics.Raycast(ray, out hit))
        {
            transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }*/

        transform.position = new Vector3(_mainCamera.transform.position.x, transform.position.y, _mainCamera.transform.position.z) + offset;
    }
}
