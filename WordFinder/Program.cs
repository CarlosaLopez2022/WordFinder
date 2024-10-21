// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;

// Interface for Word Stream as a Service for DI
public interface IWordStreamService
{
    IEnumerable<string> GetWords();
}

// Implementation of the Word Stream Service
public class WordStreamService : IWordStreamService
{
    private readonly IEnumerable<string> _words;

    public WordStreamService(IEnumerable<string> words)
    {
        _words = words;
    }

    public IEnumerable<string> GetWords()
    {
        return _words;
    }
}

public class WordFinder
{
    private readonly string[] _matrix;

    public WordFinder(IEnumerable<string> matrix)
    {
        _matrix = matrix.ToArray();
    }
    
    public IEnumerable<string> Find(IWordStreamService wordStreamService)
    {
        // Prepare the matrix
        int rows = _matrix.Length;
        int cols = _matrix[0].Length;

        // Collect all horizontal and vertical words in the matrix
        //Using HashSet for the input word stream to ensure unique words are processed
        var wordsInMatrix = new HashSet<string>();

        // Horizontal words
        foreach (var row in _matrix)
        {
            wordsInMatrix.Add(row);
        }

        // Vertical words
        for (int col = 0; col < cols; col++)
        {
            var verticalWord = new char[rows];
            for (int row = 0; row < rows; row++)
            {
                verticalWord[row] = _matrix[row][col];
            }
            wordsInMatrix.Add(new string(verticalWord));
        }

        // Get unique words from the word stream
        var wordCounts = new HashSet<string>(wordStreamService.GetWords());
        var foundWords = new HashSet<string>();

        foreach (var word in wordCounts)
        {
            foreach (var row in wordsInMatrix)
            {
                if (row.Contains(word))
                {
                    foundWords.Add(word);
                }
            }
        }

        // Return the top 10 found words. If not found this retunrs Empty set
        return foundWords.Take(10);
    }
}

// Example usage
public class Program
{
    public static void Main()
    {
        static bool ValidateMatrixCharacters(string[] matrix)
        {
            foreach (string word in matrix)
            {
                if (word.Length != matrix.Length)
                    return false;
                break;
            }
            return true;
        }
        //Change columns or rows to test validation
        var matrix = new[]
        {
            "applejkwyp",
            "bananareed",
            "cdefhybqth",
            "higrapemgk",
            "enorangens",
            "rdfetpluaq",
            "renomajxsu",
            "yfghikwipi",
            "peachvlzvf",
            "applejkwyp"
        };
        //use this to test non existing words
        //var wordStream = new[]
        //{
        //    "eye", "chair", "bulk"
        //};
        var wordStream = new[]
        {
            "apple", "banana", "cherry"
        };

        //Check Matrix must be max 64x64 and all strings contain the same number of characters
        if (matrix.Length > 64 && matrix[0].Length > 64 || !ValidateMatrixCharacters(matrix))
        {
            Console.WriteLine("Not a valid matrix. Must be 64x64 max and same lenght for rows and cols");
        }
        else
        {
            var wordStreamProvider = new WordStreamService(wordStream);
            var finder = new WordFinder(matrix);
            var results = finder.Find(wordStreamProvider);

            foreach (var word in results)
            {
                Console.WriteLine(word);
            }
        }
    }
}
