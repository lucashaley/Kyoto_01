using System;
using System.Linq;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyoto {
  public class SeasonController : MonoBehaviour
  {
    [SerializeField] private TimeController timeController;
    public Season[] seasons;
    public float[] seasonTimeMarks;

    public Season priorSeason, nextSeason;
    // public SeasonDictionary seasonStates;

    void Awake()
    {
      timeController = TimeController.Instance;
      seasons = gameObject.GetComponentsInChildren<Season>();

      // seasonTimeMarks = new float[seasons.Length + 1];
      seasonTimeMarks = new float[seasons.Length];
      for (int i=1; i<seasons.Length; i++)
      {
          seasonTimeMarks[i] = seasons[i].dateMark;
      }
      // seasonTimeMarks[seasonTimeMarks.Length-1] = seasons.First().dateMark+1f;
    }

    // Update is called once per frame
    void Update()
    {
        int nextIndex = GetStateForValue(timeController.gameDateNormalized);

        // Jesus this is a mess
        nextSeason = seasons[nextIndex % seasons.Length];
        priorSeason = seasons[(nextIndex+seasons.Length-1) % seasons.Length];

        float weight = (timeController.gameDateNormalized-priorSeason.dateMark)/(nextSeason.dateMark-priorSeason.dateMark);
        // priorSeason.SetWeight(1.0f-weight);
        // nextSeason.SetWeight(weight);

        foreach (Season season in seasons)
        {
            season.SetGameDate(timeController.gameDateNormalized);
        }
    }

    public int GetStateForValue (float gameDateNormalized)
    {
        // Jesus this is a mess
        int result = seasonTimeMarks.TakeWhile(timeMark => timeMark < gameDateNormalized).Count(); //% seasons.Length;

        return result;
    }
  }
}
