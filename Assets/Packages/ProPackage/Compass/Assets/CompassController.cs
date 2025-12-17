using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CompassController : MonoBehaviour
{
    [Header("References")]
    public RawImage compassBarImage;
    public Transform playerTransform;
    public RectTransform markerContainer;
    public GameObject markerPrefab;

    [Header("Settings")]
    public float maxDistance = 300f; // Hide markers if too far

    // Internal list to track active markers
    private List<QuestMarker> activeMarkers = new List<QuestMarker>();

    // Struct to hold marker data
    public class QuestMarker
    {
        public GameObject markerUIObject;
        public Transform worldTarget;
        public Image iconImage;
    }

    void Update()
    {
        if (playerTransform == null) return;

        UpdateCompassBar();
        UpdateQuestMarkers();
    }

    // 1. SCROLL THE BACKGROUND BAR
    void UpdateCompassBar()
    {
        // Get player Y rotation (0-360)
        float playerAngle = playerTransform.eulerAngles.y;

        // Convert angle to UV coordinate (0 to 1)
        // If texture shows 360 degrees, 1 unit = 360 degrees.
        float uvX = playerAngle / 360f;

        // Apply to RawImage UV Rect
        Rect currentRect = compassBarImage.uvRect;
        currentRect.x = uvX;
        compassBarImage.uvRect = currentRect;
    }

    // 2. POSITION THE MARKERS
    void UpdateQuestMarkers()
    {
        foreach (var marker in activeMarkers)
        {
            if (marker.worldTarget == null) continue;

            // Calculate direction and angle
            Vector3 directionToTarget = marker.worldTarget.position - playerTransform.position;

            // Calculate signed angle (left is negative, right is positive)
            float angle = Vector3.SignedAngle(playerTransform.forward, directionToTarget, Vector3.up);

            // Calculate simple distance for fading/scaling (optional)
            float distance = directionToTarget.magnitude;

            // Ideally, we clamp the marker so it stays inside the compass width
            // Standard compass width is often 180 degrees visible? 
            // Let's assume the bar width represents 180 degrees of vision.
            float compassWidth = markerContainer.rect.width;
            float visibleAngle = 180f;

            // Map angle (-180 to 180) to position
            // If angle is 0, x is 0. If angle is 90, x should be half width (if 180 FOV).
            float xPosition = (angle / visibleAngle) * compassWidth;

            // Update UI position
            RectTransform markerRect = marker.markerUIObject.GetComponent<RectTransform>();
            markerRect.anchoredPosition = new Vector2(xPosition, 0);

            // Optional: Hide if behind player or too far
            bool isVisible = Mathf.Abs(angle) < (visibleAngle / 2) && distance < maxDistance;
            marker.markerUIObject.SetActive(isVisible);
        }
    }

    // Call this from other scripts to add a quest
    public void AddQuestMarker(Transform target, Sprite icon)
    {
        GameObject newMarkerObj = Instantiate(markerPrefab, markerContainer);
        var iconComponent = newMarkerObj.GetComponent<Image>();
        iconComponent.sprite = icon;

        QuestMarker newMarker = new QuestMarker
        {
            markerUIObject = newMarkerObj,
            worldTarget = target,
            iconImage = iconComponent
        };

        activeMarkers.Add(newMarker);
    }
}