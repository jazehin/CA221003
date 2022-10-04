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

		public Fish (Fish fish)
		{
            Weight = fish.Weight;
            _weightIsSet = true;
            Predator = fish.Predator;
            SwimTop = fish.SwimTop;
            SwimDepth = fish.SwimDepth;
            Species = fish.Species;
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
			set {
				if (value < 0)
					throw new Exception("A halak nem tudnak repülni - legalábbis ezek nem.");
				if (value > 400)
					throw new Exception("A hal legmagasabb mélysége nem lehet ennél nagyobb.");
				_swimTop = value;
			}
		}

		public int SwimBottom
		{
			get
			{
				return SwimTop + SwimDepth;
			}
		}

        public int SwimDepth
		{ 
			get => _swimDepth;
			set { 
				if (value < 10)
					throw new Exception("A halnak ennél nagyobb élettérre van szüksége.");
				if (value > 400)
					throw new Exception("A hal mozgási sávja nem lehet szélesebb 400cm-nél");
				_swimDepth = value; 
			}
		}

        public string Species
		{
			get => _species; 
			set { 
				if (value is null)
					throw new Exception("A hal fajtája nem lehet null.");
				if (value.Length < 3)
					throw new Exception("A hal neve nem lehet ilyen rövid.");
				if (value.Length > 30)
					throw new Exception("A hal neve nem lehet ilyen hosszú.");

				_species = value;
			}
		}

		public new string ToString
		{
			get
			{
				return $"{Species}\t{(Species.Length < 8 ? "\t" : "")}{(Predator ? "ragadozó" : "növényevő")}\t{Weight,5:0.00} kg\t{SwimTop,3} cm - {SwimTop + SwimDepth,3} cm";
			}
		}

		public bool CanSwimAt(float depth)
		{
			depth *= 100;
			return (depth >= SwimTop && depth <= SwimBottom);
		}
    }
}
