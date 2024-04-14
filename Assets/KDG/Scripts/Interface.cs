using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    int indexNum { get; set; }
    int value { get; set; }
    void GetItem();
    void ItemCharge();
}

public interface ISound
{
    bool canhear { get; set; }
}
