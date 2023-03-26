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
        Item[,] items;
        public Item this[int row, int column]
        {
            get => items[row, column];
            set => items[row, column] = value;
        }

        public Field()
        {
            items = new Item[3, 3];
        }

        public Field(string field)
        {
            
            if (field.Length != 9
                || !field.All(x => Enum.IsDefined(typeof(Item), (int)x)))
            {
                throw new ArgumentException("Incorrect field");
            }

            items = new Item[3, 3];
            for (int i = 0; i < 9; i++)
            {
                var row = i / 3;
                var column = i % 3;
                items[row, column] = (Item)field[i];
            }
        }

        public Item GetWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                var threeInRow = true;
                var threeInColumn = true;
                var itemRow = items[i, 0];
                var itemColumn = items[0, i];

                if (itemRow == Item.Empty && itemColumn == Item.Empty)
                    continue;

                for (int j = 1; j < 3; j++)
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
    }
}