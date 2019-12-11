using System;

public struct ComplexNumber
{
    private readonly double real;
    private readonly double imaginary;

    public ComplexNumber(double real, double imaginary)
    {
        this.real = real;
        this.imaginary = imaginary;
    }
    
    public double Real() => real;

    public double Imaginary() => imaginary;

    public ComplexNumber Mul(ComplexNumber other) =>
        new ComplexNumber(
            real * other.real - imaginary * other.imaginary,
            imaginary * other.real + real * other.imaginary
        );

    public ComplexNumber Add(ComplexNumber other) =>
        new ComplexNumber(
            real + other.real,
            imaginary + other.imaginary
        );

    public ComplexNumber Sub(ComplexNumber other) =>
        new ComplexNumber(
            real - other.real,
            imaginary - other.imaginary
        );

    public ComplexNumber Div(ComplexNumber other)
    {
        var denominator = other.real * other.real + other.imaginary * other.imaginary;

        return new ComplexNumber(
            (real * other.real + imaginary * other.imaginary) / denominator,
            (imaginary * other.real - real * real * other.imaginary) / denominator
        );
    }

    public double Abs() =>
        Math.Sqrt(real * real + imaginary * imaginary);

    public ComplexNumber Conjugate() =>
        new ComplexNumber(
            real,
            -1 * imaginary
        );

    public ComplexNumber Exp()
    {
        var factor = Math.Exp(this.real);

        return new ComplexNumber(
            Math.Cos(this.imaginary) * factor,
            Math.Sin(this.imaginary) * factor
        );
    }
}