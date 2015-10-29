using UnityEngine;
using System.Collections;

public class BaseUnitClass {

    //Description
    private string unitClassName;
    private string unitClassLore;
    //STATS
    private int unitClassHP;
    private int unitClassSTR;
    private int unitClassDEF;
    private int unitClassMAG;
    private int unitClassMDF;
    private int unitClassSPD;
    private int unitClassAGI;

    public string UnitClassName
    {
        get { return unitClassName; }
        set { unitClassName = value; }
    }
    public string UnitClassLore
    {
        get { return unitClassLore; }
        set { unitClassLore = value; }
    }
    public int UnitClassHP
    {
        get { return unitClassHP; }
        set { unitClassHP = value; }
    }
    public int UnitClassSTR
    {
        get { return unitClassSTR; }
        set { unitClassSTR = value; }
    }
    public int UnitClassDEF
    {
        get { return unitClassDEF; }
        set { unitClassDEF = value; }
    }
    public int UnitClassMAG
    {
        get { return unitClassMAG; }
        set { unitClassSTR = value; }
    }
    public int UnitClassMDF
    {
        get { return unitClassMDF; }
        set { unitClassDEF = value; }
    }
    public int UnitClassSPD
    {
        get { return unitClassSPD; }
        set { unitClassSPD = value; }
    }
    public int UnitClassAGI
    {
        get { return unitClassAGI; }
        set { unitClassAGI = value; }
    }

}
