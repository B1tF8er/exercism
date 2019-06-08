using System;
using static System.Math;

public class SpiralMatrix
{
    public static int[,] GetMatrix(int size)
    {
        var circularMatrix = new int[size, size];
        int value = 1,
            firstPivot = 0,
            secondPivot = size - 1;

        while (value <= Pow(size, 2))
        {
            for (int i = firstPivot; i <= secondPivot; i++)
                circularMatrix[firstPivot, i] = value++;

            for (int j = firstPivot + 1; j <= secondPivot; j++)
                circularMatrix[j, secondPivot] = value++;

            for (int i = secondPivot - 1; i >= firstPivot; i--)
                circularMatrix[secondPivot, i] = value++;

            for (int j = secondPivot - 1; j >= firstPivot + 1; j--)
                circularMatrix[j, firstPivot] = value++;

            firstPivot++;
            secondPivot--;
        }

        return circularMatrix;
    }
}
