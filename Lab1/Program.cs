namespace Lab1;

using System;
using System.Linq;
using System.Threading.Tasks.Dataflow;

class Items
{
    public Dictionary<string, string> namesId = new Dictionary<string, string>();
    public Dictionary<string, int> idCosts = new Dictionary<string, int>();
    public Dictionary<string, int> idCount = new Dictionary<string, int>();
}
class Admin
{
    public void addItem(Items space, string item, string id, int cost, int count)
    {
        if (space.namesId.ContainsKey(item))
        {
            Console.WriteLine("Такой товар уже есть");
        }
        else
        {
            space.namesId[item] = id;
            space.idCosts[id] = cost;
            space.idCount[id] = count;
        }
    }
}
class Bank
{
    public Dictionary<string, int> summary = new Dictionary<string, int>();
    public Dictionary<string, int> nominals = new Dictionary<string, int>();
    public Dictionary<string, int> balance = new Dictionary<string, int>();
    public string Change = "Ваша сдача: \n";
    public int totalBalance = 0;
    public int personalBalance = 0;
    public bool unclaimbedChange = false;
    public void getFirstMoney()
    {
        string[] line = Console.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        int temp = 0;
        while (temp < line.Length)
        {
            balance[line[temp]] = 0;
            nominals[line[temp]] = 0;
            summary[line[temp]] = 0;
            temp++;
        }
    }
    public bool getChange(Bank bank, int price, bool flag)
    {
        Dictionary<string, int> temp = new Dictionary<string, int>(bank.summary);
        int changePrice = 0;
        int i = 0;
        bool f = false;
        while (i < temp.Count)
        {
            if (changePrice == price)
            {
                f = true;
                bank.unclaimbedChange = true;
                break;
            }
            if (changePrice + int.Parse(temp.ElementAt(i).Key) < price && (temp.ElementAt(i).Value > 0))
            {
                changePrice += int.Parse(temp.ElementAt(i).Key);
                temp[temp.ElementAt(i).Key] -= 1;
            }
            else if (temp.ElementAt(i).Value == 0)
            {
                i++;
            }
            else if (changePrice + int.Parse(temp.ElementAt(i).Key) > price && (temp.ElementAt(i).Value > 0))
            {
                i++;
            }
            else if (changePrice + int.Parse(temp.ElementAt(i).Key) == price && (temp.ElementAt(i).Value > 0))
            {
                changePrice += int.Parse(temp.ElementAt(i).Key);
                temp[temp.ElementAt(i).Key] -= 1;
            }
        }
        if (f)
        {
            f = false;
            changePrice = 0;
            i = 0;
            while (i < bank.summary.Count)
            {
                if (changePrice == price)
                {
                    f = true;
                    break;
                }
                if (changePrice + int.Parse(bank.summary.ElementAt(i).Key) < price && (bank.summary.ElementAt(i).Value > 0))
                {
                    changePrice += int.Parse(bank.summary.ElementAt(i).Key);
                    bank.summary[bank.summary.ElementAt(i).Key] -= 1;
                    bank.nominals[bank.nominals.ElementAt(i).Key] -= 1;
                    bank.balance[bank.balance.ElementAt(i).Key] += 1;
                }
                else if (bank.summary.ElementAt(i).Value == 0)
                {
                    i++;
                }
                else if (changePrice + int.Parse(bank.summary.ElementAt(i).Key) > price && (bank.summary.ElementAt(i).Value > 0))
                {
                    i++;
                }
                else if (changePrice + int.Parse(bank.summary.ElementAt(i).Key) == price && (bank.summary.ElementAt(i).Value > 0))
                {
                    changePrice += int.Parse(bank.summary.ElementAt(i).Key);
                    bank.summary[bank.summary.ElementAt(i).Key] -= 1;
                    bank.nominals[bank.nominals.ElementAt(i).Key] -= 1;
                    bank.balance[bank.balance.ElementAt(i).Key] += 1;
                }
            }
            totalBalance += price;
            personalBalance = personalBalance - price;
            int tempPrice = 0;
            i = 0;
            while (i < bank.balance.Count)
            {
                if (tempPrice + int.Parse(bank.nominals.ElementAt(i).Key) < personalBalance && (bank.nominals.ElementAt(i).Value > 0))
                {
                    tempPrice += int.Parse(bank.nominals.ElementAt(i).Key);
                    bank.nominals[bank.nominals.ElementAt(i).Key] -= 1;
                    if (flag)
                    {
                        bank.Change += $"Монета, номиналом: {bank.nominals.ElementAt(i).Key}\n";
                    }
                }
                else if (bank.nominals.ElementAt(i).Value == 0)
                {
                    i++;
                }
                else if (tempPrice + int.Parse(bank.nominals.ElementAt(i).Key) > personalBalance && (bank.nominals.ElementAt(i).Value > 0))
                {
                    i++;
                }
                else if (tempPrice + int.Parse(bank.nominals.ElementAt(i).Key) == personalBalance && (bank.nominals.ElementAt(i).Value > 0))
                {
                    tempPrice += int.Parse(bank.nominals.ElementAt(i).Key);
                    bank.nominals[bank.nominals.ElementAt(i).Key] -= 1;
                    if (flag)
                    {
                        bank.Change += $"Монета, номиналом: {bank.nominals.ElementAt(i).Key}\n";
                    }
                }

            }
            i = 0;
            while (i < bank.balance.Count)
            {
                if (tempPrice + int.Parse(bank.balance.ElementAt(i).Key) < personalBalance && (bank.balance.ElementAt(i).Value > 0))
                {
                    tempPrice += int.Parse(bank.balance.ElementAt(i).Key);
                    bank.balance[bank.balance.ElementAt(i).Key] -= 1;
                    if (flag)
                    {
                        bank.Change += $"Монета, номиналом: {bank.balance.ElementAt(i).Key}\n";
                    }
                }
                else if (bank.balance.ElementAt(i).Value == 0)
                {
                    i++;
                }
                else if (tempPrice + int.Parse(bank.balance.ElementAt(i).Key) > personalBalance && (bank.balance.ElementAt(i).Value > 0))
                {
                    i++;
                }
                else if (tempPrice + int.Parse(bank.balance.ElementAt(i).Key) == personalBalance && (bank.balance.ElementAt(i).Value > 0))
                {
                    tempPrice += int.Parse(bank.balance.ElementAt(i).Key);
                    bank.balance[bank.balance.ElementAt(i).Key] -= 1;
                    if (flag)
                    {
                        bank.Change += $"Монета, номиналом: {bank.balance.ElementAt(i).Key}\n";
                    }
                }
            }
        }
        personalBalance = 0;
        return f;
    }
}
class Program
{
    static void Main(string[] args)
    {
        int collect = 0;
        Items items = new Items();
        Admin admin = new Admin();
        Bank bank = new Bank();
        Console.Write("Введите номиналы монет по убыванию.\n");
        bank.getFirstMoney();
        Console.Write("Введите цифру команды.\n" +
        "1. Посмотреть список доступных товаров с их ценами и количеством.\n" +
        "2. Вставить монеты разных номиналов.\n" +
        "3. Выбрать товар.\n" +
        "4. Получить сдачу.\n" +
        "5. Администраторский режим.\n" +
        "6. Выход\n");
        bool condition = true;
        while (condition == true)
        {
            string command = Console.ReadLine()!;
            switch (command)
            {
                default:
                    Console.WriteLine("Неизвестная команда");
                    break;
                case "1":
                    if (items.namesId.Count == 0)
                    {
                        Console.WriteLine("Список товаров пуст\n");
                        break;
                    }
                    Console.WriteLine("Товары в наличии:\n");
                    for (int i = 0; i < items.namesId.Count; i++)
                    {
                        var pair1 = items.namesId.ElementAt(i);
                        var pair2 = items.idCount.ElementAt(i);
                        var pair3 = items.idCosts.ElementAt(i);
                        Console.WriteLine($"Название: {pair1.Key}      ID: {pair1.Value}      Цена: {pair3.Value}       Количество: {pair2.Value}");
                    }
                    break;

                case "2":
                    if (bank.unclaimbedChange)
                    {
                        Console.WriteLine("Сначала получите сдачу! П.4!");
                        break;
                    }
                    Console.WriteLine("Вставьте монеты номинала:");
                    for (int i = 0; i < bank.nominals.Count; i++)
                    {
                        var pair = bank.nominals.ElementAt(i);
                        Console.Write($"{pair.Key} ");
                    }
                    Console.WriteLine("\n");
                    Console.WriteLine("Введите в консоль номинал монет, затем количество через пробел");
                    string[] temp = Console.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    int c = 0;
                    while (temp.Length > c)
                    {
                        bank.nominals[temp[c]] += int.Parse(temp[c + 1]);
                        bank.summary[temp[c]] = bank.nominals[temp[c]] + bank.balance[temp[c]];
                        c = c + 2;
                    }
                    Console.WriteLine($"Ваш баланс: ");
                    for (int i = 0; i < bank.nominals.Count; i++)
                    {
                        var pair = bank.nominals.ElementAt(i);
                        bank.personalBalance += int.Parse(pair.Key) * pair.Value;
                        Console.WriteLine($"Монет номиналом {pair.Key} = {pair.Value}");
                    }
                    Console.WriteLine($"Суммарный баланс = {bank.personalBalance}");
                    break;

                case "3":
                    if (bank.unclaimbedChange)
                    {
                        Console.WriteLine("Сначала получите сдачу! П.4!");
                    }
                    Console.WriteLine("Введите ID товара, который вы хотите купить\n");
                    string getID = Console.ReadLine()!;
                    bool flagID = true;
                    for (int i = 0; i < items.idCosts.Count; i++)
                    {
                        if (items.idCosts.ElementAt(i).Key == getID)
                        {
                            if (items.idCosts.ElementAt(i).Value > bank.personalBalance)
                            {
                                Console.WriteLine("Недостаточно средств!\n");
                                break;
                            }
                            else
                            {
                                if (bank.getChange(bank, items.idCosts.ElementAt(i).Value, flagID))
                                {
                                    Console.WriteLine($"Товар успешно куплен! Чтобы продолжить покупки обязательно получите сдачу!\n");
                                    items.idCount[getID] -= 1;
                                }
                                else
                                {
                                    Console.WriteLine($"Внесите больше монет разных номиналов, чем меньше номинал - тем лучше!\n");
                                }
                            }
                        }
                    }
                    break;

                case "4":
                    Console.WriteLine($"{bank.Change}");
                    bank.Change = $"Ваша сдача: \n";
                    bank.unclaimbedChange = false;
                    break;

                case "5":
                    bool flag = true;
                    while (flag == true)
                    {
                        Console.WriteLine("Выберите команду:\n" + "1. Добавить товары\n" + "2. Забрать деньги из автомата\n" + "3. Выход из админки\n");
                        string cmd = Console.ReadLine()!;
                        switch (cmd)
                        {
                            default:
                                Console.WriteLine("Неизвестная команда, ошибка");
                                break;
                            case "1":
                                Console.WriteLine("Введите через пробел: Название товара, ID товара, Стоимость товара, Количество единиц товара.");
                                string[] item = Console.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                                admin.addItem(items, item[0], item[1], int.Parse(item[2]), int.Parse(item[3]));
                                Console.WriteLine("Товар успешно добавлен");
                                break;
                            case "2":
                                Console.WriteLine("Убедитесь, что автоматом сейчас никто не пользуется!\n Собрать деньги? y/n");
                                string yn = Console.ReadLine()!;
                                if (yn == "y")
                                {
                                    collect += bank.totalBalance;
                                    Console.WriteLine($"Деньги успешно собраны. Собрано всего: {collect}, Собрано сейчас: {bank.totalBalance} ");
                                    for (int i = 0; i < bank.balance.Count; i++)
                                    {
                                        bank.balance[bank.balance.ElementAt(i).Key] = 0;
                                        bank.summary[bank.summary.ElementAt(i).Key] = 0;
                                        bank.nominals[bank.nominals.ElementAt(i).Key] = 0;
                                        bank.totalBalance = 0;
                                        bank.personalBalance = 0;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Операция отменена");
                                }
                                break;
                            case "3":
                                Console.WriteLine("Вы вышли из админ режима");
                                flag = false;
                                break;
                        }
                    }
                    break;

                case "6":
                    condition = false;
                    break;
            }
            if (condition == true)
            {
                Console.WriteLine("Введите цифру команды.\n" +
                "1. Посмотреть список доступных товаров с их ценами и количеством.\n" +
                "2. Вставить монеты разных номиналов.\n" +
                "3. Выбрать товар.\n" +
                "4. Получить сдачу.\n" +
                "5. Администраторский режим.\n" +
                "6. Выход\n");
            }
        }
    }
}
