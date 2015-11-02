using UnityEngine;
using System.Collections;

public class ModelSoldier : ModelUnit{

    public ModelSoldier()
    {
        UnitClassName = "Soldier";
        UnitClassType = "Soldier";
        UnitClassLore = "Pawn";
        UnitClassHP = 5;
        UnitClassSTR = 5;
        UnitClassDEF = 3;
        UnitClassMAG = 0;
        UnitClassMDF = 2;
        UnitClassSPD = 3;
        UnitClassAGI = 3;

    }
}
