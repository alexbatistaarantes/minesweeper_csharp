namespace Minesweeper;

class Game
{

    public enum GameStatus { Started, Won, Lost }

    public Field? Field { get; protected set; }
    public Field.Sizes Size { get; protected set; }
    public GameStatus Status { get; set; } = GameStatus.Started;

    public event Action StatusUpdate;

    public Game(Field.Sizes size)
    {
        this.StartGame();
        Size = size;
    }

    public void StartGame()
    {
        this.Field = Field.NewField(this.Size);
    }

    public void Cave(int width, int height)
    {
        if (this.Field.Cave(new HeightWidth(height, width)).HasBomb)
        {
            this.Status = GameStatus.Lost;
            this.StatusUpdate?.Invoke();
        }
        else if (this.Field.BombTotal + this.Field.Caved == this.Field.Height * this.Field.Width)
        {
            this.Status = GameStatus.Won;
            this.StatusUpdate?.Invoke();
        }
    }

    public void ToggleFlag(int width, int height){
        Tile tile = this.Field.GetTile(new(height, width));
        if(!tile.Caved) tile.Flag = !tile.Flag;
    }
}