using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private Transform door;

    private bool isOpen = false;

    private Vector3 startingPos;
    private Vector3 openPos;
    private float duration = 0.5f;

    private void Start()
    {
        startingPos = door.position;
        openPos = startingPos + new Vector3(0, 5, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isOpen)
        {
            StopAllCoroutines();
            StartCoroutine(MoveDoor(openPos));
            isOpen = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (isOpen)
        {
            StopAllCoroutines();
            StartCoroutine(MoveDoor(startingPos));
            isOpen = false;
        }

    }

    private IEnumerator MoveDoor(Vector3 targetPos)
    {
        Vector3 initialPos = door.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            door.position = Vector3.Lerp(initialPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        door.position = targetPos; // Ensure the final position is set
    }
}
