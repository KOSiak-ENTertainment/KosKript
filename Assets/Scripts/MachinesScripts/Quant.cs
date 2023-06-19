using System;
using System.Collections.Generic;
using System.Text;

/*class Program
{
    static void Main(string[] args)
    {
        string message = "Поскольку ШУЭТ базируется на квантовой механики, которая не особо изучена нашими учеными, то ее работа не всегда корректна. Но если ваш Квантовый туннель работает стабильно, значит проблема в шифрации файла.В таком случае вам надо вручную зашифровать ошибочн";
        BB84 bb84 = new BB84(message);
        var ruk = new Ruk2Bug(bb84);
    }
}*/

class Ruk2Bug
{
    public int[] SecretKey;
    public List<int> IndexNumber = new ();
    public List<int> Number = new ();
    public List<int> DecryptSimbols = new();
    public Ruk2Bug(BB84 bb84)
    {
        SecretKey = bb84._key;
        var IndexStart = new Random().Next(0,bb84.MessagePrevrasVInt.Length-9);
        for (int i = IndexStart; i < IndexStart+8; i++)
        {
            IndexNumber.Add(i);
            Number.Add(bb84.MessagePrevrasVInt[i]);
            DecryptSimbols.Add(bb84.MessagePrevrasVInt[i]+SecretKey[i%SecretKey.Length]);
        }
    }
}
class QuantumChannel
{
    private int[] _qubits;
    private readonly Random _random;

    public QuantumChannel()
    {
        _random = new Random();
    }

    public void TransmitBits(int[] bits)
    {
        _qubits = new int[bits.Length];

        for (var i = 0; i < bits.Length; i++)
        {
            var basis = _random.Next(2);
            if (basis == 0)
            {
                _qubits[i] = bits[i];
            }
            else
            {
                _qubits[i] = (bits[i] + 1) % 2;
            }
        }
    }

    public int[] GetBasis()
    {
        int[] basis = new int[_qubits.Length];
        for (int i = 0; i < _qubits.Length; i++)
        {
            basis[i] = _random.Next(2);
        }
        return basis;
    }

    public void TransmitMeasuredBasis(int[] measuredBasis)
    {
        for (int i = 0; i < measuredBasis.Length; i++)
        {
            if (measuredBasis[i] == 1)
            {
                _qubits[i] = (_qubits[i] + 1) % 2;
            }
        }
    }

    public int[] GetMatchingIndices(int[] basis)
    {
        int[] matchingIndices = new int[basis.Length];
        for (int i = 0; i < basis.Length; i++)
        {
            if (basis[i] == 0)
            {
                matchingIndices[i] = i;
            }
            else
            {
                matchingIndices[i] = -1;
            }
        }
        return matchingIndices;
    }

    public int[] ExtractKey(int[] matchingIndices)
    {
        int[] key = new int[matchingIndices.Length];
        for (int i = 0; i < matchingIndices.Length; i++)
        {
            if (matchingIndices[i] != -1)
            {
                key[i] = _qubits[matchingIndices[i]];
            }
        }
        return key;
    }
}

class ClassicalChannel
{
    private int[] _basis;
    private int[] _measuredBasis;
    private readonly Random _random;

    public ClassicalChannel()
    {
        _random = new Random();
    }

    public void TransmitBasis(int[] basis)
    {
        _basis = basis;
    }

    public int[] GetMeasuredBasis()
    {
        _measuredBasis = new int[_basis.Length];
        for (var i = 0; i < _basis.Length; i++)
        {
            _measuredBasis[i] = _random.Next(2);
        }
        return _measuredBasis;
    }

    public int[] GetBasis()
    {
        return _basis;
    }
}

class BB84
{
    public readonly string _message;
    public int[] _key; //ключ
    public byte[] EncryptedMessage;
    public string DecryptedMessage;
    public byte[] MessagePrevrasVInt;

    public BB84(string message)
    {
        _message = message;
        RunEncryption();
    }

    public void RunEncryption()
    {
        var channel = new QuantumChannel();
        channel.TransmitBits(GenerateBits());

        ClassicalChannel classicalChannel = new ClassicalChannel();
        classicalChannel.TransmitBasis(channel.GetBasis());

        int[] measuredBasis = classicalChannel.GetMeasuredBasis();
        channel.TransmitMeasuredBasis(measuredBasis);

        int[] matchingIndices = channel.GetMatchingIndices(classicalChannel.GetBasis());
        //Console.WriteLine("Matching Indices: " + string.Join(", ", matchingIndices));

        _key = channel.ExtractKey(matchingIndices);
        MessagePrevrasVInt = Encoding.UTF8.GetBytes(_message);
        EncryptedMessage = EncryptMessage(_message, _key);
        //Console.WriteLine("Encrypted Message: " + BitConverter.ToString(encryptedMessage));

        DecryptedMessage = DecryptMessage(EncryptedMessage, _key);
        //Console.WriteLine("Decrypted Message: " + decryptedMessage);
    }

    private int[] GenerateBits()
    {
        Random random = new Random();
        int[] bits = new int[8];
        for (int i = 0; i < bits.Length; i++)
        {
            bits[i] = random.Next(2);
        }
        return bits;    
    }

    private byte[] EncryptMessage(string message, int[] key)
    {
        byte[] encryptedMessage = new byte[MessagePrevrasVInt.Length];
        for (int i = 0; i < MessagePrevrasVInt.Length; i++)
        {
            encryptedMessage[i] = (byte)(MessagePrevrasVInt[i] ^ key[i % key.Length]);
        }
        return encryptedMessage;
    }

    private string DecryptMessage(byte[] encryptedMessage, int[] key)
    {
        byte[] decryptedMessage = new byte[encryptedMessage.Length];
        for (int i = 0; i < encryptedMessage.Length; i++)
        {
            decryptedMessage[i] = (byte)(encryptedMessage[i] ^ key[i % key.Length]);
        }
        return Encoding.UTF8.GetString(decryptedMessage);
    }
}