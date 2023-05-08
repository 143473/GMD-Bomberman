using System;
using System.Collections;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class LerpColorCurse : MonoBehaviour
{
    public Color colourStart;
    public  Color colourEnd;
    public Material materialToChange;

    public float rate= 2; // Number of times per second new colour is chosen
    public float i = 0; //
    void Start()
    {
        materialToChange = gameObject.GetComponent<Renderer>().material;

         colourStart = new Color(Random.value, Random.value, Random.value);
         colourEnd = new Color(Random.value, Random.value, Random.value);
    }

    private void Update()
    {    // Blend towards the current target colour
        if (gameObject.GetComponentInParent<FinalBombermanStats>().GetBooleanStat(Stats.Cursed))
        {
            i += Time.deltaTime*rate;
            materialToChange.color = Color.Lerp (colourStart, colourEnd, i);
          
            // If we've got to the current target colour, choose a new one
            if(i >= 1) {
                i = 0;
                colourStart = materialToChange.color;
                colourEnd = new Color(Random.value, Random.value, Random.value);
            }
        }

    }
}