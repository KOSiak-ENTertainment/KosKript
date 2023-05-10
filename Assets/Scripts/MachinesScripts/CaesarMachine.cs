using System;
using System.Collections.Generic;
using System.Text;

namespace MachinesScripts
{
    /// <summary>
    /// Русский Алфавит в Кодировке Цезаря
    /// </summary>
    public class CaesarAlphabet
    {
        public readonly Dictionary<int, char> CaesarAlphabetCapitalLetters;
        public readonly Dictionary<int, char> CaesarAlphabetLowercaseLetters;
        public readonly int Shift;

        public CaesarAlphabet()
        {
            CaesarAlphabetLowercaseLetters = new Dictionary<int, char>(33);
            CaesarAlphabetCapitalLetters = new Dictionary<int, char>(33);
            Shift = new Random().Next(2, 10);
            FillAlphabet();
        }

        private void FillAlphabet()
        {
            FillAlphabet('а', 'я');
            FillAlphabet('А', 'Я');
        }
        /// <summary>
        /// Создаем Алфавит Цезаря для зашифрованных букв от Begin до End включительно
        /// </summary>
        /// <param name="begin">Первая буква</param>
        /// <param name="end">Последняя буква</param>
        private void FillAlphabet(char begin,char end)
        {
            var counter = 1;
            for (var letter = begin; letter <= end; letter++,counter++)
            {
                var newSymbol = (char)(letter + Shift);
                if (newSymbol > end)
                    newSymbol = (char)(newSymbol - end + begin-1);
                else if (newSymbol < begin)
                    newSymbol = (char)(end-(begin-newSymbol));
                if (char.IsUpper(begin))
                    CaesarAlphabetCapitalLetters.Add(counter, newSymbol);
                else
                    CaesarAlphabetLowercaseLetters.Add(counter, newSymbol);
            }
        }
    }
    /// <summary>
    /// Машинка Цезаря
    /// </summary>
    public class CaesarMachine
    {
        
        public readonly string UncodedFile;
        public readonly string EncodedFile;
        public readonly CaesarAlphabet CaesarAlphabet;
        
        public CaesarMachine(string file)
        {
            UncodedFile = file;
            CaesarAlphabet = new CaesarAlphabet();
            EncodedFile = CodeThisFile(CaesarAlphabet);


        }
        /// <summary>
        /// Кодируем файл
        /// </summary>
        /// <param name="caesarAlphabet">Алфавит Цезаря со сдвигом</param>
        /// <returns>Кодированный файл</returns>
        private string CodeThisFile(CaesarAlphabet caesarAlphabet)
        {
            var madeText = new StringBuilder();
            foreach (var symbol in UncodedFile)
            {
                if (char.IsLetter(symbol))
                    madeText.Append(CipherLetter(symbol));
                else
                    madeText.Append(symbol);
            }

            return madeText.ToString();
        }
        

        private char CipherLetter(char letter)
        {
            return char.IsUpper(letter) ? CaesarAlphabet.CaesarAlphabetCapitalLetters[letter - 'А'+1] 
                : CaesarAlphabet.CaesarAlphabetLowercaseLetters[letter - 'а'+1];
        }
        
    }
    /// <summary>
    /// Реализация Первого Бага
    /// </summary>
    public class SymbolAutoRegistrationError
    {
        public readonly int CountCipherSymbol;
        public readonly string EncryptedPieceText;
        public readonly string UnencryptedPieceText;
        public readonly int IntervalBegin;
        public SymbolAutoRegistrationError(CaesarMachine caesarMachine)
        {
            var greatAndFuriousRandom = new Random();
            CountCipherSymbol = greatAndFuriousRandom.Next(15, 25);
            IntervalBegin = greatAndFuriousRandom.Next(0, caesarMachine.EncodedFile.Length - CountCipherSymbol);
            if (caesarMachine.UncodedFile[IntervalBegin + CountCipherSymbol] == ' ')
                CountCipherSymbol--;
            EncryptedPieceText = caesarMachine.EncodedFile.Substring(IntervalBegin, CountCipherSymbol);
            UnencryptedPieceText = caesarMachine.UncodedFile.Substring(IntervalBegin, CountCipherSymbol);
        }
    }
    /// <summary>
    /// Реализация Второго Бага
    /// </summary>
    public class SystemOverloadError
    {
        public readonly string EncodedText;
        public readonly string UncodedText;
        public readonly int Shift;
        public readonly char MostPopularLetterInText;
        public readonly int IntervalBegin;
        public readonly int CountCipherSymbol;
        public SystemOverloadError(CaesarMachine caesarMachine)
        {
            var greatAndFuriousRandom = new Random();
            MostPopularLetterInText= caesarMachine.CaesarAlphabet.CaesarAlphabetCapitalLetters[15];
            CountCipherSymbol = greatAndFuriousRandom.Next(20, 35);
            IntervalBegin = greatAndFuriousRandom.Next(0, caesarMachine.EncodedFile.Length - CountCipherSymbol);
            EncodedText = caesarMachine.EncodedFile.Substring(IntervalBegin, CountCipherSymbol);
            UncodedText = caesarMachine.UncodedFile.Substring(IntervalBegin, CountCipherSymbol);
            Shift = caesarMachine.CaesarAlphabet.Shift;
        }
    }
}