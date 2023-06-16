using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

public class RSA
{
    public BigInteger SimpleNumberOne; // простое число simpleNumberOne
    public BigInteger SimpleNumberTwo; // простое число simpleNumberTwo
    public BigInteger ModulN; // модуль ModulN = simpleNumberOne * simpleNumberTwo
    public BigInteger FuncEllerFromModulN; // функция Эйлера от ModulN: funcEllerFromModulN = (simpleNumberOne - 1) * (simpleNumberTwo - 1)
    public BigInteger OpenExpo; // открытая экспонента
    public BigInteger CloseExpo; // закрытая экспонента
    public readonly BigInteger[] EncryptedText;
    public readonly string UnEncryptedText;
    public string TextAfterDecrypt;

    public RSA(string text)
    {
        while (CloseExpo<=0 || SimpleNumberOne==SimpleNumberTwo)
        {
            GenerateKeys();
        }

        UnEncryptedText = text;
        EncryptedText = Encrypt(UnEncryptedText,OpenExpo, ModulN);
        TextAfterDecrypt = Decrypt(EncryptedText);
    }

    public BigInteger[] Encrypt(string message, BigInteger e, BigInteger n)
    {
        var bytes = Encoding.UTF8.GetBytes(message); // преобразование текста в массив байтов
        var ciphertext = new BigInteger[bytes.Length];

        for (int i = 0; i < bytes.Length; i++)
        {
            BigInteger m = bytes[i]; // получение очередного байта сообщения
            ciphertext[i] = BigInteger.ModPow(m, e, n); // шифрование байта: ciphertext = m^OpenExpo mod ModulN
        }

        return ciphertext;
    }

    public string Decrypt(BigInteger[] ciphertext)
    {
        var bytes = new byte[ciphertext.Length];

        for (var i = 0; i < ciphertext.Length; i++)
        {
            BigInteger c = ciphertext[i]; // получение очередного зашифрованного байта
            BigInteger m = BigInteger.ModPow(c, CloseExpo, ModulN); // расшифрование байта: m = c^CloseExpo mod ModulN
            bytes[i] = (byte)m; // преобразование байта в значение ASCII
        }

        return Encoding.UTF8.GetString(bytes); // преобразование массива байтов в текстовую строку
    }

    private void GenerateKeys()
    {
        // Шаг 1: выбор двух простых чисел simpleNumberOne и simpleNumberTwo
        SimpleNumberOne = GetPrimeNumber();
        SimpleNumberTwo = GetPrimeNumber();

        // Шаг 2: вычисление модуля ModulN
        ModulN = SimpleNumberOne * SimpleNumberTwo;

        // Шаг 3: вычисление функции Эйлера от ModulN
        FuncEllerFromModulN = (SimpleNumberOne - 1) * (SimpleNumberTwo - 1);

        // Шаг 4: выбор открытой экспоненты
        OpenExpo = 65537;

        // Шаг 5: вычисление закрытой экспоненты CloseExpo с помощью расширенного алгоритма Евклида
        CloseExpo = ExtendedEuclideanAlgorithm(OpenExpo, FuncEllerFromModulN);
    }

    private BigInteger GetPrimeNumber()
    {
        Random random = new Random();
        BigInteger number;
        do
        {
            number = new BigInteger(random.Next(100, 1000)); // пример случайного числа в заданном диапазоне
        } while (!IsPrime(number));
        return number;
    }

    private bool IsPrime(BigInteger number)
    {
        if (number < 2)
            return false;

        for (BigInteger i = 2; i * i <= number; i++)
        {
            if (number % i == 0)
                return false;
        }

        return true;
    }

    private BigInteger ExtendedEuclideanAlgorithm(BigInteger a, BigInteger b)
    {
        BigInteger x0 = 1, x1 = 0, y0 = 0, y1 = 1;

        while (b != 0)
        {
            BigInteger quotient = a / b;

            BigInteger temp = b;
            b = a % b;
            a = temp;

            temp = x1;
            x1 = x0 - quotient * x1;
            x0 = temp;

            temp = y1;
            y1 = y0 - quotient * y1;
            y0 = temp;
        }

        return x0;
    }
}

public class Ruk1Bug
{
    public List<BigInteger> CodSymbol = new ();
    public BigInteger ModulN;
    public List<BigInteger> CompleteSymbol = new ();
    
    public Ruk1Bug(RSA rsa)
    {
        ModulN = rsa.ModulN.ToString()[0] - '0';
        
        for (var i = 0; i < 7; i++)
        {
            var sym = rsa.EncryptedText[i];
            var numberString = sym.ToString();
            BigInteger firstTwoDigits = Convert.ToInt32(numberString.Substring(0, 2));
            
            CodSymbol.Add(firstTwoDigits);
            CompleteSymbol.Add(firstTwoDigits%ModulN);
            Console.WriteLine(firstTwoDigits + " " + firstTwoDigits%ModulN + " " + ModulN);
        }

    }
}

/*public class Program
{
    public static void Main(string[] args)
    {
        for (int i = 0; i < 1000; i++)
        {
            // Text to encrypt
            var plaintext = "Коллега, вы меня поражаете. Второй день и очередной уровень секретного доступа.Так еще и с наградой! Поздравляю вас! На моей памяти, еще не было людей, которые бы получали повышение так скоро! Я считаю, это надо отметить… Например в одном баре, здесь на углу…Ой, уже началась смена, ну, не буду вас отвлекать. Удачного рабочего дня!";
            var rsa = new RSA(plaintext);
            var Ruk = new Ruk1Bug(rsa);
        }
    }
}*/