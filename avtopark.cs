using System;
using System.Collections.Generic;

public class Car
{
    public string LicensePlate { get; set; }
    public string Model { get; set; }
    public string DriverName { get; set; }

    public Car(string licensePlate, string model, string driverName)
    {
        LicensePlate = licensePlate;
        Model = model;
        DriverName = driverName;
    }

    public override string ToString()
    {
        return $"LicensePlate: {LicensePlate}, Model: {Model}, DriverName: {DriverName}";
    }
}

public class Fleet
{
    private Car[] _cars;

    public Fleet(Car[] initialCars)
    {
        _cars = initialCars ?? new Car[0];
    }

    public Car this[int index]
    {
        get
        {
            if (index < 0 || index >= _cars.Length)
                throw new IndexOutOfRangeException($"Индекс {index} выходит за пределы диапазона [0, {_cars.Length - 1}]");
            return _cars[index];
        }
        set
        {
            if (index < 0 || index >= _cars.Length)
                throw new IndexOutOfRangeException($"Индекс {index} выходит за пределы диапазона [0, {_cars.Length - 1}]");
            _cars[index] = value;
        }
    }

    public Car this[string licensePlate]
    {
        get
        {
            foreach (var car in _cars)
            {
                if (car?.LicensePlate == licensePlate)
                    return car;
            }
            return null;
        }
        set
        {
            int existingIndex = -1;
            for (int i = 0; i < _cars.Length; i++)
            {
                if (_cars[i]?.LicensePlate == licensePlate)
                {
                    existingIndex = i;
                    break;
                }
            }

            if (existingIndex != -1)
            {
                _cars[existingIndex] = value;
            }
            else
            {
                Array.Resize(ref _cars, _cars.Length + 1);
                _cars[_cars.Length - 1] = value;
            }
        }
    }
}

class Program
{
    static void Main()
    {
        Car[] initialCars = new Car[]
        {
            new Car("A123BC", "Toyota Camry", "Иван Иванов"),
            new Car("B456DE", "Honda Civic", "Пётр Петров"),
            new Car("C789FG", "Ford Focus", "Сергей Сергеев")
        };

        Fleet fleet = new Fleet(initialCars);

        Console.WriteLine("Автомобиль по индексу 1:");
        Console.WriteLine(fleet[1]);
        Console.WriteLine();

        Console.WriteLine("Автомобиль с номерным знаком A123BC:");
        Console.WriteLine(fleet["A123BC"]);
        Console.WriteLine();

        Car newCar = new Car("D012HI", "Mazda 3", "Анна Антонова");
        fleet["D012HI"] = newCar;

        Console.WriteLine("Добавлен новый автомобиль:");
        Console.WriteLine(fleet["D012HI"]);
    }
}
