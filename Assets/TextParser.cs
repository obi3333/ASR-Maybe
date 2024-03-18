using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class TextParser : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // Reference to the TextMeshProUGUI component on the GameObject

    void Start()
    {
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUI component not found. Attach this script to a GameObject with a TextMeshProUGUI component.");
            return;
        }

        string textToParse = textMeshPro.text;
        
        int umCount = CountUm(textToParse);
        Dictionary<string, int> repeatedWordCounts = CountRepeatedWords(textToParse);

        // Display results in the console
        Debug.Log("Number of 'ums': " + umCount);
        foreach (var pair in repeatedWordCounts)
        {
            Debug.Log("Word '" + pair.Key + "' repeated " + pair.Value + " times.");
        }
    }

    int CountUm(string text)
    {
        // Case-insensitive search for the word "um"
        Regex regex = new Regex(@"\bum\b", RegexOptions.IgnoreCase);
        MatchCollection matches = regex.Matches(text);
        return matches.Count;
        
    }

    Dictionary<string, int> CountRepeatedWords(string text)
    {
        Dictionary<string, int> wordCounts = new Dictionary<string, int>();
        string[] words = text.Split(' ');

        foreach (string word in words)
        {
            string cleanedWord = Regex.Replace(word, "[^a-zA-Z']", ""); // Remove non-alphabetic characters
            cleanedWord = cleanedWord.ToLower(); // Convert to lowercase for case-insensitive comparison

            if (string.IsNullOrEmpty(cleanedWord))
                continue;

            if (wordCounts.ContainsKey(cleanedWord))
                wordCounts[cleanedWord]++;
            else
                wordCounts[cleanedWord] = 1;
        }

        // Filter out words with count 1 (not repeated)
        Dictionary<string, int> filteredWordCounts = new Dictionary<string, int>();
        foreach (var pair in wordCounts)
        {
            if (pair.Value > 1)
            {
                filteredWordCounts.Add(pair.Key, pair.Value);
            }
        }

        return filteredWordCounts;
    }

    public void Update()
    {
        string textToParse = textMeshPro.text;
        
        int umCount = CountUm(textToParse);
        Dictionary<string, int> repeatedWordCounts = CountRepeatedWords(textToParse);

        // Display results in the console
        Debug.Log("Number of 'ums': " + umCount);
        foreach (var pair in repeatedWordCounts)
        {
            Debug.Log("Word '" + pair.Key + "' repeated " + pair.Value + " times.");
        }
    }
}
