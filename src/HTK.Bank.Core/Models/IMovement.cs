using System;

namespace HTK.Bank.Core.Models
{
    public interface IMovement
    {
        DateTime Time { get; }
        int X { get; }
        int Y { get; }
        string Type { get; }
        double? Direction { get; }
        double? AngleOfCurvature { get; }
        double? CurvatureDistance { get; }
    }
}