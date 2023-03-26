using System.IO;
using System.Windows.Forms;

namespace TicTacToeHelper
{
    public class BreadthSearch
    {
        public static Field GetWinningVariant(Field currentField, Item player)
        {
            Dictionary<Field, Field> path = new Dictionary<Field, Field>();
            path[currentField] = null;
            Field result = null;
            var queue = new Queue<(Field, Item)>();
            queue.Enqueue((currentField, player));
            while (queue.Count != 0)
            {
                var (field, active) = queue.Dequeue();
                var nextFields = field
                    .GetAllMoves(active)
                    .Where(g => !path.ContainsKey(g));
                foreach (var nextField in nextFields)
                {
                    
                    path[nextField] = field;
                    queue.Enqueue((nextField, active == Item.X ? Item.O : Item.X));
                    if (nextField.GetWinner() == player)
                    {
                        result = nextField;
                    }
                }
                if (result != null)
                    break;
            }

            while (result != null)
            {
                if (path[result] == currentField)
                    break;
                result = path[result];
            }
            return result;
        }
    }
}
