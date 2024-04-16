using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemInfo
{
    public class Item
    {
        public int count { get; set; }
        public float cooltime { get; set; }
        public bool canUse { get; set; }
        public WaitForSeconds itemcool = new WaitForSeconds(3f);
        public WaitForSeconds heartseeDuration = new WaitForSeconds(5f);
        public WaitForSeconds animDelay = new WaitForSeconds(0.5f);
    }
    public class CoolTime
    {
        public WaitForSeconds cool1sec = new WaitForSeconds(1f);
        public WaitForSeconds coolhalf1sec = new WaitForSeconds(0.5f);
    }
    public class E_CoolTime
    {
        public WaitForSeconds cool1sec = new WaitForSeconds(1f);
        public WaitForSeconds cool3sec = new WaitForSeconds(3f);
        public WaitForSeconds cool5sec = new WaitForSeconds(5f);
        public WaitForSeconds cool10sec = new WaitForSeconds(10f);
    }
    public class CctvCool
    {
        public WaitForSeconds cool3sec = new WaitForSeconds(3f);
    }
    public class Enemy
    {

    }
}




