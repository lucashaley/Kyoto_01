using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

namespace Kyoto
{
  public class TimeController : Singleton<TimeController>
  {
    [Header("Date and Time")]
    [SerializeField] private float timeScale = 1;
    private float tickMultiplier = 100f;
    [SerializeField] private string startTime;
    public DateTime startDateTime;
    public bool overrideDate;
    public String overriddenDate;

    // Not sure we need this yet
    // private float previousGameDateTimeNormalizedDelta = 0f;
    // public float gameDateTimeNormalizedDelta = 0f;

    [Header("Real")]
    [SerializeField] private string realDate;
    [SerializeField] private string realTime;


    [Header("Game")]
    private DateTime gameDateTime;
    [SerializeField] private string gameDate;
    [SerializeField] private string gameTime;
    public float gameDateNormalized;
    public float gameTimeNormalized;

    private float daySeconds;

    private GameController gameController;

    // protected override void OnRegistration ()
    // {
    //     UpdateGameDateTime();
    //     UpdateGameDateNormalized();
    // }

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.Instance;

        startTime = System.DateTime.Now.ToString("o");
        startDateTime = System.DateTime.Now;

        if (overrideDate)
        {

        }

        UpdateGameDateTime();
        UpdateGameDateNormalized();

      // Not sure we need this yet
      // previousGameDateTimeNormalizedDelta = GetGameDateNormalized();

      TimeSpan oneDay = new TimeSpan(1,0,0,0);
      daySeconds = (float)oneDay.TotalSeconds;
    }

    // Update is called once per frame
    void Update()
    {
      UpdateGameDateTime();
      UpdateGameDateNormalized();
      UpdateGameTimeNormalized();

      gameController.UpdateGameDateNormalizedText(gameDateNormalized);
    }

    private void UpdateGameDateTime()
    {
      DateTime now = System.DateTime.Now;
      realDate = System.DateTime.Now.ToString("MM/dd/yyyy");
      realTime = System.DateTime.Now.ToString("HH:mm:ss");

      System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
      // DateTime startDateTime = System.DateTime.ParseExact(startTime, "o", provider);
      TimeSpan difference = now - startDateTime;

      TimeSpan differenceScaled = TimeSpan.FromTicks((long)(difference.Ticks * timeScale * tickMultiplier));
      gameDateTime = now + differenceScaled;
      gameDate = gameDateTime.ToString("MM/dd/yyyy");
      gameTime = gameDateTime.ToString("HH:mm:ss");
    }

    public void UpdateGameDateNormalized()
    {
      int numberOfDaysThisYear = DateTime.IsLeapYear(gameDateTime.Year)?366:365;
      float normalized = (float)gameDateTime.DayOfYear/numberOfDaysThisYear;

      // Not sure we need this yet
      // gameDateTimeNormalizedDelta = gameDateTimeNormalized - previousGameDateTimeNormalizedDelta;

      gameDateNormalized = normalized;
    }

    public void UpdateGameTimeNormalized()
    {
        TimeSpan nowSpan = new TimeSpan(gameDateTime.Hour, gameDateTime.Minute, gameDateTime.Second);
        float nowSeconds = (float)nowSpan.TotalSeconds;
        gameTimeNormalized = nowSeconds/daySeconds;
    }
  }
}
