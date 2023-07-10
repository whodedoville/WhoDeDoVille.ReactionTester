using System.Security.Cryptography;
using System.Text;

namespace WhoDeDoVille.ReactionTester.Domain.Common.Providers;

/// <summary>
/// Helper functions used in multiple locations.
/// </summary>
public static class SharedProvider
{
    /// <summary>
    /// Randomizes string array
    /// </summary>
    /// <param name="myArray"></param>
    /// <returns>string array</returns>
    public static string[] GetRandomizedArray(string[] myArray)
    {
        // TODO: Should be changed to use a Generic List instead of string array.
        string[] myReturnArray = new string[myArray.Length];
        Array.Copy(myArray, myReturnArray, myArray.Length);

        for (int i = 0; i < myArray.Length; i++)
        {
            int rng = RandomNumberGenerator.GetInt32(i, myArray.Length);

            (myReturnArray[rng], myReturnArray[i]) = (myReturnArray[i], myReturnArray[rng]);
        }
        return myReturnArray;
    }

    /// <summary>
    /// Gets random number between low and high value
    /// </summary>
    /// <param name="lowNumber">Low number is included</param>
    /// <param name="highNumber">High number is excluded</param>
    /// <returns>int value</returns>
    public static int GetRandomNumber(int lowNumber, int highNumber)
    {
        if (lowNumber == highNumber) return lowNumber;
        if (lowNumber > highNumber) (lowNumber, highNumber) = (highNumber, lowNumber);
        return RandomNumberGenerator.GetInt32(lowNumber, highNumber);
    }

    /// <summary>
    /// Gets numbers inbetween two values
    /// </summary>
    /// <param name="lowNumber">Low number is included</param>
    /// <param name="highNumber">High number is included</param>
    /// <returns>string array</returns>
    public static string[] ArrayOfStringsFromLowAndHighRange(int lowNumber, int highNumber)
    {
        if (lowNumber == highNumber) return new string[] { lowNumber.ToString() };
        if (lowNumber > highNumber) (lowNumber, highNumber) = (highNumber, lowNumber);
        return Enumerable.Range(lowNumber, (highNumber - lowNumber) + 1).Select(n => n.ToString()).ToArray();
    }

    /// <summary>
    /// Gets numbers inbetween two values
    /// </summary>
    /// <param name="lowNumber">Low number is included</param>
    /// <param name="highNumber">High number is included</param>
    /// <returns>int array</returns>
    public static int[] ArrayOfIntsFromLowAndHighRange(int lowNumber, int highNumber)
    {
        if (lowNumber == highNumber) return new int[] { lowNumber };
        if (lowNumber > highNumber) (lowNumber, highNumber) = (highNumber, lowNumber);
        return Enumerable.Range(lowNumber, (highNumber - lowNumber) + 1).ToArray();
    }

    /// <summary>
    /// Creates hash from string
    /// </summary>
    /// <param name="rawData">String to be hashed</param>
    /// <returns>string that is 64 characters long</returns>
    public static string ComputeSha256Hash(string rawData)
    {
        // Create a SHA256
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
