using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic.Main_Classes
{
    public class Skill
    {
        public ushort ID;//0
        public SpellType Type;//1
        public string Name;//2
        public byte Offensive;//3 - PK Flashing
        public byte Ground;//4
        public byte MultiTarget; //5
        public byte Level;//7
        public ushort Mana;//8
        public int BaseDamage;//9
        public byte Stamina;//27
        public byte Range; //13
        public byte Accuracy; //11
    }
}
