using System;
using System.Xml.Serialization;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class Cell
    {
        [XmlElement("I")]
        public int I;
        [XmlElement("J")]
        public int J;
        [XmlElement("Type")]
        public CellType Type;
    }
}
