using UnityEngine;

public class BeastHelpManager : MonoBehaviour
{
    public GameObject dialogCanvas;         // The world-space canvas with Yes/No buttons
    public PlayerDataManager playerDataManager; // The object writeing in the file data

    private bool playerChoseToHelp = false;

    void Start()
    {
        dialogCanvas.SetActive(false); // Hide dialog at start
    }

   public void ShowHelpDialog()
{
    dialogCanvas.SetActive(true);
    // Time.timeScale = 0f; // dont stop the game
}


    public void OnPlayerAcceptedHelp()
    {
        Time.timeScale = 1f;
        dialogCanvas.SetActive(false);
        playerChoseToHelp = true;

        playerDataManager.LogEvent("BeastHelpChoice", "Player chose YES to help the beast");
            }

    public void OnPlayerDeclinedHelp()
    {
        Time.timeScale = 1f;
        dialogCanvas.SetActive(false);
        playerChoseToHelp = false;

        playerDataManager.LogEvent("BeastHelpChoice", "Player chose NO to help the beast");
    }

    public bool DidPlayerChooseToHelp()
{
    return playerChoseToHelp;
}


}
