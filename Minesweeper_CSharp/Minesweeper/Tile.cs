namespace Minesweeper;

class Tile{
    public bool HasBomb {get; init;}
    
    public bool Caved {get; set;} = false;
    public bool Flag {get; set;} = false;
}