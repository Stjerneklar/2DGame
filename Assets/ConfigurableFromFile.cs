using Assets.Code.Utility;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class ConfigurableFromFile<TSettings> : MonoBehaviour where TSettings : class, new()
{
    public virtual string SavePath() => Application.persistentDataPath + @"/yourfilenamehere.json";
    public TSettings settings;

    public void Awake()
    {

        if (File.Exists(SavePath()))
        {
            settings = JsonHelper.LoadExternal<TSettings>(SavePath());
        }
        else
        {
            settings = new TSettings();
            Save();
        }
    }

    public void Save() => JsonHelper.Save(settings, SavePath());
    public ConfigurableFromFile() { }
}