using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeResetter : MonoBehaviour
{
    public GameObject cubePrefab; // Reference to the cube prefab
    public Transform originalPosition; // The original position where the cube should be reset

    [SerializeField]
    private GameObject RecursivePortalPair;

    [SerializeField]
    private Portal portal1;

    [SerializeField]
    private Portal portal2;

    [SerializeField]
    private Crosshair crosshair;

    GameObject copyObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickupable Object"))
        {
            copyObject = Instantiate(other.gameObject, originalPosition.position,originalPosition.rotation);
            copyObject.GetComponent<Rigidbody>().useGravity = true;
            copyObject.transform.localScale = new Vector3(1, 1, 1);
            Destroy(other.gameObject);
            //Instantiate(cubePrefab, originalPosition.position, originalPosition.rotation);
        }
        else if (other.CompareTag("Player"))
        {
            portal1.Reset();
            portal2.Reset();
            crosshair.ResetCrosshair();
        }
    }
   
}
