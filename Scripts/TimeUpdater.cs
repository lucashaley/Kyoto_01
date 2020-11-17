using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;
using UnityAtoms.BaseAtoms;

public class TimeUpdater : MonoBehaviour
{
    public FloatVariable timeScale;
    public FloatVariable gameDateVariable, gameTimeVariable;
    public FloatVariable gameDateNormalizedVariable;
    public FloatVariable gameTimeNormalizedVariable;
    public IntVariable dayOfYear;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChangeDayOfYear()
    {
        dayOfYear.Value = 0;
    }
}
