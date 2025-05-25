using UnityEngine;

public class BeastHelpManager : MonoBehaviour
{
    public GameObject dialogCanvas;         // The world-space canvas with Yes/No buttons
    public PlayerDataManager playerDataManager; // The object writeing in the file data
    public GameObject task_Scenario2;


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


private System.Collections.IEnumerator Showtask_Scenario2(float duration)
{
    if (task_Scenario2 != null)
    {
        task_Scenario2.SetActive(true);
        yield return new WaitForSeconds(duration);
        
    }
}


    public void OnPlayerAcceptedHelp()
    {
        Time.timeScale = 1f;
        dialogCanvas.SetActive(false);
        playerChoseToHelp = true;

        playerDataManager.LogEvent("BeastHelpChoice", "Player chose YES to help the beast");
        StartCoroutine(Showtask_Scenario2(3f));


    }

    public void OnPlayerDeclinedHelp()
    {
        Time.timeScale = 1f;
        dialogCanvas.SetActive(false);
        playerChoseToHelp = false;

        playerDataManager.LogEvent("BeastHelpChoice", "Player chose NO to help the beast");
         //begin scenario 3
        FindFirstObjectByType<Scenario3Manager>()?.BeginScenario3();
    }

    public bool DidPlayerChooseToHelp()
{
    return playerChoseToHelp;
}


}
