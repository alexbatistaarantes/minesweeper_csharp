namespace Minesweeper;

struct HeightWidth(int height, int width){
    public int Height {get; set;} = height;
    public int Width {get; set;} = width;

    public bool Equals(HeightWidth obj) => obj.Height == this.Height && obj.Width == this.Width;
}