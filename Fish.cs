using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA221003
{
    internal class Fish
    {
		private float _weight;
		private bool _weightIsSet = false;
		private bool _predator;
		private int _swimTop;
		private int _swimDepth;
		private string _species;

		public Fish(float weight, bool predator, int swimTop, int swimDepth, string species)
		{
			Weight = weight;
            _weightIsSet = true;
            Predator = predator;
			SwimTop = swimTop;
			SwimDepth = swimDepth;
			Species = species;
		}

		public float Weight
		{
			get { 
				return _weight;
			}
			
			set {
				
				if (value < 0.5)
					throw new Exception("Túl alacsony súly.");

				if (value > 40)
					throw new Exception("Túl magas súly.");

				if (_weightIsSet && value < _weight * 0.9)
					throw new Exception("Ennyivel nem csökkenhet a mérete.");

				if (_weightIsSet && value > _weight * 1.1)
					throw new Exception("Ennyivel nem nőhet a mérete.");

				_weight = value;
			}
		}

        public bool Predator 
		{ 
			get => _predator; 
			private set => _predator = value; 
		}
        public int SwimTop
		{ 
			get => _swimTop;
			set => _swimTop = value;
		}
        public int SwimDepth
		{ 
			get => _swimDepth;
			set => _swimDepth = value; 
		}
        public string Species
		{
			get => _species; 
			set => _species = value;
		}
    }
}
