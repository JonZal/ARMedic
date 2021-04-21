using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace CandyCoded.ARFoundationComponents
{

    [Serializable]
    public class GameObjectPlacedEvent : UnityEvent<GameObject>
    {

    }

    [RequireComponent(typeof(ARSessionOrigin))]
    public class ARPlaceObjectOnPlane : MonoBehaviour
    {

#pragma warning disable CS0649
        public GameObject objectToPlace;

        public bool placeMultiple;

        public float verticalOffset = 0.01f;
#pragma warning restore CS0649

        public GameObjectPlacedEvent GameObjectPlaced;

        public Camera mainCamera { get; private set; }

        private bool _objectToPlaceActiveState;

        private void Awake()
        {

            mainCamera = gameObject.GetComponent<ARSessionOrigin>().camera;

        }

        public void PlaceObjectOnPlane(Pose pose, ARPlane plane)
        {

            var objectToPlaceGameObject = objectToPlace;

            if (placeMultiple)
            {

                objectToPlaceGameObject = Instantiate(objectToPlace);

            }

            objectToPlaceGameObject.SetActive(true);

            objectToPlaceGameObject.transform.position =
                pose.position + pose.up.normalized * verticalOffset;

            objectToPlaceGameObject.transform.rotation = pose.rotation;

            if (plane.alignment.Equals(PlaneAlignment.None) || plane.alignment.Equals(PlaneAlignment.NotAxisAligned))
            {
                return;
            }

            var cameraPosition = mainCamera.transform.position;

            objectToPlaceGameObject.transform.LookAt(new Vector3(
                cameraPosition.x,
                objectToPlaceGameObject.transform.position.y,
                cameraPosition.z
            ));

            GameObjectPlaced?.Invoke(objectToPlaceGameObject);

        }

        private void OnEnable()
        {

            if (!objectToPlace || !objectToPlace.scene.IsValid() || objectToPlace.name == "EmptyObject")
            {
                Debug.Log("didn't invoke object to place");
                if (GameObject.Find("Pilnas_Modelis_Nr_1_Iliostoma") != null)
                {
                    objectToPlace = GameObject.Find("Pilnas_Modelis_Nr_1_Iliostoma");
                    Debug.Log("Found S3 object");
                }
                else
                {
                    Invoke("OnEnable", 5);
                    return;
                }
            }
            Debug.Log("ARPlaceObject do be workin");
            _objectToPlaceActiveState = objectToPlace.activeSelf;

            objectToPlace.SetActive(false);

        }

        private void OnDisable()
        {

            if (objectToPlace && objectToPlace.scene.IsValid())
            {

                objectToPlace.SetActive(_objectToPlaceActiveState);

            }

        }

    }

}
