using Minesweeper;

namespace Minesweeper_CSharp
{
    public partial class Form1 : Form
    {

        private static readonly int BUTTON_LENGTH = 30;
        private static readonly int BUTTON_MARGIN = 1;
        private static readonly Color CavedTileForeColor = Color.White;
        private static readonly Color CavedTileBackColor = Color.DarkGray;

        private List<List<Button>> tilesButtons = new();
        private Game game;

        public Form1()
        {
            InitializeComponent();

            this.game = new(Field.Sizes.Small);
            this.game.StatusUpdate += HandleStatusUpdate;
            this.CreateField(Field.measures[Field.Sizes.Small].Width, Field.measures[Field.Sizes.Small].Height);
        }

        private void UpdateField()
        {
            for (int h = 0; h < this.game.Field.Height; h++)
                for (int w = 0; w < this.game.Field.Width; w++)
                {
                    Tile tile = this.game.Field.GetTile(new(h, w));
                    Button tileButton = this.tilesButtons[h][w];

                    if (tile.Caved)
                    {
                        tileButton.Enabled = false;
                        tileButton.ForeColor = Form1.CavedTileForeColor;
                        tileButton.BackColor = Form1.CavedTileBackColor;

                        if (tile.HasBomb) tileButton.Text = "💣";
                        else
                        {
                            int quantifier = this.game.Field.BombQuantifiers[h, w];
                            tileButton.Text = quantifier == 0 ? " " : quantifier.ToString();
                        }
                    }
                    else tileButton.Text = tile.Flag ? "🚩" : " ";
                }
        }

        private void HandleStatusUpdate()
        {
            if (this.game.Status == Game.GameStatus.Won)
            {
                label_gameStatus.Text = "You won!";
            }
            else label_gameStatus.Text = "You lose!";
        }

        private void HandleCave(int width, int height)
        {
            this.game.Cave(width, height);
        }

        private void ToggleFlag(int width, int height) => this.game.ToggleFlag(width, height);

        private void HandleClick(int width, int height, MouseButtons mouseButton)
        {
            switch (mouseButton)
            {
                case MouseButtons.Left: this.HandleCave(width, height); break;
                case MouseButtons.Right: this.ToggleFlag(width, height); break;
                default: break;
            };
            this.UpdateField();
        }

        public void CreateField(int width, int height)
        {
            if (tilesButtons.Count > 0) this.EraseField();

            for (int h = 0; h < height; h++){
                this.tilesButtons.Add(new());

                for (int w = 0; w < width; w++){

                    (int top, int left) location;
                    location.top = (BUTTON_LENGTH * h) + this.groupField.DisplayRectangle.Top + (h > 0 ? BUTTON_MARGIN : 0);
                    location.left = (BUTTON_LENGTH * w) + this.groupField.DisplayRectangle.Left + (w > 0 ? BUTTON_MARGIN : 0);

                    // Create button
                    Button tile = new()
                    {
                        Name = $"btn_{h}_{w}",
                        Text = "",
                        Location = new Point(location.left, location.top),
                        Size = new Size(BUTTON_LENGTH, BUTTON_LENGTH)
                    };

                    // Add event to handle click
                    int tempWidth = w;
                    int tempHeight = h;
                    tile.MouseDown += (object sender, MouseEventArgs e) => { this.HandleClick(tempWidth, tempHeight, e.Button); };

                    this.groupField.Controls.Add(tile);
                    tilesButtons[h].Add(tile);
                }
            }
        }

        private void EraseField()
        {
            foreach (List<Button> list in this.tilesButtons)
                foreach (Button button in list)
                    this.Controls.Remove(button);
            this.tilesButtons = new();
        }
    }
}
