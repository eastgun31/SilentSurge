using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    int value { get; set; }
    void UseItem();
    void ItemCharge();
}
