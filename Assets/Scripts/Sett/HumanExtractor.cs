using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HumanExtractor : MonoBehaviour
{
    public int id;
    public TextMeshProUGUI humanText;

    public void UpdateImageWithId(int id)
    {
        //GetComponent<Image>().sprite = image;
        //Debug.Log("Modifying id");
        humanText.text = id.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
