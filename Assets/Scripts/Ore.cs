using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    [SerializeField]
    private GameObject outputItem;
    [SerializeField]
    private float smeltTime = 5.0f;

    private Coroutine currentSmeltTimer;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger occurred");
        if (other.gameObject.CompareTag("Forge"))
        {
            Debug.Log("Entered forge");
            currentSmeltTimer = StartCoroutine("SmeltOre", smeltTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger finished");
        if (other.gameObject.CompareTag("Forge"))
        {
            Debug.Log("Exited forge");
            if (currentSmeltTimer != null)
                StopCoroutine(currentSmeltTimer);
        }
    }

    private IEnumerator SmeltOre(float smeltTime)
    {
        yield return new WaitForSeconds(smeltTime);

        // Spawn new ingot and destroy ore
        Instantiate(outputItem, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
