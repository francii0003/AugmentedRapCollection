using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace AugmentedRapCollection
{
    [RequireComponent(typeof(ARTrackedImageManager))]
    public class AlbumTrackingManager : MonoBehaviour
    {
        [Header("Resources")]
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private List<AlbumData> albumDataList;

        [Header("Tracking Settings")]
        [SerializeField] private float maxTrackingDistance = 2.5f;
        [SerializeField] private Vector3 cardLocalOffset = new Vector3(0f, 0.45f, 0.02f);

        private ARTrackedImageManager trackedImageManager;
        private readonly Dictionary<TrackableId, GameObject> spawnedCards = new Dictionary<TrackableId, GameObject>();

        private void Awake()
        {
            trackedImageManager = GetComponent<ARTrackedImageManager>();
        }

        private void OnEnable()
        {
            if (trackedImageManager != null)
                trackedImageManager.trackablesChanged.AddListener(OnTrackedImagesChanged);
        }

        private void OnDisable()
        {
            if (trackedImageManager != null)
                trackedImageManager.trackablesChanged.RemoveListener(OnTrackedImagesChanged);
        }

        private void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
        {
            foreach (var trackedImage in eventArgs.added)
                ProcessTrackedImage(trackedImage);

            foreach (var trackedImage in eventArgs.updated)
                ProcessTrackedImage(trackedImage);

            foreach (var removedImage in eventArgs.removed)
                DestroyCard(removedImage.Key);
        }

        private void ProcessTrackedImage(ARTrackedImage trackedImage)
        {
            Vector3 cameraPosition = Camera.main != null ? Camera.main.transform.position : Vector3.zero;
            float distance = Vector3.Distance(cameraPosition, trackedImage.transform.position);

            bool isTracking = trackedImage.trackingState == TrackingState.Tracking;
            bool isCloseEnough = distance <= maxTrackingDistance;

            if (!spawnedCards.ContainsKey(trackedImage.trackableId))
            {
                if (!(isTracking && isCloseEnough)) return;
                SpawnCardForImage(trackedImage);
            }

            if (spawnedCards.TryGetValue(trackedImage.trackableId, out GameObject card))
            {
                if (isTracking && isCloseEnough)
                {
                    card.SetActive(true);
                }
                else if (!isCloseEnough)
                {
                    card.SetActive(false);
                }
            }
        }

        private void SpawnCardForImage(ARTrackedImage trackedImage)
        {
            string imageName = trackedImage.referenceImage.name;
            AlbumData matchingData = albumDataList.Find(data => data.referenceImageName == imageName);

            if (matchingData == null)
            {
                Debug.LogWarning($"[AlbumTrackingManager] No AlbumData found for image: {imageName}");
                return;
            }

            GameObject newCard = Instantiate(cardPrefab, trackedImage.transform);
            newCard.transform.localPosition = cardLocalOffset;
            newCard.transform.localRotation = Quaternion.identity;

            AlbumCardUI cardUI = newCard.GetComponent<AlbumCardUI>();
            if (cardUI != null)
                cardUI.SetData(matchingData);

            spawnedCards.Add(trackedImage.trackableId, newCard);
            Debug.Log($"<color=green>[AR] Card successfully spawned for:</color> {matchingData.albumTitle}");
        }

        private void DestroyCard(TrackableId imageId)
        {
            if (spawnedCards.TryGetValue(imageId, out GameObject cardToDestroy))
            {
                Destroy(cardToDestroy);
                spawnedCards.Remove(imageId);
                Debug.Log("<color=red>[AR] Card removed.</color>");
            }
        }
    }
}