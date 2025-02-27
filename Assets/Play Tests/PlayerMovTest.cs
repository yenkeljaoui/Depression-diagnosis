using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    // מתודה שמזיזה את השחקן בכיוון הנתון עבור משך הזמן הנתון.
    public void Move(Vector3 direction, float deltaTime)
    {transform.Translate(direction * speed * deltaTime, Space.World); }
}
   




public class PlayerMovementTests
{
    private GameObject player;
    private PlayerMovement playerMovement;

    [SetUp]
    public void Setup()
    {
        // יצירת אובייקט השחקן והוספת הסקריפט
        player = new GameObject("Player");
        player.transform.position = Vector3.zero;
        playerMovement = player.AddComponent<PlayerMovement>();
        playerMovement.speed = 5f;
    }

    [UnityTest]
    public IEnumerator PlayerMovesForward()
    {
        Vector3 initialPosition = player.transform.position;
        // סימולציה של תנועה קדימה (Vector3.forward הוא (0, 0, 1))
        playerMovement.Move(Vector3.forward, 1f); // סימולציה של תנועה למשך שנייה אחת
        yield return null; // המתנה לפריים כדי לעדכן את המיקום
        Assert.Greater(player.transform.position.z, initialPosition.z, "השחקן צריך לזוז קדימה.");
    }

    [UnityTest]
    public IEnumerator PlayerMovesRight()
    {
        Vector3 initialPosition = player.transform.position;
        // סימולציה של תנועה ימינה (Vector3.right הוא (1, 0, 0))
        playerMovement.Move(Vector3.right, 1f);
        yield return null;
        Assert.Greater(player.transform.position.x, initialPosition.x, "השחקן צריך לזוז ימינה.");
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(player);
    }
}
