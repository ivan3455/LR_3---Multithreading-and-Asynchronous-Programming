using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using Serilog.Sinks.File;

class MultithreadingAndAsyncProgrammingDemo
{
    static Random random = new Random();

    static void Main()
    {
        // Налаштовування Serilog для логування у файл
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("multithreading_log.txt")
            .CreateLogger();

        // Демонстрація роботи з класом Thread
        ThreadDemo();

        // Демонстрація роботи з класом Thread(декілька потоків)
        RunMultipleThreads();

        // Демонстрація роботи з Async-Await
        TaskDemo();

        Console.ReadLine();
    }

    // Продемонструвати роботу з класом Thread
    static void ThreadDemo()
    {
        Log.Information("Приклад роботи з класом Thread:");

        Thread thread = new Thread(PerformThreadWork); // Створення нового потоку
        thread.Start(); // Запуск потоку

        Log.Information("Головний потік працює...");
    }

    static void PerformThreadWork()
    {
        Log.Information("Робочий потік виконує роботу...");
        Thread.Sleep(2000); // Пауза у виконанні потоку на 2 секунди
        Log.Information("Робочий потік завершив свою роботу.");
    }

    static void ThreadsWork(object threadInfo)
    {
        string threadName = (string)threadInfo;
        Log.Information($"{threadName} виконує роботу...");
        for (int i = 0; i <= 3; i++)
        {
            int randomNumber = random.Next(0, 101);
            Log.Information($"{threadName}. Спроба {i + 1}. Результат: {randomNumber}");
            Thread.Sleep(300); // Пауза у виконанні потоку на 0.3 секунди
        }
        Log.Information($"{threadName} завершив свою роботу.");
    }

    static void RunMultipleThreads()
    {
        Thread thread1 = new Thread(ThreadsWork);
        Thread thread2 = new Thread(ThreadsWork);
        Thread thread3 = new Thread(ThreadsWork);

        thread1.Start("Потік 1");
        thread2.Start("Потік 2");
        thread3.Start("Потік 3");
    }

    // Демонстрація роботи з Async-Await
    static async void TaskDemo()
    {
        Log.Information("\nПриклад роботи з Async-Await:");

        Task<int> task = PerformAsyncTask(); // Запуск асинхронної задачі
        Log.Information("Головний потік працює...");

        int result = await task; // Очікування завершення асинхронної задачі
        Log.Information($"Результат від асинхронного методу: {result}");
    }

    static async Task<int> PerformAsyncTask()
    {
        Log.Information("Асинхронний метод виконує роботу...");

        await Task.Delay(2000); // Очікування 2 секунди (асинхронно)
        Log.Information("Асинхронний метод завершив свою роботу.");

        return 111;
    }
}
