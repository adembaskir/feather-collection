using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static Movement Instance;
    public float moveSpeed;
    public float swipeSpeed;
    float touchPosX;
    private Camera cam;
    public GameObject firstCube;
    public Animator anim;
    public float turnSpeed;
    float rotSpeed;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        cam = Camera.main;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        transform.position += Vector3.forward * moveSpeed * Time.fixedDeltaTime;
        GameObject firstCube = AtmRush.instance.feather[0];
        if (Input.GetButton("Fire1"))
        {
            Move();
        }
        
    }
    private void Move()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.transform.localPosition.z;

        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            
            Vector3 hitVec = hit.point;
            hitVec.x = Mathf.Clamp(hitVec.x, -4.2f, 4.2f);
            hitVec.y = firstCube.transform.localPosition.y;
            hitVec.z = firstCube.transform.localPosition.z;
          

            firstCube.transform.localPosition = Vector3.MoveTowards(firstCube.transform.localPosition, hitVec, Time.fixedDeltaTime * swipeSpeed);
            
            if(firstCube.transform.localPosition.x >= 0f && firstCube.transform.localPosition.x <= 4f)
            {
                rotSpeed = turnSpeed * firstCube.transform.position.x;

                firstCube.transform.rotation =  Quaternion.Euler(0, 0,-rotSpeed);


            }
            if (firstCube.transform.localPosition.x <= 0f && firstCube.transform.localPosition.x >= -4f)
            {
                rotSpeed = turnSpeed * -firstCube.transform.position.x;
                firstCube.transform.rotation = Quaternion.Euler(0, 0, rotSpeed);
            }

        }
    }

}
