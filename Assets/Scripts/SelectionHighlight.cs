using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
public class SelectionHighlight : MonoBehaviour
{
    [SerializeField]
    public SelectedObject[] placedObjects;

    [SerializeField]
    public Button[] answerButtons = null;

    [SerializeField]
    public Button resetColorsButton = null;

    [SerializeField]
    private Color activeColor = Color.red;

    [SerializeField]
    private Color inactiveColor = Color.gray;

    [SerializeField]
    private Color[] originalColors;

    [SerializeField]
    private Color originalBaseColor;

    [SerializeField]
    private GameObject baseObject;

    // Start is called before the first frame update
    void Start()
    {
        answerButtons[0].onClick.AddListener(() => { ChangeSelectedObject(0); });
        answerButtons[1].onClick.AddListener(() => { ChangeSelectedObject(1); });
        answerButtons[2].onClick.AddListener(() => { ChangeSelectedObject(2); });
        answerButtons[3].onClick.AddListener(() => { ChangeSelectedObject(3); });
        resetColorsButton.onClick.AddListener(() => { ResetColors(); });
        SetupColors();
    }

    void ResetColors()
    {
        int i = 0;
        foreach (SelectedObject current in placedObjects)
        {
            MeshRenderer meshRenderer = current.GetComponent<MeshRenderer>();
            meshRenderer.material.color = originalColors[i];
            i++;
        }
        baseObject.GetComponent<MeshRenderer>().material.color = originalBaseColor;
    }

    void SetupColors()
    {
        int i = 0;
        foreach (SelectedObject current in placedObjects)
        {
            MeshRenderer meshRenderer = current.GetComponent<MeshRenderer>();
            originalColors[i] = meshRenderer.material.color;
            i++;
        }
        originalBaseColor = baseObject.GetComponent<MeshRenderer>().material.color;
    }

    void ChangeSelectedObject(int selectedIndex)
    {
        int i = 0;
        foreach (SelectedObject current in placedObjects)
        {
            baseObject.GetComponent<MeshRenderer>().material.color = inactiveColor;
            MeshRenderer meshRenderer = current.GetComponent<MeshRenderer>();
            if (i != selectedIndex)
            {
                //Debug.Log("Inactivated object");
                Debug.LogFormat("Inactivated obj {0}", i);
                Debug.LogFormat("Selected idx {0}", selectedIndex);
                current.Selected = false;
                //meshRenderer.material.color = originalColors
                meshRenderer.material.color = inactiveColor;
            }
            else
            {
                //Debug.Log("Activated object");
                Debug.LogFormat("Activated obj {0}", i);
                Debug.LogFormat("Selected idx {0}", selectedIndex);
                current.Selected = true;
                meshRenderer.material.color = originalColors[i];
            }
            i++;
        }
    }
}
