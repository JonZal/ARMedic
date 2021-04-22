using Amazon;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace UnityCustomObj
{

    [Serializable]
    public class GameObjectPlacedEvent : UnityEvent<GameObject>
    {

    }

    [RequireComponent(typeof(ARSessionOrigin))]
    [RequireComponent(typeof(ARPlaneManager))]
    public class ARCustomPlaceObject : MonoBehaviour
    {

#pragma warning disable CS0649
        public GameObject objectToPlace;

        public bool placeMultiple;

        public float verticalOffset = 0.01f;

        public string gameObjectName;

        public Button ResetObjectButton = null;
#pragma warning restore CS0649

        public GameObjectPlacedEvent GameObjectPlaced;

        public Camera mainCamera { get; private set; }

        private bool _objectToPlaceActiveState;

        private ARPlaneManager _planeManager;

        private void Awake()
        {
            mainCamera = gameObject.GetComponent<ARSessionOrigin>().camera;
            _planeManager = gameObject.GetComponent<ARPlaneManager>();
        }

        void Start()
        {
            Debug.Log("Start");
            UnityInitializer.AttachToGameObject(this.gameObject);
            ResetObjectButton.onClick.AddListener(() => { ResetObject(); });
        }

        public void ResetObject()
        {
            Debug.Log("ResetObject");
            GameObjectPlaced.RemoveAllListeners();
            _planeManager.enabled = true;
            Debug.LogFormat("Plane manager enabled: {0}", _planeManager.enabled.ToString());
        }

        public void PlaceCustomObjectOnPlane(Pose pose, ARPlane plane)
        {
            Debug.Log("TouchPlaceObject");
            Debug.LogFormat("Find object {0}", gameObjectName);
            var objectToPlaceGameObject = objectToPlace; //objectToPlace.transform.Find(gameObjectName).gameObject;

            if (placeMultiple)
            {
                objectToPlaceGameObject = Instantiate(objectToPlace);//objectToPlace.transform.Find(gameObjectName).gameObject);
            }
            //objectToPlaceGameObject.transform.Rotate(-90.0f, 0.0f, 0.0f);
            Debug.LogFormat("Object found {0}", objectToPlaceGameObject.name);

            objectToPlaceGameObject.SetActive(true);

            objectToPlaceGameObject.transform.position =
                pose.position + pose.up.normalized * verticalOffset;

            objectToPlaceGameObject.transform.rotation = pose.rotation;

            objectToPlaceGameObject.transform.Rotate(-90.0f, 0.0f, 0.0f);

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
            Debug.Log("Object placed");
            _planeManager.enabled = false;
            Debug.LogFormat("Plane manager enabled: {0}", _planeManager.enabled.ToString());
        }

        private void OnEnable()
        {
            if (!objectToPlace || !objectToPlace.scene.IsValid())
            { 
                Invoke("OnEnable", 1);
                return;
            }
            Debug.Log("ARCustomPlaceObject do be workin");
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
