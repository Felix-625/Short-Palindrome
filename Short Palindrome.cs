using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'shortPalindrome' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts STRING s as parameter.
     */

    public static int shortPalindrome(string s)
    {
        long MOD = 1000000007;
    long result = 0;
    
    // Frequency arrays
    long[] freq = new long[26];           // Single character frequency
    long[,] pairFreq = new long[26, 26];  // Frequency of character pairs (second char, first char)
    long[] tripletFreq = new long[26];    // Frequency of triplets ending with specific character
    
    foreach (char c in s)
    {
        int current = c - 'a';
        
        // Add triplets that can form 'a b b a' with current character as last 'a'
        result = (result + tripletFreq[current]) % MOD;
        
        // Update triplets: for each possible first character, 
        // we can form triplets ending with current character
        for (int i = 0; i < 26; i++)
        {
            tripletFreq[i] = (tripletFreq[i] + pairFreq[i, current]) % MOD;
        }
        
        // Update pairs: for each possible first character,
        // we can form pairs ending with current character
        for (int i = 0; i < 26; i++)
        {
            pairFreq[i, current] = (pairFreq[i, current] + freq[i]) % MOD;
        }
        
        // Update single character frequency
        freq[current]++;
    }
    
    return (int)(result % MOD);
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string s = Console.ReadLine();

        int result = Result.shortPalindrome(s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
