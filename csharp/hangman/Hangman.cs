using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;

public class HangmanState
{
    public string MaskedWord { get; }
    public ImmutableHashSet<char> GuessedChars { get; }
    public int RemainingGuesses { get; }

    public HangmanState(string maskedWord,
        ImmutableHashSet<char> guessedChars,
        int remainingGuesses)
    {
        MaskedWord = maskedWord;
        GuessedChars = guessedChars;
        RemainingGuesses = remainingGuesses;
    }
}

public class TooManyGuessesException : Exception
{
}

public class Hangman
{
    public IObservable<HangmanState> StateObservable { get; }
    public IObserver<char> GuessObserver { get; }
    private const char HidingChar = '_';
    private const int MaxGuessCount = 9;

    public Hangman(string word)
    {
        var emptySetOfChars = new HashSet<char>();
        var maskedWord = MaskedWord(word, emptySetOfChars);
        var hangmanState = new HangmanState(maskedWord,
            emptySetOfChars.ToImmutableHashSet(),
            MaxGuessCount);
        var stateSubject = new BehaviorSubject<HangmanState>(hangmanState);

        StateObservable = stateSubject;

        GuessObserver = Observer.Create<char>(@char =>
        {
            var guessedChars = new HashSet<char>(stateSubject.Value.GuessedChars);
            var isHit = !guessedChars.Contains(@char)
                && word.Contains(@char);

            guessedChars.Add(@char);

            var maskedWord = MaskedWord(word, guessedChars);

            if (maskedWord == word)
                stateSubject.OnCompleted();
            else if (stateSubject.Value.RemainingGuesses < 1)
                stateSubject.OnError(new TooManyGuessesException());
            else
            {
                var guessCount = isHit
                    ? stateSubject.Value.RemainingGuesses
                    : stateSubject.Value.RemainingGuesses - 1;
                var hangmanState = new HangmanState(maskedWord,
                    guessedChars.ToImmutableHashSet(),
                    guessCount);
                stateSubject.OnNext(hangmanState);
            }
        });
    }

    private string MaskedWord(string word, HashSet<char> guessedChars) =>
        string.Concat(word.Select(@char =>
            guessedChars.Contains(@char) ? @char : HidingChar)
        );
}
