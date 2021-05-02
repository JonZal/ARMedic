using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorCastingToObject : MonoBehaviour
{
    //[SerializeField]
    //private GameObject welcomePanel;

    [SerializeField]
    private SelectedObject[] placedObjects;

    [SerializeField]
    private Color activeColor = Color.red;

    [SerializeField]
    private Color inactiveColor = Color.gray;

    //[SerializeField]
    //private Button dismissButton;

    [SerializeField]
    private Camera arCamera;

    private Vector2 touchPosition = default;

    [SerializeField]
    //private bool displayOverlay = false;

    void Awake()
    {
       // dismissButton.onClick.AddListener(Dismiss);
    }

    void Start()
    {
        //ChangeSelectedObject(placedObjects[0]);
    }

    //private void Dismiss() => welcomePanel.SetActive(false);

    void Update()
    {
        // do not capture events unless the welcome panel is hidden
        //if (welcomePanel.activeSelf)
        //    return;

        if (Input.touchCount > 0)
        {
            Debug.Log("Screen touched");
            Touch touch = Input.GetTouch(0);

            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touch phase began");
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    Debug.Log("Raycast began");
                    Debug.LogFormat("{0} object touched", hitObject.transform.name);
                    SelectedObject placementObject = hitObject.transform.GetComponent<SelectedObject>();
                    if (placementObject != null)
                    {
                        Debug.Log("Go into placement object");
                        ChangeSelectedObject(placementObject);
                    }
                }
            }
        }
    }

    void ChangeSelectedObject(SelectedObject selected)
    {
        foreach (SelectedObject current in placedObjects)
        {
            MeshRenderer meshRenderer = current.GetComponent<MeshRenderer>();
            if (selected != current)
            {
                Debug.Log("Inactivated object");
                current.Selected = false;
                meshRenderer.material.color = inactiveColor;
            }
            else
            {
                Debug.Log("Activated object");
                current.Selected = true;
                meshRenderer.material.color = activeColor;
            }

            //if (displayOverlay)
                //current.ToggleOverlay();
        }
    }
}
