using UnityEngine;
using System.Collections;

public class BaseMageClass : BaseUnitClass
{

    public BaseMageClass()
    {
        UnitClassName = "Mage";
        UnitClassType = "Mage";
        UnitClassLore = "Wizardry";
        UnitClassHP = 5;
        UnitClassSTR = 0;
        UnitClassDEF = 2;
        UnitClassMAG = 5;
        UnitClassMDF = 3;
        UnitClassSPD = 3;
        UnitClassAGI = 3;
    }
}