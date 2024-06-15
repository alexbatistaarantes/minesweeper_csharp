using Minesweeper;

namespace Minesweeper_CSharp
{
    public partial class Form1 : Form {
        private int BUTTON_SIZE = 30;
        private int BUTTON_MARGIN = 1;
        private List<List<Button>> tilesButtons = new();
        private Game game;

        public Form1(){
            InitializeComponent();

            this.game = new(Field.Sizes.Small);
            this.CreateField(Field.measures[Field.Sizes.Small].Width, Field.measures[Field.Sizes.Small].Height);
        }

        private void UpdateField(){
            for (int h = 0; h < this.game.Field.Height; h++)
                for (int w = 0; w < this.game.Field.Width; w++)
                {
                    Tile tile = this.game.Field.GetTile(new(h, w));
                    Button tileButton = this.tilesButtons[h][w];

                    if (!tile.Caved) continue;

                    tileButton.Enabled = false;

                    if (tile.HasBomb) tileButton.Text = "B";
                    else tileButton.Text = this.game.Field.BombQuantifiers[h, w].ToString();
                }
        }

        private void HandleCave(int width, int height){
            this.game.Cave(width, height);
            this.UpdateField();
        }

        public void CreateField(int width, int height){
            if (tilesButtons.Count > 0) this.EraseField();

            for (int h = 0; h < height; h++)
            {
                this.tilesButtons.Add(new());
                for (int w = 0; w < width; w++)
                {
                    Button tile = new()
                    {
                        Name = $"btn_{h}_{w}",
                        Text = "",
                        Location = new Point((this.BUTTON_SIZE + this.BUTTON_MARGIN) * w, (this.BUTTON_SIZE + this.BUTTON_MARGIN) * h),
                        Size = new Size(this.BUTTON_SIZE, this.BUTTON_SIZE)
                    };
                    int tempWidth = w;
                    int tempHeight = h;
                    tile.Click += (_, _) => { this.HandleCave(tempWidth, tempHeight); };

                    this.Controls.Add(tile);
                    tilesButtons[h].Add(tile);
                }
            }
        }

        private void EraseField(){
            foreach (List<Button> list in this.tilesButtons)
                foreach (Button button in list)
                    this.Controls.Remove(button);
            this.tilesButtons = new();
        }
    }
}
