using System;
using System.Security.Cryptography;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите длину пароля:");
        int passwordLength = int.Parse(Console.ReadLine());

        string password = GeneratePassword(passwordLength);
        Console.WriteLine($"Сгенерированный пароль: {password}");
    }

    static string GeneratePassword(int length)
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+";

        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] tokenData = new byte[length * 4]; // Умножаем на 4, чтобы получить достаточное количество случайных байтов
            rng.GetBytes(tokenData);

            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                uint randomValue = BitConverter.ToUInt32(tokenData, i * 4);
                int index = (int)(randomValue % validChars.Length);
                chars[i] = validChars[index];
            }

            return new string(chars);
        }
    }
}
