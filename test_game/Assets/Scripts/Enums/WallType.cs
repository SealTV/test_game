using System.Xml.Serialization;

namespace Assets.Scripts.Enums
{
    public enum WallType
    {
        [XmlEnum("Default")]
        Default,
        [XmlEnum("Gun")]
        Gun
    }
}
