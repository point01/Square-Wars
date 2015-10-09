using UnityEngine;
using System.Collections;

public class SoldierUnit : BaseUnit
{
    public SoldierUnit()
    {
        UnitClassName = "Soldier";
        UnitClassLore = "Basic af";
        UnitClassHP = 5;
        UnitClassSTR = 5;
        UnitClassDEF = 3;
        UnitClassSPD = 3;
        UnitClassAGI = 3;
    }
}