public class Vector3d
{
    //Variables
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public double Length
    {
        get
        {
            return GetLength();
        }
    }

    //Indexer
    public double this[int i]
    {
        get
        {
            switch (i)
            {
                case 0:
                    return this.X;
                case 1:
                    return this.Y;
                case 2:
                    return this.Z;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        set
        {
            switch (i)
            {
                case 0:
                    this.X = value;
                    break;
                case 1:
                    this.Y = value;
                    break;
                case 2:
                    this.Z = value;
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }

    //Constructors
    public Vector3d(double x, double y, double z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    public Vector3d()
    {
        X = 0;
        Y = 0;
        Z = 0;
    }

    public Vector3d(Vector3d other)
    {
        X = other.X;
        Y = other.Y;
        Z = other.Z;
    }

    //Override
    public override string ToString()
    {
        return $"[{X}, {Y}, {Z}]";
    }

    //Methods/Functions
    static Vector3d Add(Vector3d a, Vector3d b)
    {
        double newx = a.X + b.X;
        double newy = a.Y + b.Y;
        double newz = a.Z + b.Z;

        return new Vector3d(newx, newy, newz);
    }

    public static double Dot(Vector3d a, Vector3d b)
    {
        return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
    }

    public static Vector3d Cross(Vector3d a, Vector3d b)
    {
        return new Vector3d(a.Y * b.Z - b.Y * a.Z,
                            a.Z * b.X - a.X * b.Z,
                            a.X * b.Y - a.Y * b.X);
    }

    public static Vector3d XAxis()
    {
        return new Vector3d(1, 0, 0);
    }

    public static Vector3d YAxis()
    {
        return new Vector3d(0, 1, 0);
    }

    public static Vector3d ZAxis()
    {
        return new Vector3d(0, 0, 1);
    }

    public static Vector3d operator +(Vector3d a, Vector3d b)
    {
        return Add(a, b);
    }

    public bool Normalize()
    {
        if (Length == 0)
            return false;
        else
        {
            double len = Length;
            X /= len;
            Y /= len;
            Z /= len;
            return true;
        }
    }


    double GetLength()
    {
        return Math.Sqrt(X * X + Y * Y + Z * Z);
    }
}