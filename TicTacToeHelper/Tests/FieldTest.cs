using NUnit.Framework;

namespace TicTacToeHelper.Tests;

[TestFixture]
public class FieldTest
{
    [Test]
    public void EmptyField()
    {
        var field = new Field(3);
        Assert.AreEqual(Item.Empty, field.GetWinner());
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    public void WinnerInRow(int row)
    {
        var field = new Field(3);
        for (int i = 0; i < 3; i++)
        {
            field[row, i] = Item.X;
        }
        Assert.AreEqual(Item.X, field.GetWinner());
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    public void WinnerInColumn(int column)
    {
        var field = new Field(3);
        for (int i = 0; i < 3; i++)
        {
            field[i, column] = Item.X;
        }
        Assert.AreEqual(Item.X, field.GetWinner());
    }

    [Test]
    public void WinnerInMainDiagonal()
    {
        var field = new Field(3);
        for (int i = 0; i < 3; i++)
        {
            field[i, i] = Item.X;
        }
        Assert.AreEqual(Item.X, field.GetWinner());
    }

    [Test]
    public void WinnerInNotMainDiagonal()
    {
        var field = new Field(3);
        field[0, 2] = Item.X;
        field[1, 1] = Item.X;
        field[2, 0] = Item.X;
        Assert.AreEqual(Item.X, field.GetWinner());
    }

    [TestCase("   "
            + "xxo"
            + "oxx")]
    [TestCase("xox"
            + "   "
            + "oxx")]
    [TestCase("xox"
            + "xxo"
            + "   ")]
    [TestCase("xox"
            + "xoo"
            + "oxx")]
    [TestCase("xox"
            + "oox"
            + "xxo")]
    [TestCase("xox"
            + "xoo"
            + "oxx")]
    [TestCase("xox"
            + "o o"
            + "xox")]
    public void NotWinner(string cells)
    {
        var field = new Field(3, cells);
        Assert.AreEqual(Item.Empty, field.GetWinner());
    }
}
