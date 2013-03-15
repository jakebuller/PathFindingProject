
public class Point
{
    private static int x;
    private static int y;
	public Point(int X, int Y)
	{
        x = X;
        y = Y;
	}

    public int GetX()
    {
        return x;
    }
    public int GetY()
    {
        return y;
    }
    public void SetX(int X)
    {
        x = X;
        return;
    }
    public void SetY(int Y)
    {
        y = Y;
        return;
    }
}
