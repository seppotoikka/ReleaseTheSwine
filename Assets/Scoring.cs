using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    Pig[] pigs;
    bool scoreGiven;
    public SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        pigs = FindObjectsOfType<Pig>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!scoreGiven)
        {
            foreach(Pig pig in pigs)
            {
                if (!(pig.PigState == Pig.State.inAir && pig.rb.IsSleeping()))
                    return;
            }
            scoreGiven = true;
            StartCoroutine(ReloadScene());
        }
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(3f);
        sceneLoader.ReloadScene();
    }
}
