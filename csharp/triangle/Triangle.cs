using System;

public static class Triangle
{
    public static bool IsScalene(double side1, double side2, double side3) =>
        IsTriangle(side1, side2, side3)
        && !IsIsosceles(side1, side2, side3);

    public static bool IsIsosceles(double side1, double side2, double side3) =>
        IsTriangle(side1, side2, side3)
        && AtLeastTwoSidesAreEqual(side1, side2, side3);
        
    public static bool IsEquilateral(double side1, double side2, double side3) =>
        IsTriangle(side1, side2, side3)
        && AllSidesAreEqual(side1, side2, side3); 

    public static bool IsTriangle(double side1, double side2, double side3) =>
        side1 < side2 + side3
        && side2 < side1 + side3
        && side3 < side2 + side1;

    private static bool AtLeastTwoSidesAreEqual(double side1, double side2, double side3) =>
        side1 == side2
        || side1 == side3
        || side2 == side3;

    private static bool AllSidesAreEqual(double side1, double side2, double side3) =>
        side1 == side2
        && side1 == side3;
}