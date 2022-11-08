using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfUp.Shared
{
    public class Board
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Length { get; set; }

        public double Width { get; set; }

        public double Thickness { get; set; }

        public double Volume { get; set; }

        public string Type { get; set; }

        public double Price { get; set; }

        public string? Equipment { get; set; }

        public string? Image { get; set; }

        public bool IsRented { get; set; } = false;

        public bool Premium { get; set; }


    }
}
