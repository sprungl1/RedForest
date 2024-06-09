using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Delivery : MonoBehaviour
{
    public GameObject holidn_pig;
    public AudioSource ac;
    public AudioClip clip;
    public bool delivered;
    public bool HasItem;
    public GameObject item;
    public GameObject deliverMessageUI; // Assign your text UI element in the inspector
    public TextMeshProUGUI pigsDeliveredText; // Reference to the TextMeshPro Text object

    private int pigsDeliveredCount = 0;

    private void Start()
    {
        item = GameObject.FindGameObjectWithTag("pig");
        delivered = false;
        deliverMessageUI.SetActive(false); // Make sure the message is hidden at start
        UpdatePigsDeliveredText(); // Update the text when the game starts
    }

    void CheckItemActiveState()
    {
        if (holidn_pig.activeSelf)
        {
            HasItem = true;
        }
        else { HasItem = false; }
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckItemActiveState();
        if (other.CompareTag("Player") && HasItem == true && delivered == false)
        {
            deliverMessageUI.SetActive(true); // Show the message
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            deliverMessageUI.SetActive(false); // Hide the message when player leaves
        }
    }

    private void Update()
    {
        if (deliverMessageUI.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            Deliver();
        }
    }

    void Deliver()
    {
        ac.PlayOneShot(clip);
        delivered = true;
        HasItem = false;
        pigsDeliveredCount++; // Increase the count of pigs delivered
        UpdatePigsDeliveredText(); // Update the text
        StartCoroutine(ResetDeliveredFlag());
    }

    IEnumerator ResetDeliveredFlag()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        delivered = false;
    }

    // Function to update the pigs delivered count in the TextMeshPro Text object
    void UpdatePigsDeliveredText()
    {
        pigsDeliveredText.text = "Pigs Delivered: " + pigsDeliveredCount.ToString();
    }
}
