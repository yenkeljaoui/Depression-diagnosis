using UnityEngine;

public class OverheadUIButtonFollower : MonoBehaviour
{
    public Transform playerHead; // XR Origin's Camera
    public Vector3 offset = new Vector3(0, 0.4f, 0.5f); // מעט מעל הראש ובקצת קדימה

    void Update()
    {
        if (playerHead == null) return;

        // מקם את הכפתור ביחס לראש
        transform.position = playerHead.position + playerHead.TransformVector(offset);

        // הכפתור תמיד יסתובב לכיוון השחקן
        transform.LookAt(playerHead);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0);
    }
}
