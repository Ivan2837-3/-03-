using System;

public class Variant06_Car
{
    public string Model { get; set; } = string.Empty;
    public decimal DailyPrice { get; set; }
}

public class Variant06_RentalRequest
{
    public Variant06_Car Car { get; set; }
    public int Days { get; set; }
    public bool IncludeInsurance { get; set; }
    public bool IncludeChildSeat { get; set; }
}

public class Variant06_RentalFixed
{
    private const decimal DailyInsuranceCost = 500m;
    private const decimal DailyChildSeatCost = 200m;
    private const int LongRentalThresholdDays = 14;
    private const decimal LongRentalDiscountRate = 0.10m;

    public decimal CalculateTotalCost(Variant06_RentalRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (request.Car == null) throw new ArgumentNullException(nameof(request.Car));
        if (request.Days <= 0) throw new ArgumentOutOfRangeException(nameof(request.Days));
        if (request.Car.DailyPrice <= 0) throw new ArgumentOutOfRangeException(nameof(request.Car.DailyPrice));

        decimal total = request.Days * request.Car.DailyPrice;

        if (request.IncludeInsurance)
            total += request.Days * DailyInsuranceCost;

        if (request.IncludeChildSeat)
            total += request.Days * DailyChildSeatCost;

        if (request.Days > LongRentalThresholdDays)
            total -= total * LongRentalDiscountRate;

        return total;
    }
}

class Program
{
    static void Main()
    {
        var car = new Variant06_Car { Model = "Audi Q7", DailyPrice = 800m };
        var request = new Variant06_RentalRequest
        {
            Car = car,
            Days = 15,
            IncludeInsurance = true,
            IncludeChildSeat = false
        };
        var rental = new Variant06_RentalFixed();
        decimal cost = rental.CalculateTotalCost(request);
        Console.WriteLine($"Стоимость аренды: {cost:C}");
    }
}