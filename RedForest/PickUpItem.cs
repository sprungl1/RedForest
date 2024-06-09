using System;
using UnityEngine;
using UnityEngine.UI; // Make sure to include this if you're using UI components

public class PickupItem : MonoBehaviour
{
    public EnemyController wolf;
    public GameObject pigPickUp;
    public Delivery delivery;
    public WanderAndFlee wander;
    public bool HasItem;
    public GameObject pickupMessageUI; // Assign your text UI element in the inspector

    private void Start()
    {
        pigPickUp.SetActive(false);
        HasItem = false;
        pickupMessageUI.SetActive(false); // Make sure the message is hidden at start
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && wander.dead)
        {

            pickupMessageUI.SetActive(true); // Show the message

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickupMessageUI.SetActive(false); // Hide the message when player leaves
        }
    }

    private void Update()
    {

        if (wolf.robbed == true)
        {
            pigPickUp.SetActive(false);
        }

            if (delivery.delivered == true)
            {
            
                pigPickUp.SetActive(false);
            }

            if (pickupMessageUI.activeSelf && Input.GetKeyDown(KeyCode.E))
            {
                Pickup();

            }
        

       
            void Pickup()
            {
                HasItem = true;
                // Code to handle the actual pickup
                // For example, adding the item to the inventory, etc.
                Debug.Log("Item picked up!");

                // Deactivate the GameObject
                this.gameObject.SetActive(false);
            gameObject.SetActive(false);
                // Don't forget to hide the pickup message
                pickupMessageUI.SetActive(false);
                pigPickUp.SetActive(true);
            }
        
    }
}
