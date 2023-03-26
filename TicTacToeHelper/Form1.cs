using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace TicTacToeHelper
{
    public partial class Form1 : Form
    {
        Label choise;
        Label description;
        RadioButton choiseButtonX;
        RadioButton choiseButtonO;
        TableLayoutPanel table;
        Button generateVariants;

        public Form1()
        {
            Initialize();

            Load += (sender, args) => OnSizeChanged(EventArgs.Empty);
            SizeChanged += FormLayout;
            table.MouseClick += (sender, args) =>
            {
                var p = args.Location;
                var index = p.Y / 70 * 3 + p.X / 70;

                if (args.Button == MouseButtons.Left)
                {
                    table.Controls[index].Text = "X";
                }
                if (args.Button == MouseButtons.Right)
                {
                    table.Controls[index].Text = "O";
                }
                if (args.Button == MouseButtons.Middle)
                {
                    table.Controls[index].Text = "";
                }
            };
            choiseButtonX.Enter += (sender, args) =>
            {
                // TODO: назначать выбор в поле класса GameModel
                choise.Text = "X";
            };
            choiseButtonO.Enter += (sender, args) =>
            {
                // TODO: назначать выбор в поле класса GameModel
                choise.Text = "O";
            };
            generateVariants.MouseClick += (sender, args) =>
            {
                // TODO: запустить поиск в ширину для поиска выигрышных вариантов
                // Выдавать исключение, если выбор символа не сделан
                choise.Text = "Генерация";
            };
        }

        private void FormLayout(object? sender, EventArgs e)
        {
            choise.Size = new Size(300, 30);
            choiseButtonX.Size = new Size(100, 30);
            choiseButtonO.Size = choiseButtonX.Size;
            description.Size = new Size(300, 90);
            table.Size = new Size(210, 210);
            generateVariants.Size = new Size(310, 50);

            choise.Location = new Point(0, 3);
            choiseButtonX.Location = new Point(choise.Width, 0);
            choiseButtonO.Location = new Point(choise.Width + choiseButtonX.Width, 0);
            description.Location = new Point(0, choise.Height + 50);
            table.Location = new Point(0, description.Location.Y + description.Height);
            generateVariants.Location = new Point(0, table.Location.Y + table.Height + 20);
        }

        public void Initialize()
        {
            ClientSize = new Size(1080, 640);
            DoubleBuffered = true;
            var font = new Font("Arial", 14);

            choise = new Label()
            {
                Text = "Какой фигурой вы играете?",
                Font = font
            };
            choiseButtonX = new RadioButton()
            {
                Text = "Крестик",
                Font = font
            };
            choiseButtonO = new RadioButton()
            {
                Text = "Нолик",
                Font = font
            };
            description = new Label()
            {
                Text = "Лкм - поставить крестик\nПкм - поставить нолик\nСредняя кнопка мыши - очистить",
                Font = font
            };

            table = new TableLayoutPanel()
            {
                ColumnCount = 3,
                RowCount = 3,
                BackColor = Color.LightGray,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble
            };
            for (int i = 0; i < 3; i++)
            {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3f));
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3f));

                for (int j = 0; j < 3; j++)
                {
                    var symbol = new Label()
                    {
                        Font = new Font("Arial", 30),
                        Enabled = false,
                        Margin = new Padding(10),
                        Dock = DockStyle.Fill
                    };
                    table.Controls.Add(symbol, j, i);
                }
            }

            generateVariants = new Button()
            {
                Text = "Сгенерировать варианты ходов!",
                Font = font,
                FlatStyle = FlatStyle.Popup
            };

            Controls.Add(choise);
            Controls.Add(choiseButtonX);
            Controls.Add(choiseButtonO);
            Controls.Add(description);
            Controls.Add(table);
            Controls.Add(generateVariants);
        }
    }
}