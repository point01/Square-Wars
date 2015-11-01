using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class FileReader
{

    public FileReader()
    {

    }

    /* Reads a file into an array of strings */
    public string[] textAssetToStrArray(string name)
    {
        TextAsset filetxt = Resources.Load(name) as TextAsset;
        string txt = filetxt.text;
        return txt.Split(new string[] { "\n" }, StringSplitOptions.None);
    }
}