using System;
using System.Windows.Forms;

namespace TicTacToeHelper
{
    public partial class Form1 : Form
    {
        static int size = 3;
        GameModel gameModel = new(size);
        Label choise = new();
        Label description = new();
        Label exception = new();
        RadioButton choiseButtonX = new();
        RadioButton choiseButtonO = new();
        TableLayoutPanel currentField = new();
        TableLayoutPanel winningField = new(); 
        Button generateVariants = new();

        public Form1()
        {
            Initialize();
            Load += (sender, args) => OnSizeChanged(EventArgs.Empty);
            SizeChanged += FormLayout;
            currentField.MouseClick += (sender, args) =>
            {
                var p = args.Location;
                var column = p.X / 70;
                var row = p.Y / 70;
                var index = row * size + column;

                if (args.Button == MouseButtons.Left)
                {
                    currentField.Controls[index].Text = "X";
                    gameModel.CurrentField[row, column] = Item.X;
                }
                if (args.Button == MouseButtons.Right)
                {
                    currentField.Controls[index].Text = "O";
                    gameModel.CurrentField[row, column] = Item.O;
                }
                if (args.Button == MouseButtons.Middle)
                {
                    currentField.Controls[index].Text = "";
                    gameModel.CurrentField[row, column] = Item.Empty;
                }
            };
            choiseButtonX.Enter += (sender, args) =>
            {
                gameModel.Player = Item.X;
                exception.Text = "";
            };
            choiseButtonO.Enter += (sender, args) =>
            {
                gameModel.Player = Item.O;
                exception.Text = "";
            };
            generateVariants.MouseClick += (sender, args) =>
            {
                if (gameModel.Player == 0)
                {
                    exception.Text = "Нужно выбрать за кого вы играете!";
                    return;
                }
                
                exception.Text = "";
                var winningMove = gameModel.GetWinningField();
                if (winningMove != null)
                {
                    FillWinningField(winningMove);
                }
            };
        }

        private void FormLayout(object? sender, EventArgs e)
        {
            choise.Size = new Size(300, 30);
            choiseButtonX.Size = new Size(100, 30);
            choiseButtonO.Size = choiseButtonX.Size;
            description.Size = new Size(300, 90);
            exception.Size = new Size(300, 90);
            currentField.Size = new Size(210, 210);
            winningField.Size = new Size(210, 210);
            generateVariants.Size = new Size(250, 50);

            choise.Location = new Point(0, 3);
            choiseButtonX.Location = new Point(choise.Width + 50, 0);
            choiseButtonO.Location = new Point(choiseButtonX.Location.X + choiseButtonX.Width, 0);
            description.Location = new Point(0, choise.Height + 50);
            currentField.Location = new Point(0, description.Location.Y + description.Height + 20);
            winningField.Location = new Point(currentField.Location.X + currentField.Width + 120, currentField.Location.Y);
            generateVariants.Location = new Point(0, currentField.Location.Y + currentField.Height + 20);
            exception.Location = new Point(0, generateVariants.Location.Y + generateVariants.Height + 40);
        }

        public void Initialize()
        {
            ClientSize = new Size(1080, 640);
            DoubleBuffered = true;
            var font = new Font("Arial", 14);

            choise.Text = "Какой фигурой вы играете?";
            choiseButtonX.Text = "Крестик";
            choiseButtonO.Text = "Нолик";
            description.Text = "Лкм - поставить крестик\nПкм - поставить нолик\nСредняя кнопка мыши - очистить";
            generateVariants.Text = "Сгенерировать ход!";

            choiseButtonX.Font = font;
            choiseButtonO.Font = font;
            description.Font = font;
            exception.Font = font;
            choise.Font = font;
            generateVariants.Font = font;

            exception.ForeColor = Color.Red;

            generateVariants.FlatStyle = FlatStyle.Popup;

            InitialField(currentField);
            InitialField(winningField);

            Controls.Add(choise);
            Controls.Add(choiseButtonX);
            Controls.Add(choiseButtonO);
            Controls.Add(description);
            Controls.Add(exception);
            Controls.Add(currentField);
            Controls.Add(winningField);
            Controls.Add(generateVariants);
        }

        void InitialField(TableLayoutPanel field)
        {
            field.ColumnCount = size;
            field.RowCount = size;
            field.BackColor = Color.LightGray;
            field.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;

            for (int i = 0; i < size; i++)
            {
                field.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3f));
                field.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3f));

                for (int j = 0; j < size; j++)
                {
                    var symbol = new Label()
                    {
                        Font = new Font("Arial", 30),
                        Enabled = false,
                        Margin = new Padding(10),
                        Dock = DockStyle.Fill
                    };
                    field.Controls.Add(symbol, j, i);
                }
            }
        }

        void FillWinningField(Field field)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var index = i * 3 + j;
                    winningField.Controls[index].Text = ((char)field[i, j]).ToString().ToUpper();
                }
            }
        }
    }
}