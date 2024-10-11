using System;
using System.Collections.Generic;

public class Product : ICloneable
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public object Clone()
    {
        return new Product
        {
            Name = this.Name,
            Price = this.Price,
            Quantity = this.Quantity
        };
    }
}

public class Discount : ICloneable
{
    public string Code { get; set; }
    public decimal Amount { get; set; }

    public object Clone()
    {
        return new Discount
        {
            Code = this.Code,
            Amount = this.Amount
        };
    }
}

public class Order : ICloneable
{
    public List<Product> Products { get; set; } = new List<Product>();
    public decimal ShippingCost { get; set; }
    public List<Discount> Discounts { get; set; } = new List<Discount>();
    public string PaymentMethod { get; set; }

    public object Clone()
    {
        var clonedOrder = new Order
        {
            ShippingCost = this.ShippingCost,
            PaymentMethod = this.PaymentMethod
        };

        // Глубокое клонирование товаров
        foreach (var product in Products)
        {
            clonedOrder.Products.Add((Product)product.Clone());
        }

        // Глубокое клонирование скидок
        foreach (var discount in Discounts)
        {
            clonedOrder.Discounts.Add((Discount)discount.Clone());
        }

        return clonedOrder;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создание первоначального заказа
        var order1 = new Order
        {
            ShippingCost = 5.99m,
            PaymentMethod = "Credit Card"
        };

        order1.Products.Add(new Product { Name = "Laptop", Price = 999.99m, Quantity = 1 });
        order1.Products.Add(new Product { Name = "Mouse", Price = 19.99m, Quantity = 2 });
        order1.Discounts.Add(new Discount { Code = "SUMMER2024", Amount = 100m });

        // Клонирование заказа
        var order2 = (Order)order1.Clone();
        order2.PaymentMethod = "PayPal"; // Изменение метода оплаты для клона

        // Вывод информации о заказах
        Console.WriteLine("Первоначальный заказ:");
        PrintOrder(order1);
        Console.WriteLine("\nКлонированный заказ:");
        PrintOrder(order2);
    }

    static void PrintOrder(Order order)
    {
        Console.WriteLine($"Метод оплаты: {order.PaymentMethod}");
        Console.WriteLine($"Стоимость доставки: {order.ShippingCost:C}");
        Console.WriteLine("Товары:");
        foreach (var product in order.Products)
        {
            Console.WriteLine($"- {product.Name}: {product.Price:C} (Количество: {product.Quantity})");
        }
        Console.WriteLine("Скидки:");
        foreach (var discount in order.Discounts)
        {
            Console.WriteLine($"- Код: {discount.Code}, Сумма: {discount.Amount:C}");
        }
    }
}
