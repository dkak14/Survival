using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    public int Day;
    public enum Season
    {
        Spring, Summer, Autumn, Winter
    }
    public Season NowSeason;
    [SerializeField]
    int Quarter;
    [SerializeField]
    float DayTime;
    [SerializeField]
    int SearchPerDay;
    [SerializeField]
    int i = 0;
    [SerializeField]
    Text SeasonText;
    [SerializeField]
    Text DayText;
    [SerializeField]
    Text timerText;
    void Start()
    {
        Instance = this;
        StartCoroutine(timer());
    }

    void Update()
    {
        
    }
    IEnumerator timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(DayTime / SearchPerDay);
            
            i++;
            if (i >= DayTime)
            {
                Day++;                
                if (Day % Quarter == 0)
                {
                    if (NowSeason < Season.Winter)
                        NowSeason++;
                    else
                        NowSeason = 0;
                }
                i = 0;
            }
            UiSetting();
        }
    }
    void UiSetting()
    {
        DayText.text = "Day : " + Day;
        SeasonText.text = "Season : " + NowSeason;
        timerText.text = DayTime + " / " + i;
    }
}
