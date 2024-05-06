using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class GuideLineDB : ScriptableObject
{
	public List<GuideLineData> guideLine; // Replace 'EntityType' to an actual type that is serializable.
}
