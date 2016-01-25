using System;
using System.Xml.Serialization;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class Wall
    {
        [XmlElement("I")]
        public int I;

        [XmlElement("J")]
        public int J;

        [XmlElement("Type")]
        public WallType Type;

        [XmlElement("FireSpped")]
        public float FireSpeed = 1;

        [XmlElement("BollSpeed")]
        public float BollSpeed = 0.5f;
    }
}
