using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyoto
{
  public class Season : MonoBehaviour
  {
    public float dateMark;
    public float weight;
    private Season priorSeason, nextSeason;
    private float priorDate, nextDate;

    private TimeController timeController;

    public AnimationCurve curve;
    // public float curveValue;

    // // this format sets the SeasonController the first time it gets asked for
    // public SeasonController SeasonController
    // {
    //   get
    //   {
    //     if (_seasonController == null)
    //     {
    //       _seasonController = transform.parent.GetComponent<SeasonController>();
    //       if (_seasonController == null)
    //       {
    //         Debug.LogError("Seasons must be a child of a SeasonController to  operate.");
    //         return null;
    //       }
    //     }
    //
    //     return _seasonController;
    //   }
    // }
    //
    // SeasonController _seasonController;

    void Start()
    {
        timeController = TimeController.Instance;
        InitSiblings();
    }

    private void InitSiblings()
    {
        int siblingCount = transform.parent.childCount;
        priorSeason = transform.parent.GetChild((transform.GetSiblingIndex()+siblingCount-1) % siblingCount).GetComponent<Season>();
        nextSeason = transform.parent.GetChild((transform.GetSiblingIndex()+1) % siblingCount).GetComponent<Season>();
        priorDate = priorSeason.dateMark < dateMark ? priorSeason.dateMark : priorSeason.dateMark - 1;
        nextDate = nextSeason.dateMark > dateMark ? nextSeason.dateMark : nextSeason.dateMark + 1;

        // Animation Curve
        curve.preWrapMode = WrapMode.Loop;
        curve.postWrapMode = WrapMode.Loop;

        // create key for this season
        curve.AddKey(dateMark, 1f);

        if (priorDate > 0)
        {
            curve.AddKey(priorDate, 0f);
            // curve.AddKey(0f, 0f);
        } else {
            curve.AddKey(1+priorDate, 0f);
            curve.AddKey(priorDate, 0f);
        }

        if (nextDate < 1)
        {
            curve.AddKey(nextDate, 0f);
            // curve.AddKey(1f, 0f);
        } else {
            curve.AddKey(nextDate-1, 0f);
            curve.AddKey(nextDate, 0f);
        }

        if (priorDate > 0 && nextDate < 1)
        {
            curve.AddKey(0f, 0f);
            curve.AddKey(1f, 0f);
        }

        // go through all keys and flatten
        // because Unity keys is by-value, we need to copy and replace
        Keyframe[] tempKeys = curve.keys;
        for (int i = 0; i < tempKeys.Length; i++)
        {
            tempKeys[i].inTangent = 0f;
            tempKeys[i].outTangent = 0f;
        }
        curve.keys = tempKeys;
    }

    void Update ()
    {
        SetGameDate(timeController.gameDateNormalized);
    }

    public void SetGameDate (float inDate)
    {
        // curveValue = curve.Evaluate(inDate);
        weight = curve.Evaluate(inDate);

        // // Version 1
        // if ((inDate > priorSeason.dateMark && inDate < dateMark) || (inDate > priorSeason.dateMark && inDate > dateMark && priorSeason.dateMark > dateMark) || (inDate < dateMark && priorSeason.dateMark > dateMark))
        // {
        //     // calculate against the previous
        //     float cycleModifier = (priorSeason.dateMark>dateMark)?1f:0f;
        //     weight = (inDate-priorSeason.dateMark-cycleModifier)/(dateMark-priorSeason.dateMark-cycleModifier);
        // }


        // // Version 2
        // if ((inDate < dateMark && (inDate > priorSeason.dateMark || priorSeason.dateMark > dateMark)) || (inDate > dateMark && dateMark < nextSeason.dateMark))
        // {
        //     // calculate against the previous
        //     float cycleModifier = (priorSeason.dateMark>dateMark)?1f:0f;
        //     weight = (inDate-priorSeason.dateMark-cycleModifier)/(dateMark-priorSeason.dateMark-cycleModifier);
        // }
        // else if ((inDate > dateMark && (inDate < nextSeason.dateMark || nextSeason.dateMark < dateMark)) || (inDate < dateMark && dateMark < priorSeason.dateMark))
        // {
        //     // calculate against the next
        //     weight = 1 - Mathf.Abs((inDate-dateMark)/(nextSeason.dateMark-dateMark));
        // }
        // else
        // {
        //     weight = 0f;
        // }

        // // Version 3
        // if (isFirst)
        // {
        //     if (inDate > dateMark && inDate < nextSeason.dateMark)
        //     {
        //         // weight against next
        //         weight = (inDate-dateMark)/(nextSeason.dateMark-dateMark);
        //     }
        //     else if ((inDate < dateMark && inDate > 0f) || (inDate > priorSeason.dateMark && inDate < 1f))
        //     {
        //         // weight against prior
        //         // this is broken
        //         weight = (inDate-priorSeason.dateMark-1)/(dateMark-priorSeason.dateMark-1);
        //     }
        //     else
        //     {
        //         weight = 0f;
        //     }
        // }
        // else if (!isFirst && !isLast)
        // {
        //     if (inDate > dateMark && inDate < nextSeason.dateMark)
        //     {
        //         // weight against next
        //         weight = (inDate-dateMark)/(nextSeason.dateMark-dateMark);
        //     }
        //     else if (inDate < dateMark && inDate > priorSeason.dateMark)
        //     {
        //         // weight against prior
        //         weight = (inDate-priorSeason.dateMark)/(dateMark-priorSeason.dateMark);
        //     }
        //     else
        //     {
        //         weight = 0f;
        //     }
        // }
        // else if (isLast)
        // {
        //     if ((inDate > dateMark && inDate < 1f) || (inDate > 0f && inDate < nextSeason.dateMark))
        //     {
        //         // weight against next
        //         weight = 1-((inDate-dateMark)/(nextSeason.dateMark+1-dateMark));
        //     }
        //     else if (inDate < dateMark && inDate > priorSeason.dateMark)
        //     {
        //         // weight against prior
        //         weight = (inDate-priorSeason.dateMark)/(dateMark-priorSeason.dateMark);
        //     }
        //     else
        //     {
        //         weight = 0f;
        //     }
        // }
        // else
        // {
        //     Debug.Log("We should never reach here");
        // }

        // // Version 4
        // if ((priorDate <= 0f)&&(inDate > priorDate+1))
        // {
        //     // We've gone past the last season, need to cycle back to start
        //     // weight = Mathf.SmoothStep(priorDate+1, 1f, GetPercentageInRange(priorDate+1, 1f, inDate));
        //     weight = Mathf.SmoothStep(0, 1f, GetPercentageInRange(priorDate+1, 1f, inDate));
        // }
        // if ((nextDate >= 1f)&&(inDate < nextDate-1))
        // {
        //     // we're before the first season
        //     weight = Mathf.SmoothStep(0f, 1f, 1-GetPercentageInRange(0f, nextDate-1, inDate));
        // }
        //
        //
        // if (inDate > priorDate && inDate <= nextDate)
        // {
        //     // float tempWeight = (inDate-dateMark)/(nextDate-dateMark);
        //     // weight = inDate < dateMark ? tempWeight : 1 - tempWeight;
        //     if (inDate < dateMark)
        //     {
        //         weight = Mathf.SmoothStep(0f, 1f, GetPercentageInRange(priorDate, dateMark, inDate));
        //     }
        //     else if (inDate >= dateMark)
        //     {
        //         weight = Mathf.SmoothStep(0f, 1f, 1-GetPercentageInRange(dateMark, nextDate, inDate));
        //     }
        //     else
        //     {
        //         Debug.Log("How the fuck did we get here?");
        //     }
        // }
    }

    private float GetPercentageInRange(float inMin, float inMax, float inPosition)
    {
        float percent = (inPosition - inMin) / (inMax - inMin);
        // (0.15 - (-0.67))/(0.2 - (-0.67))
        return percent;
    }
  }
}
