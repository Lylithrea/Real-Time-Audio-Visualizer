using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_AreaGenerator : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField]private int row = 5;
    [SerializeField]private int col = 5;
    [SerializeField] private int distance = 1;
    [SerializeField] private bool overWriteFrequencyBands = false;

    public void GenerateArea()
    {
        if (objectToSpawn != null)
        {
            GameObject parent = new GameObject();

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    GameObject newObj = Instantiate(objectToSpawn, parent.transform);
                    newObj.transform.position = new Vector3(i * distance, 0, j * distance);

                    if (overWriteFrequencyBands)
                    {
                        setupFrequency(newObj, i, j);
                    }

                }
            }
        }
    }

    private void setupFrequency(GameObject newObj, int currentRow, int currentCol)
    {
        if (newObj.GetComponent<FB_Size>() == null)
        {
            newObj.AddComponent<FB_Size>();
        }

        FB_Size frequencyScript = newObj.GetComponent<FB_Size>();

        float bands = row;
        float start = currentRow;
        if (col > row)
        {
            bands = col;
            start = currentCol;
        }

        float steps = (Tooling.Base.bands - 1) / bands;

        frequencyScript.minBand = Mathf.FloorToInt(start * steps);
        frequencyScript.maxBand = Mathf.FloorToInt(start * steps + steps);

    }


}

