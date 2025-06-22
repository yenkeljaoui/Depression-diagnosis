using UnityEngine;

public class OverheadFinishButton : MonoBehaviour
{
    public PlayerDataManager playerDataManager;

    public void OnFinishClicked()
    {
        // Save data if manager exists
        if (playerDataManager != null)
        {
            playerDataManager.SaveData();
        }

        // Quit application
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
