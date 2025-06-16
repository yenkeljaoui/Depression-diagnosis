using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BasicPlayModeTest
{
    [UnityTest]
    public IEnumerator GameObject_ExistsAndActive()
    {
        // Arrange – יוצרים GameObject חדש
        var obj = new GameObject("TestObject");

        // Act – מחכים פריים אחד
        yield return null;

        // Assert – בודקים שהוא עדיין חי ואקטיבי
        Assert.IsNotNull(obj);
        Assert.IsTrue(obj.activeSelf);
    }
}