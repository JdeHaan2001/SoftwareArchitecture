using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
    private WaveManager waveManager;

    [UnitySetUp]
    public IEnumerator SetupTests()
    {
        yield return new WaitForSeconds(0.5f);

        waveManager = GameObject.FindObjectOfType<WaveManager>();
    }

    [UnityTest]
    public IEnumerator Did_Wave_Start()
    {
        yield return null;
    }
}
