using System.Text;

namespace TicTacToeHelper
{
    public enum Item
    {
        Empty = ' ',
        X = 'x',
        O = 'o'
    }

    public class Field
    {
        int size = 3;
        Item[,] items;
        public Item this[int row, int column]
        {
            get => items[row, column];
            set => items[row, column] = value;
        }

        public Field()
        {
            items = new Item[size, size];
        }

        public Field(string field)
        {
            
            if (field.Length != size * size
                || !field.All(x => Enum.IsDefined(typeof(Item), (int)x)))
            {
                throw new ArgumentException("Incorrect field");
            }

            items = new Item[size, size];
            for (int i = 0; i < size * size; i++)
            {
                var row = i / size;
                var column = i % size;
                items[row, column] = (Item)field[i];
            }
        }

        public Item GetWinner()
        {
            for (int i = 0; i < size; i++)
            {
                var threeInRow = true;
                var threeInColumn = true;
                var itemRow = items[i, 0];
                var itemColumn = items[0, i];

                if (itemRow == Item.Empty && itemColumn == Item.Empty)
                    continue;

                for (int j = 1; j < size; j++)
                {
                    if (itemRow != items[i, j])
                    {
                        threeInRow = false;
                    }
                    if (itemColumn != items[j, i])
                    {
                        threeInColumn = false;
                    }
                }
                if (threeInRow) return itemRow;
                if (threeInColumn) return itemColumn;
            }

            if (items[0, 0] == items[1, 1] && items[1, 1] == items[2, 2])
                return items[1, 1];
            if (items[0, 2] == items[1, 1] && items[1, 1] == items[2, 0])
                return items[1, 1];

            return Item.Empty;
        }

        public Field Move(int row, int column, Item item)
        {
            if (items[row, column] != Item.Empty)
                throw new InvalidOperationException("This cell is not empty");
            var result = new Field();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = items[i, j];
                }
            }
            result[row, column] = item;
            return result;
        }

        public IEnumerable<Field> GetAllMoves(Item item)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (items[i, j] == Item.Empty)
                        yield return Move(i, j, item);
                }
            }
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result.Append((char)items[i, j]);
                }
                result.Append('\n');
            }
            return result.ToString();
        }
    }
}