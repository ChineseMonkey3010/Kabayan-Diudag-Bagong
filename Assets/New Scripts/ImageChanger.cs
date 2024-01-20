using static System.Net.Mime.MediaTypeNames;

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ImageChanger : MonoBehaviour
{
    public Sprite[] images; // Array of sprites to display
    public GameObject[] ObjectImages;
    public float changeInterval = 1.0f; // Time interval to change images
    //Player 1
    public int currentIndex = 0;
    public int currentIndex1 = 0;
    public int currentIndex2 = 0;
    private bool canPressedAlpha1 = true;
    private bool canPressedAlpha2 = true;
    private bool canPressedAlpha3 = true;
    public Transform instantiatePoint1; //for the passive and traps
    public Transform instantiatePoint2; //for the effects
    //Player 2
    public int currentIndex3 = 0;
    public int currentIndex4 = 0;
    public int currentIndex5 = 0;// Index to keep track of the current image
    private bool canPressedAlpha8 = true;
    private bool canPressedAlpha9 = true;
    private bool canPressedAlpha0 = true;
    public Transform instantiatePoint3; //for the passive and traps
    public Transform instantiatePoint4; //for the effects
    //Misc
    private UnityEngine.UI.Image displayImage; // Reference to the Image component
    private bool isCycling = false;
    public int tempindex1;
    public Button batten;
    public int speed;
    public LayerMask groundLayer;

    void Start()
    {
        isCycling = false;
        canPressedAlpha1 = true;
        canPressedAlpha8 = true;

        displayImage = GetComponent<UnityEngine.UI.Image>(); // Get the Image component attached to the GameObject
        if (images.Length > 0)
        {
            // Set the initial image to display
            displayImage.sprite = images[currentIndex];
        }
    }


    void FixedUpdate()
    {
        // Toggle image cycling when 'X' key is pressed
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!isCycling)
            {
                StartCoroutine(StartStopImageCycle());
            }
            else
            {
                canPressedAlpha1 = true;
                canPressedAlpha8 = true;

            }
        }

        if (!isCycling)
        {
            if (canPressedAlpha1 && Input.GetKeyDown(KeyCode.Alpha1))
            {
                SpawnedObject(currentIndex);
                UnityEngine.Debug.Log("Spawned: " + currentIndex);
                canPressedAlpha1 = false;
            }
            else if (canPressedAlpha1 && Input.GetKeyDown(KeyCode.Alpha2))
            {
                SpawnedObject(currentIndex1);
                UnityEngine.Debug.Log("Spawned: " + currentIndex1);
                canPressedAlpha1 = false;
            }
            else if (canPressedAlpha1 && Input.GetKeyDown(KeyCode.Alpha3))
            {
                SpawnedObject(currentIndex2);
                UnityEngine.Debug.Log("Spawned: " + currentIndex2);
                canPressedAlpha1 = false;
            }
            else if (canPressedAlpha8 && Input.GetKeyDown(KeyCode.Alpha8))
            {
                SpawnedObject(currentIndex3);
                UnityEngine.Debug.Log("Spawned: " + currentIndex3);
                canPressedAlpha8 = false;
            }
            else if (canPressedAlpha8 && Input.GetKeyDown(KeyCode.Alpha9))
            {
                SpawnedObject(currentIndex4);
                UnityEngine.Debug.Log("Spawned: " + currentIndex4);
                canPressedAlpha8 = false;
            }
            else if (canPressedAlpha8 && Input.GetKeyDown(KeyCode.Alpha0))
            {
                SpawnedObject(currentIndex5);
                UnityEngine.Debug.Log("Spawned: " + currentIndex5);
                canPressedAlpha8 = false;
            }
        }


    }

    IEnumerator StartStopImageCycle()
    {
        isCycling = true;
        float startTime = Time.time;
        canPressedAlpha1 = true; // Allow pressing Alpha1, Alpha2, Alpha3 again
        canPressedAlpha8 = true;

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
        UnityEngine.Debug.Log("Final Index: " + this.transform.name + " - " + currentIndex3);
        UnityEngine.Debug.Log("Final Index: " + this.transform.name + " - " + currentIndex4);
        UnityEngine.Debug.Log("Final Index: " + this.transform.name + " - " + currentIndex5);


        isCycling = false;
    }

    public GameObject getCurrentGameObject()
    {
        return ObjectImages[currentIndex];
    }

    private void SpawnedObject(int i)
    {
        GameObject spawnedObject = Instantiate(ObjectImages[i]);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.0f, groundLayer);
        StartCoroutine(MoveObjectOverTime(spawnedObject));
    }

    IEnumerator MoveObjectOverTime(GameObject obj)
    {
        while (true)
        {
            var movement = Time.deltaTime * speed;
            obj.transform.position = new Vector3(obj.transform.position.x - movement, obj.transform.position.y, obj.transform.position.z);

            yield return null; // Wait for the next frame
        }

    }
}

