
public class GridCell
{
    public float centerX;
    public float centerY;
    public bool IsOccupied;
    public bool isSinking;

    public GridCell(float x, float y, bool isOccupied, bool isSinking)
    {
        this.centerX = x;
        this.centerY = y;
        this.IsOccupied = isOccupied;
        this.isSinking = isSinking;
    }
}
