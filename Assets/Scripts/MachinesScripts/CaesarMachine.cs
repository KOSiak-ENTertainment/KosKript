using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.IO;
using MachinesScripts;

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
        public readonly int MaxPossibleShift;
        public readonly int MinPossibleShift;
        /// <summary>
        /// Алфавит Цезаря конструктор. Если задается сдвиг в лево, то задается и минимальный символ
        /// </summary>
        /// <param name="maxPossibleShift">Максимально возможнный сдвиг в право</param>
        /// <param name="isMayBeShiftInLeft">Возможен ли сдвиг в лево</param>
        /// <param name="minPossibleShift">Если сдвиг в лево возможен, то задается насколько в лево</param>
        public CaesarAlphabet(int maxPossibleShift,  bool isMayBeShiftInLeft, int minPossibleShift=1)
        {
            var greatAndFuriousRandom = new Random();
            CaesarAlphabetLowercaseLetters = new Dictionary<int, char>(33);
            CaesarAlphabetCapitalLetters = new Dictionary<int, char>(33);
            MaxPossibleShift = maxPossibleShift;
            MinPossibleShift = minPossibleShift;
            Shift = greatAndFuriousRandom.Next(1,MaxPossibleShift);
            if (isMayBeShiftInLeft)
                if (greatAndFuriousRandom.Next(0, 1) == 0)
                    Shift = greatAndFuriousRandom.Next(minPossibleShift,-1);
            FillAlphabet();
        }
        /// <summary>
        /// Создаем Алфавит Цезаря для зашифрованных букв от Begin до End включительно
        /// </summary>
        /// <param name="begin">Первая буква</param>
        /// <param name="end">Последняя буква</param>
        private void FillAlphabet()
        {
            var rusLower = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            var rusCaptital ="АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            for (int letter = 0, counter = 0; letter <= 32; letter++,counter++)
            {
                var newSymbol = letter + Shift;
                newSymbol = newSymbol switch
                {
                    > 32 => newSymbol - 33,
                    < 0 => 32 - newSymbol-1,
                    _ => newSymbol
                };
                CaesarAlphabetCapitalLetters.Add(counter, rusCaptital[newSymbol]);
                CaesarAlphabetLowercaseLetters.Add(counter, rusLower[newSymbol]);
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
        
        public CaesarMachine(string file, int maxPossibleShift, bool isMayBeShiftInLeft,int minPossibleShift)
        {
            UncodedFile = file;
            CaesarAlphabet = new CaesarAlphabet(maxPossibleShift,isMayBeShiftInLeft,minPossibleShift);
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
                madeText.Append(char.IsLetter(symbol) ? CipherLetter(symbol) : symbol);
            return madeText.ToString();
        }
        private char CipherLetter(char letter)
        {
            var rusLower = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            var rusCaptital ="АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            if (char.IsUpper(letter))
            {
                var symbol  = rusCaptital.IndexOf(letter);
                return CaesarAlphabet.CaesarAlphabetCapitalLetters[symbol];
            }
            else
            {
                var symbol  = rusLower.IndexOf(letter);
                return CaesarAlphabet.CaesarAlphabetLowercaseLetters[symbol];
            }
        }
    }
    /// <summary>
    /// Реализация Бага
    /// </summary>
    public class BugCreator
    {
        public readonly int CountCipherSymbol;
        public readonly string EncryptedPieceText;
        public readonly string UnencryptedPieceText;
        public readonly int Shift;
        public readonly char MostPopularLetterInText;
        public readonly int IntervalBegin;
        public readonly int MaxPossibleSymbolToChiper;
        public BugCreator(CaesarMachine caesarMachine)
        {
            //Второй баг
            MostPopularLetterInText=caesarMachine.CaesarAlphabet.CaesarAlphabetCapitalLetters[15];
            Shift = caesarMachine.CaesarAlphabet.Shift;
            //Первый баг
            var greatAndFuriousRandom = new Random();
            IntervalBegin = greatAndFuriousRandom.Next(caesarMachine.EncodedFile.Length / 4,
                caesarMachine.EncodedFile.Length / 2);
            for (var i = IntervalBegin; i < caesarMachine.EncodedFile.Length / 2; i++)
            {
                if (char.IsLetter(caesarMachine.EncodedFile[i]) || char.IsNumber(caesarMachine.EncodedFile[i]))
                {
                    IntervalBegin = i;
                    break;
                }
            }
            MaxPossibleSymbolToChiper = greatAndFuriousRandom.Next(10, 15);
            CountCipherSymbol = 0;
            for (var i = IntervalBegin; i < caesarMachine.EncodedFile.Length-1; i++)
            {
                if (CountCipherSymbol >= MaxPossibleSymbolToChiper)
                    if (char.IsLetter(caesarMachine.EncodedFile[i]) || char.IsNumber(caesarMachine.EncodedFile[i]))
                    {
                        CountCipherSymbol++;
                        break;
                    }
                if (char.IsLetter(caesarMachine.EncodedFile[i]) 
                    || char.IsPunctuation(caesarMachine.EncodedFile[i]) 
                    || char.IsNumber(caesarMachine.EncodedFile[i]) 
                    || caesarMachine.EncodedFile[i] == ' ') 
                    CountCipherSymbol++;
                else
                    IntervalBegin = i+1;
            }
            if (caesarMachine.UncodedFile[IntervalBegin + CountCipherSymbol] == ' ')
                CountCipherSymbol--;
            EncryptedPieceText = caesarMachine.EncodedFile.Substring(IntervalBegin, CountCipherSymbol);
            UnencryptedPieceText = caesarMachine.UncodedFile.Substring(IntervalBegin, CountCipherSymbol);
        }
    }
}