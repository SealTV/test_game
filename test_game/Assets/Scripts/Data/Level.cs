using System.Linq;
using System.Xml.Serialization;

namespace Assets.Scripts.Data
{
    [XmlRoot]
    public class Level
    {
        [XmlElement("I")]
        public int Rows;
        [XmlElement("J")]
        public int Columns;

        [XmlIgnore]
        public Cell[][] Cells;

        [XmlArray]
        [XmlArrayItem("Cells")]
        public Cell[] SerializedCells
        {
            get { 
                return this.Cells.SelectMany(cells => cells.ToArray()).ToArray();
            }
            set
            {
                this.Cells = new Cell[Rows][];
                for(int x = 0; x < this.Columns; x++)
                    this.Cells[x] = new Cell[Columns];

                foreach(var cell in value)
                    this.Cells[cell.I][cell.J] = cell;
            }
        }

        [XmlIgnore]
        public Wall[][] Walls;

        [XmlArray]
        [XmlArrayItem("WallType")]
        public Wall[] SerializedWalls
        {
            get
            {
                return this.Walls.SelectMany(walls => walls.ToArray()).ToArray();
            }
            set
            {
                this.Walls = new Wall[2][];

                this.Walls[0] = new Wall[Rows];
                this.Walls[1] = new Wall[Columns];

                foreach(var wall in value)
                    this.Walls[wall.I][wall.J] = wall;
            }
        }
    }
}
