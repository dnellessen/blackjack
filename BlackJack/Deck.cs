
using System.Collections;

namespace CardDeck
{
    class Deck
    {
        private readonly string[] _suits = new string[] { "♣", "♦", "♥", "♠" };
        private readonly Dictionary<string, uint> _symbols = new Dictionary<string, uint>(){
                {"J", 10}, {"Q", 10}, {"K", 10}, {"A", 11}, {"2", 2}, {"3", 3},
                {"4", 4}, {"5", 5}, {"6", 6}, {"7", 7}, {"8", 8}, {"9", 9}, {"10", 10}
            };

        private Stack<Card> Cards = new Stack<Card>();

        public Deck()
        {
            foreach (string suit in this._suits)
            {
                foreach (string symbol in this._symbols.Keys)
                {
                    this.Cards.Push(new Card(suit, symbol, this._symbols[symbol]));
                }
            }

            this.Shuffle();
        }

        public int Size { get { return this.Cards.Count; } }

        /// <summary>
        /// Shuffles the deck.
        /// </summary>
        public void Shuffle()
        {
            Random random = new Random();

            Card[] arr = this.Cards.ToArray();
            for (int i = arr.Length - 1; i > 0; i--)
            {
                Card temp = arr[i];
                int index = random.Next(0, i + 1);
                arr[i] = arr[index];
                arr[index] = temp;
            }

            this.Cards = new Stack<Card>(arr);
        }

        /// <summary>
        /// Returns a card from the deck.
        /// </summary>
        public Card GetCard()
        {
            return this.Cards.Pop();
        }
    }
}