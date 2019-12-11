using System;
using System.Linq;
using System.Text;

public class SimpleCipher
{
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyz";

    private static readonly Random Rand = new Random();

    public SimpleCipher() =>
        Key = string.Concat(Enumerable
            .Range(0, 100)
            .Select(_ => Alphabet[Rand.Next(Alphabet.Length)])
        );

    public SimpleCipher(string key) =>
        Key = key;

    public string Key { get; }

    public string Encode(string plaintext) =>
        Process(plaintext, EncodeCharacter);

    public string Decode(string ciphertext) =>
        Process(ciphertext, DecodeCharacter);

    private string Process(string text, Func<string, int, char> mechanism)
    {
        var result = new StringBuilder(text.Length);

        for (var index = 0; index < text.Length; index++)
            result.Append(mechanism(text, index));

        return result.ToString();
    }

    private char EncodeCharacter(string plaintext, int index) =>
        Alphabet[GetLetterIndex(plaintext, index, Mechanism.Encode)];

    private char DecodeCharacter(string ciphertext, int index) =>
        Alphabet[GetLetterIndex(ciphertext, index, Mechanism.Decode)];

    private int GetLetterIndex(string text, int index, Mechanism mechanism)
    {
        var letterIndex = Alphabet.IndexOf(text[index]);

        switch (mechanism)
        {
            case Mechanism.Encode:
                letterIndex += Alphabet.IndexOf(Key[index % Key.Length]);

                if (letterIndex >= Alphabet.Length)
                    letterIndex -= Alphabet.Length;

                break;
            case Mechanism.Decode:
                letterIndex -= Alphabet.IndexOf(Key[index % Key.Length]);

                if (letterIndex < 0)
                    letterIndex += Alphabet.Length;

                break;
            default:
                throw new InvalidOperationException($"{mechanism}");
        }

        return letterIndex;
    }

    private enum Mechanism
    {
        Encode,
        Decode
    }
}