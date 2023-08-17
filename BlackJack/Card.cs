
namespace CardDeck
{
    class Card
    {
        public readonly static int Width = 9;
        public readonly static int Height = 7;
        private string _suit;
        private string _symbol;

        public Card(string suit, string symbol, uint value)
        {
            this._suit = suit;
            this._symbol = symbol;
            this.Value = value;
        }

        public uint Value { get; set; }

        /// <summary>
        /// Returns a card as string like for example this: J♥
        /// </summary>
        public string AsString { get { return this._suit + this._symbol; } }

        /// <summary>
        /// Returns a card that, looks like a playing card, as a string.
        /// It has a height of 7 and a width of 9 characters (Card.Width, Card.Height).
        /// </summary>
        public string AsStringCard
        {
            get
            {
                if (this._symbol == "10")
                    return $"┌───────┐\n|{this._symbol}     |\n|       |\n|   {this._suit}   |\n|       |\n|     {this._symbol}|\n└───────┘";
                return $"┌───────┐\n|{this._symbol}      |\n|       |\n|   {this._suit}   |\n|       |\n|      {this._symbol}|\n└───────┘";
            }
        }

        /// <summary>
        /// Returns a card that, looks like a playing card, as an array for each row.
        /// Size: 7.
        /// </summary>
        public string[] AsStringArrayCard
        {
            get
            {
                if (this._symbol == "10")
                    return new string[] {
                        "┌───────┐",
                        $"|{this._symbol}     |",
                        "|       |",
                        $"|   {this._suit}   |",
                        "|       |",
                        $"|     {this._symbol}|",
                        "└───────┘"
                    };

                return new string[] {
                    "┌───────┐",
                    $"|{this._symbol}      |",
                    "|       |",
                    $"|   {this._suit}   |",
                    "|       |",
                    $"|      {this._symbol}|",
                    "└───────┘"
                    };
            }
        }

        /// <summary>
        /// Returns an empty card (symbols replaced with ?) that, looks like a playing card, as an array for each row.
        /// Size: 7.
        /// </summary>
        public string[] GetStringArrayHiddenCard
        {
            get
            {
                return new string[] {
                    "┌───────┐",
                    $"|?      |",
                    "|       |",
                    $"|   ?   |",
                    "|       |",
                    $"|      ?|",
                    "└───────┘"
                };
            }
        }
    }
}