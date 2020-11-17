using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

public class TestTimeEvent : MonoBehaviour
{
    /*
    [SerializeField]
    private FloatEvent TimeChangedEvent;
    // [SerializeField]
    // private IntVariable MaxHealth;

    void Start()
    {
        TimeChangedEvent.Register(this.ChangeTime);
    }

    void OnDestroy()
    {
        TimeChangedEvent.Unregister(this.ChangeTime);
    }
    */
    public void ChangeTime(int value)
    {
        // GetComponent<Image>().fillAmount = 1.0f * health / MaxHealth.Value;
        Debug.Log("MUNG BEANS");
    }
}
