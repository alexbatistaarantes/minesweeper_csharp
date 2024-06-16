namespace Minesweeper;

class Field{
    public enum Sizes {Small, Medium, Large};
    public static readonly Dictionary<Sizes, HeightWidth> measures = new(){
        {Sizes.Small, new(10, 10)},
        {Sizes.Medium, new(15, 15)},
        {Sizes.Large, new(20, 20)}
    };
    // Attributes
    public int Height {get; protected init;}
    public int Width {get; protected init;}
    public List<List<Tile>> Tiles {get; protected set;} = [];
    public required int[,] BombQuantifiers; // separate array instead of staying in the Field class to better increase the numbers arround bombs
    public int BombChance = 15;
    public int Caved = 0;
    public int BombTotal = 0;

    public Tile Cave(HeightWidth coords){
        // If field not populated yet, creates it
        if(this.Tiles.Count == 0) this.PopulateField(coords);

        Tile tile = this.GetTile(coords);
        if (!tile.Caved){
            tile.Caved = true;
            if(!tile.HasBomb){
                this.Caved++;
                this.SpreadCave(coords);
            }
        }
        return tile;
    }

    private void SpreadCave(HeightWidth coord){
        List<HeightWidth> arroundCoords = this.GetTilesArroundCoords(coord);
        // iterate through the neighbour tiles
        foreach(HeightWidth arroundCoord in arroundCoords){
            Tile tile = this.GetTile(arroundCoord);
            // if it's already caved or has a bomb, skip it
            if(tile.Caved || tile.HasBomb) continue;
            // cave it
            tile.Caved = true;
            this.Caved++;
            // if have 0 bomb arround it, spread cave arround it
            if(this.BombQuantifiers[arroundCoord.Height, arroundCoord.Width] == 0) this.SpreadCave(arroundCoord);
        }
    }

    // Fill the fill based on the initial cell that was selected
    private void PopulateField(HeightWidth firstCavedAt){
        this.BombQuantifiers = new int[this.Height, this.Width];
        Random rand = new();

        for(int height = 0; height < this.Height; height++){
            this.Tiles.Add([]);
            for(int width = 0; width < this.Width; width++){
                HeightWidth coords = new(height, width);
                bool hasBomb = rand.Next(101) < this.BombChance;

                if(hasBomb && !coords.Equals(firstCavedAt)){
                    this.Tiles[height].Add(new(){HasBomb=true});
                    this.IncreaseBombQuantifierArround(coords);
                    this.BombTotal++;
                }else
                    this.Tiles[height].Add(new(){HasBomb=false});
            }
        }
    }
    // Increase the number of quantity of bombs arround a tile
    private void IncreaseBombQuantifierArround(HeightWidth coord){
        List<HeightWidth> arroundCoords = this.GetTilesArroundCoords(coord);
        foreach(HeightWidth arroundCoord in arroundCoords)
            this.BombQuantifiers[arroundCoord.Height, arroundCoord.Width]++;
    }

    // #################
    // Factories

    public static Field NewField(HeightWidth measure){
        if(measure.Height < 2 || measure.Width < 2) throw new ArgumentException("The height and width of the field must be at least 2.");
        Field field = new() {Height = measure.Height, Width = measure.Width, BombQuantifiers=new int[measure.Height, measure.Width]};
        return field;
    }
    public static Field NewField(Sizes size) => NewField(measures[size]);

    // #################
    // Utils

    // Get the coordinates of the tiles direct arround it (not diagonally arround)
    public List<HeightWidth> GetNeighbourTilesArroundCoords(HeightWidth coords){
        List<HeightWidth> tiles = [];
        if(coords.Height-1 >= 0) tiles.Add(new(coords.Height-1, coords.Width)); // top
        if(coords.Height+1 < this.Height) tiles.Add(new(coords.Height+1, coords.Width)); // bottom
        if(coords.Width-1 >= 0) tiles.Add(new(coords.Height, coords.Width-1)); // left
        if(coords.Width+1 < this.Width) tiles.Add(new(coords.Height, coords.Width+1)); // right
        return tiles;
    }
    // Get the coordinates of the tiles arround the specified tile coordinate
    public List<HeightWidth> GetTilesArroundCoords(HeightWidth coords){
        (int min, int max) rangeHeight = (Math.Max(coords.Height-1, 0), Math.Min(coords.Height+1, this.Height-1));
        (int min, int max) rangeWidth  = (Math.Max(coords.Width-1, 0), Math.Min(coords.Width+1, this.Width-1));

        List<HeightWidth> tilesCoords = [];
        for(int height = rangeHeight.min; height <= rangeHeight.max; height++)
            for(int width = rangeWidth.min; width <= rangeWidth.max; width++)
                if(height != coords.Height || width != coords.Width)
                    tilesCoords.Add(new HeightWidth(height, width));
        return tilesCoords;}
    public Tile GetTile(HeightWidth coords){
        if(!CoordsValids(coords)) throw new ArgumentException("The height and width provided are out of range.");
        return this.Tiles[coords.Height][coords.Width]; }
    private bool CoordsValids(HeightWidth coords) => coords.Height < this.Height && coords.Width < this.Width && coords.Height >= 0 && coords.Width >= 0;
}
