using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Data
{
    public class WaveRouteSearch
    {
        private Cell[][] map;
        private int rows;
        private int columns;

        public WaveRouteSearch(Cell[][] cellsMap, int rows, int columns)
        {
            map = cellsMap;
            this.rows = rows;
            this.columns = columns;
        }

        public Cell[] SearchRoute(Cell start, Cell target)
        {
            if(map[target.I][target.J] == null)
                return null;

            int[][] routeMap = new int[rows][];
            for(int i = 0; i < rows; i++)
            {
                routeMap[i] = new int[columns];
                for(int j = 0; j < columns; j++)
                    routeMap[i][j] = map[i][j] != null ? -1 : -1000;
            }

            routeMap[start.I][start.J] = 0;
            int value = 0;
            bool isRun = true;
            do
            {
                var actualCells = GetCellsWithValue(value, routeMap);
                if(actualCells.Count() == 0)
                {
                    isRun = false;
                    continue;
                }

                foreach(var cell in actualCells)
                {
                    var cellsToMark = GetNearestPositios(cell.I, cell.J, routeMap, -1);

                    foreach(var cellToMark in cellsToMark)
                        routeMap[cellToMark.I][cellToMark.J] = value + 1;
                }
                value++;
                if(routeMap[target.I][target.J] != -1)
                    isRun = false;
            } while(isRun);

            if(routeMap[target.I][target.J] == -1)
                return null;

            List<Cell> result = new List<Cell>();
            result.Add(map[target.I][target.J]);

            Cell currentCell = map[target.I][target.J];
                int targetValue = routeMap[currentCell.I][currentCell.J] - 1;
            isRun = true;
            do
            {
                var cells = GetNearestPositios(currentCell.I, currentCell.J, routeMap, targetValue);

                currentCell = cells[0];
                targetValue = routeMap[currentCell.I][currentCell.J] - 1;
                result.Add(currentCell);

            } while(targetValue > 0);

            result.Reverse();
            return result.ToArray();
        }

        private Cell[] GetCellsWithValue(int value, int[][] routeMap)
        {
            List<Cell> result = new List<Cell>();
            for(int i = 0; i < rows; i++)
                for(int j = 0; j < columns; j++)
                    if(map[i][j] != null && routeMap[i][j] == value) result.Add(map[i][j]);

            return result.ToArray();
        }

        private Cell[] GetNearestPositios(int i, int j, int[][] routeMap, int targetValue)
        {
            List<Cell> result = new List<Cell>();
            if(i - 1 >= 0 && routeMap[i - 1][j] == targetValue)
                result.Add(map[i - 1][j]);

            if(j + 1 < columns && routeMap[i][j + 1] == targetValue)
                result.Add(map[i][j + 1]);

            if(i + 1 < rows && routeMap[i + 1][j] == targetValue)
                result.Add(map[i + 1][j]);

            if(j - 1 >= 0 && routeMap[i][j - 1] == targetValue)
                result.Add(map[i][j - 1]);

            return result.ToArray();
        }
    }
}
