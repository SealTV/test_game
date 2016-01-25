using System.Xml.Serialization;

namespace Assets.Scripts.Enums
{
    public enum CellType
    {
        [XmlEnum("Default")]
        Default,
        [XmlEnum("Target")]
        Target,
        [XmlEnum("Start")]
        Start,
        [XmlEnum("Finish")]
        Finish,
    }
}
