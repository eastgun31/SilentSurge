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
}




