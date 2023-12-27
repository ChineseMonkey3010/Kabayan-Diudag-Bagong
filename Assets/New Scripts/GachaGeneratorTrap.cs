using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel.Design.Serialization;
using System.Collections.Specialized;
using System;

public class GachaGeneratorTrap : MonoBehaviour
{
    //[SerializeField] private GameObject trap;
    //[SerializeField] private GameObject effects;
    //[SerializeField] private GameObject passives; 

    //[SerializeField] private GameObject trapContainer;
    //[SerializeField] private GameObject effectContainer;
    //[SerializeField] private GameObject passiveContainer; 

    //[SerializeField] private Button generateButton;

    //private TextMeshProUGUI[] trapImage;
    //private TextMeshProUGUI[] effectImage;
    //private TextMeshProUGUI[] passiveImage;
    


    //[SerializeField]
    //// Start is called before the first frame update
    //void Start()
    //{
    //    int trap = UnityEngine.Random.Range(1, 6);
    //    int effect = UnityEngine.Random.Range(1, 8);
    //    int passive = UnityEngine.Random.Range(1, 5);

    //    trapImage = trapContainer.GetComponentsInChildren<TextMeshProUGUI>();
    //    effectImage = effectContainer.GetComponentsInChildren<TextMeshProUGUI>();
    //    passiveImage = passiveContainer.GetComponentsInChildren<TextMeshProUGUI>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    //private void GenerateBox(GameObject container, int value)
    //{
    //    GameObject box = new GameObject($"Box {value}");
    //    box.transform.parent = container.transform;
    //    box.transform.localPosition = new Vector3(1, 1, 1);
    //    box.AddComponent<TextMeshProUGUI>();
    //    box.GetComponent<TextMeshProUGUI>().enableWordWrapping = false;
    //    box.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center; 
    //}

    //private string ReturnImage(TextAsset file)
    //{
    //    string[] lines = file.text.Split('\n');
    //    string line = lines(UnityEngine.Random.Range(0, lines.Length - 1));
    //    return line.Substring(0, line.Length - 1); 
    //}

    //private IEnumerator AnimateRoll(float delay, bool isFinal, GameObject container)
    //{
    //    yield return new WaitForSecondsRealtime(delay);
    //    for (int i = 0; i <= 225, i++)
    //    {
    //        container.transform.localPosition = new Vector3(container.transform.localPosition.x, container.transform.localPosition.y - 4f);
    //        yield return new WaitForSecondsRealtime(0.005f); 

    //    }

    //    if (isFinal)
    //    {
    //        generateButton.interactable = true;

    //    }
    //}
}
