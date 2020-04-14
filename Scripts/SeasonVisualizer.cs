using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyoto
{
public class SeasonVisualizer : MonoBehaviour
{
    public Transform cube;
    // public Season season;
    public WeightedCycle cycle;
    private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        cube = transform.GetChild(0);
        // season = GetComponent<Season>();
        cycle = GetComponent<WeightedCycle>();
        startingPosition = cube.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(Vector3.up * season.weight);
        // cube.transform.position = startingPosition + (Vector3.up * season.weight);
        // cube.transform.position = startingPosition + (Vector3.up * season.weight);
        cube.transform.position = startingPosition + (Vector3.up * cycle.GetWeight());
    }
}
}
