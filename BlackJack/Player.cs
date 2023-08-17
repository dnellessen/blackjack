
using CardDeck;

namespace Game
{
    class Player
    {
        private static uint NumPlayers = 0;

        private List<Card> _hand = new List<Card>();
        private uint _count = 0;
        private float _bet = 0f;

        public Player()
        {
            this.Id = NumPlayers;
            this.Name = $"Player{this.Id}";
            NumPlayers++;
        }

        public Player(string name)
        {
            this.Id = NumPlayers;
            this.Name = name;
            NumPlayers++;
        }

        public Player(string name, uint startingMoney)
        {
            this.Id = NumPlayers;
            this.Name = name;
            this.TotalMoney = (int)startingMoney;
            NumPlayers++;
        }

        public uint Id { get; }

        public string Name { get; }

        public uint Count { get { return this._count; } }

        public float Bet
        {
            get { return this._bet; }
            set
            {
                this._bet = value;
                this.TotalMoney -= (float)value;
            }
        }

        public float TotalMoney { get; set; } = 100f;

        public List<Card> Hand
        {
            get { return _hand; }
        }

        /// <summary>
        /// Returns the hand as a single string. E.g.: J♥ 5♠
        /// </summary>
        public string HandAsString
        {
            get
            {
                string handStr = "";
                foreach (Card card in this.Hand)
                {
                    handStr += card.AsString + " ";
                }

                return handStr;
            }
        }

        /// <summary>
        /// Adds a given card to the hand.
        /// </summary>
        public void AddCard(Card card)
        {
            this._hand.Add(card);
            this._count += card.Value;
        }

        /// <summary>
        /// Resets hand and bet.
        /// </summary>
        public void Reset()
        {
            this._hand.Clear();
            this._count = 0;
            this._bet = 0f;
        }

    }
}