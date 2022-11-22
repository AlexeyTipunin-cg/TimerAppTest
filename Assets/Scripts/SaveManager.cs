using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    private const string DATA = "Timers";

    public static void Save(List<DateTime> data)
    {
        var jsonString = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString(DATA, jsonString);
    }

    public static List<DateTime> GetData()
    {
        var jsonString = PlayerPrefs.GetString(DATA, "[]");
        var output = JsonConvert.DeserializeObject<List<DateTime>>(jsonString);
        return output;
    }
}

