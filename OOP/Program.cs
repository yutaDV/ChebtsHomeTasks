abstract class Product
{
    protected string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    protected double Weight { get; set; }// те саме що і про назву тільки скорочено
    protected DateTime ExpirationDate { get; set; }

        public Product(string name, double weight, DateTime expirationDate)// конструктур
        {
            Name = name;
            Weight = weight;
            ExpirationDate = expirationDate;
        }

    public virtual void Quality()
    {
        if (DateTime.Today <= ExpirationDate)
        {
            Console.WriteLine("This product is good.");
        }
        else
        {
            Console.WriteLine("This product has expired.");
        }
    }
}

class Fruit : Product
{
    public bool IsRipe { get; set; }

    public Fruit(string name, double weight, DateTime expirationDate, bool isRipe)
        : base(name, weight, expirationDate)
    {
        IsRipe = isRipe;
    }

    public override void Quality()
    {
        if (!IsRipe)
        {
            Console.WriteLine("This fruit is not ripe yet.");
            return;
        }

        base.Quality();
    }

    public void Ripen()
    {
        if (IsRipe)
        {
            Console.WriteLine($"{Name} is already ripe.");
            return;
        }

        Console.WriteLine($"{Name} is now ripe.");
        IsRipe = true;
    }

    public void Eat(double amount)
    {
        if (!IsRipe)
        {
            Console.WriteLine($"{Name} is not ripe yet. You can't eat it.");
            return;
        }

        if (Weight - amount >= 0)
        {
            Weight -= amount;
            Console.WriteLine($"You have eaten {amount:F1} units of {Name}. There are {Weight:F1} units left.");
        }
        else
        {
            Weight = 0;
            Console.WriteLine($"You have eaten {Weight + amount:F1} units of {Name}. There are {Weight:F1} units left.");
        }

        if (Weight == 0)
        {
            Console.WriteLine($"{Name} is all eaten up.");
        }
    }
}

class Meat : Product
{
    public double FatPercentage { get; set; }

    public Meat(string name, double weight, DateTime expirationDate, double fatPercentage)
        : base(name, weight, expirationDate)
    {
        FatPercentage = fatPercentage;
    }

    public override void Quality()
    {
        if (DateTime.Today <= ExpirationDate && FatPercentage < 30)
        {
            Console.WriteLine("This meat is good.");
        }
        else
        {
            Console.WriteLine("This meat has expired or has too much fat.");
        }
    }

    public void Cook()
    {
        Console.WriteLine($"{Name} is now cooked.");
    }
}

class Vegetable : Product
{
    public bool IsOrganic { get; set; }

    public Vegetable(string name, double weight, DateTime expirationDate, bool isOrganic)
        : base(name, weight, expirationDate)
    {
        IsOrganic = isOrganic;
    }

    public override void Quality()
    {
        if (IsOrganic && DateTime.Today <= ExpirationDate)
        {
            Console.WriteLine("This vegetable is good and organic.");
        }
        else if (DateTime.Today <= ExpirationDate)
        {
            Console.WriteLine("This vegetable is good.");
        }
        else
        {
            Console.WriteLine("This vegetable has expired.");
        }
    }

    public void Peel()
    {
        Console.WriteLine($"{Name} is now peeled.");
    }
}

public class Program
{
    static void Main()
    {
        var apple = new Fruit("Apple", 1.2, new DateTime(2023, 4, 1), false);
        apple.Quality(); // This product is good.
        apple.Ripen(); // Apple is now ripe.
        apple.Ripen(); // Apple is already ripe.
        apple.Eat(0.5); // You have eaten 0.5 units of Apple. There are 0.7 units left.
        apple.Eat(0.8); // You have eaten 0.8 units of Apple. There are -0.1 units left.
        apple.Quality(); // This product has expired

        var beef = new Meat("Beef", 1.5, new DateTime(2023, 4, 5), 345);
        beef.Quality(); // This product is good.
        beef.Cook(); // Beef is now cooked.
        beef.Quality(); // This product is good.

        var carrot = new Vegetable("Carrot", 0.3, new DateTime(2023, 4, 1), false);
        carrot.Quality(); // This product is good.
        carrot.Peel(); // Carrot is now peeled.
        carrot.Quality(); // This product is good.
    }
}