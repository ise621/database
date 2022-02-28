using Microsoft.EntityFrameworkCore;

namespace Database.Data
{
    [Owned]
    public sealed class CielabColor
    {
        public double LStar { get; private set; }
        public double AStar { get; private set; }
        public double BStar { get; private set; }

        public CielabColor(
            double lStar,
            double aStar,
            double bStar
        )
        {
            LStar = lStar;
            AStar = aStar;
            BStar = bStar;
        }
    }
}