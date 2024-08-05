using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private Transform door;

    [SerializeField]
    private float minimumMagnitude = 1.5f; //minimum "magnitude of size" needed to make pressure plate activate

    private bool isOpen = false;
    private int objectsOnPlate = 0;

    private Vector3 doorStartingPos;
    private Vector3 doorOpenPos;
    private float doorMoveDuration = 0.5f;

    private Vector3 plateStartingPos;
    private Vector3 plateDownPos;
    private float plateMoveDuration = 0.2f;

    private void Start()
    {
        doorStartingPos = door.position;
        doorOpenPos = doorStartingPos + new Vector3(0, 5, 0);

        plateStartingPos = transform.position;
        plateDownPos = plateStartingPos + new Vector3(0, -0.05f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.GetComponent<Renderer>().bounds.size.magnitude);
        if (other.GetComponent<Renderer>().bounds.size.magnitude < minimumMagnitude)
        {
            return;
        }
        objectsOnPlate++;
        if (!isOpen && objectsOnPlate > 0)
        {
            StopAllCoroutines();
            StartCoroutine(MovePlate(plateDownPos));
            StartCoroutine(MoveDoor(doorOpenPos));
            isOpen = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Renderer>().bounds.size.magnitude < minimumMagnitude)
        {
            return;
        }
        objectsOnPlate--;
        if (isOpen && objectsOnPlate <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(MovePlate(plateStartingPos));
            StartCoroutine(MoveDoor(doorStartingPos));
            isOpen = false;
        }

    }

    private IEnumerator MoveDoor(Vector3 targetPos)
    {
        Vector3 initialPos = door.position;
        float elapsedTime = 0f;

        while (elapsedTime < doorMoveDuration)
        {
            door.position = Vector3.Lerp(initialPos, targetPos, elapsedTime / doorMoveDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        door.position = targetPos; // Ensure the final position is set
    }

    private IEnumerator MovePlate(Vector3 targetPos)
    {
        Vector3 initialPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < plateMoveDuration)
        {
            transform.position = Vector3.Lerp(initialPos, targetPos, elapsedTime / plateMoveDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        transform.position = targetPos; // Ensure the final position is set
    }
}
