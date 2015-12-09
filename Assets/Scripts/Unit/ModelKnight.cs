using UnityEngine;
using System.Collections;

public class ModelKnight: ModelUnit
{

    public ModelKnight()
    {
        UnitClassName = "Knight";
        UnitClassType = "Knight";
        UnitClassLore = "Rook";
        UnitClassHP = 8;
        UnitClassSTR = 5;
        UnitClassDEF = 4;
        UnitClassMAG = 0;
        UnitClassMDF = 1;
        UnitClassSPD = 2;
        UnitClassAGI = 1;
    }
}