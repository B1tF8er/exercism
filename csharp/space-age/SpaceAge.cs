using System;
using System.Collections.Generic;
using Planet = System.String;
using OrbitalPeriod = System.Double;

public class SpaceAge
{
    private const OrbitalPeriod EarthOrbitalPeriod = 31557600;

    private readonly int seconds;

    private readonly IDictionary<Planet, OrbitalPeriod> orbitalPeriods;

    public SpaceAge(int seconds)
    {
        this.seconds = seconds;
        orbitalPeriods = new Dictionary<Planet, OrbitalPeriod>
        {
            { "Earth", 1 },
            { "Mercury", 0.2408467 },
            { "Venus", 0.61519726 },
            { "Mars", 1.8808158 },
            { "Jupiter", 11.862615 },
            { "Saturn", 29.447498 },
            { "Uranus", 84.016846 },
            { "Neptune", 164.79132 }
        };
    }

    public double OnEarth() => CalculateSpaceAgeFor("Earth");

    public double OnMercury() => CalculateSpaceAgeFor("Mercury");

    public double OnVenus() => CalculateSpaceAgeFor("Venus");

    public double OnMars() => CalculateSpaceAgeFor("Mars");

    public double OnJupiter() => CalculateSpaceAgeFor("Jupiter");

    public double OnSaturn() => CalculateSpaceAgeFor("Saturn");

    public double OnUranus() => CalculateSpaceAgeFor("Uranus");

    public double OnNeptune() => CalculateSpaceAgeFor("Neptune");

    private double CalculateSpaceAgeFor(Planet planet) =>
        Math.Round(seconds / (EarthOrbitalPeriod * orbitalPeriods[planet]), 2);
}