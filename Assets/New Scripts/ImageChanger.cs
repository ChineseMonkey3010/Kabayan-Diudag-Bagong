using static System.Net.Mime.MediaTypeNames;

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    public Sprite[] images; // Array of sprites to display
    public GameObject[] ObjectImages;
    public float changeInterval = 1.0f; // Time interval to change images
    //Player 1
    public int currentIndex = 0;
    public int currentIndex1 = 0;
    public int currentIndex2 = 0;
    //Player 2
    public int currentIndex3 = 0;
    public int currentIndex4 = 0;
    public int currentIndex5 = 0;// Index to keep track of the current image
    private UnityEngine.UI.Image displayImage; // Reference to the Image component
    private bool isCycling = false;
    public int tempindex1;
    public Button batten;

    void Start()
    {
        displayImage = GetComponent<UnityEngine.UI.Image>(); // Get the Image component attached to the GameObject
        if (images.Length > 0)
        {
            // Set the initial image to display
            displayImage.sprite = images[currentIndex];
        }
    }

 
    void Update()
    {
        // Toggle image cycling when 'X' key is pressed
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!isCycling)
            {
                StartCoroutine(StartStopImageCycle());
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnedObject(currentIndex);
            UnityEngine.Debug.Log("Spawned: " + currentIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnedObject(currentIndex1);
            UnityEngine.Debug.Log("Spawned: " + currentIndex1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpawnedObject(currentIndex2);
            UnityEngine.Debug.Log("Spawned: " + currentIndex2);

        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SpawnedObject(currentIndex);
            UnityEngine.Debug.Log("Spawned: " + currentIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SpawnedObject(currentIndex1);
            UnityEngine.Debug.Log("Spawned: " + currentIndex1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SpawnedObject(currentIndex2);
            UnityEngine.Debug.Log("Spawned: " + currentIndex2);

        }


    }

    IEnumerator StartStopImageCycle()
    {
        isCycling = true;
        float startTime = Time.time;

        while (Time.time - startTime < 5f) // Run for 5 seconds
        {
            yield return new WaitForSeconds(changeInterval);

            // Increment the index or reset to a random index if it exceeds the array length
            currentIndex = UnityEngine.Random.Range(0, images.Length);
            currentIndex1 = UnityEngine.Random.Range(0, images.Length);
            currentIndex2 = UnityEngine.Random.Range(0, images.Length);

            currentIndex3 = UnityEngine.Random.Range(0, images.Length);
            currentIndex4 = UnityEngine.Random.Range(0, images.Length);
            currentIndex5 = UnityEngine.Random.Range(0, images.Length);
            // Change the displayed image
            displayImage.sprite = images[currentIndex];
        }

        // Ensure that the final displayed image corresponds to the final currentIndex
        displayImage.sprite = images[currentIndex];
        UnityEngine.Debug.Log("Final Index: " + this.transform.name + " - " + currentIndex);
        UnityEngine.Debug.Log("Final Index: " + this.transform.name + " - " + currentIndex1);
        UnityEngine.Debug.Log("Final Index: " + this.transform.name + " - " + currentIndex2);



        isCycling = false;
    }

    public GameObject getCurrentGameObject()
    {
        return ObjectImages[currentIndex];
    }

    private void SpawnedObject(int i)
    {
        Instantiate(ObjectImages[i]);
    }

}

