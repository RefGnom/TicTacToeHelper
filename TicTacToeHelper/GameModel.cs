namespace TicTacToeHelper
{
    public class GameModel
    {
        int size;
        public readonly Field CurrentField;
        public Item Player { get; set; }

        public GameModel(int size)
        {
            this.size = size;
            CurrentField = new Field(size);
        }

        public Field? GetWinningField() => BreadthSearch.GetWinningVariant(CurrentField, Player);
    }
}
