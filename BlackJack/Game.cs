using CardDeck;

namespace Game
{
    class BlackJack
    {
        private Deck CardDeck;
        private Player player;
        private Player dealer;

        private readonly string banner = "BLACKJACK";
        private string whiteSpaceWidth = "";
        private readonly string keysInfo1 = "(H)it | (S)tand | (Q)uit";
        private readonly string keysInfo2 = "(C)ontinue | (Q)uit";
        private readonly string keysInfo3 = "(Q)uit";

        private float FactorLose { get; } = 0f;
        private float FactorTie { get; } = 1f;
        private float FactorWin { get; } = 2f;
        private float FactorBlackjack { get; } = 2.5f;

        public BlackJack()
        {
            this.CardDeck = new Deck();
            this.dealer = new Player("Dealer");
            this.player = new Player();
        }

        /// <summary>
        /// Prints starting layout.
        /// </summary>
        private void PrintLayout()
        {

            for (this.whiteSpaceWidth = ""; this.whiteSpaceWidth.Length < Console.BufferWidth; this.whiteSpaceWidth += " ") ;

            Console.Clear();

            this.PrintHeader();
            this.PrintFooter();
            this.PrintMidLine();
            this.PrintNames();

            this.PrintMessage(this.whiteSpaceWidth);

            Console.ResetColor();
        }

        /// <summary>
        /// Erases a given line completely.
        /// </summary>
        private void EraseLine(int lineNum)
        {
            ConsoleColor tempBg = Console.BackgroundColor;
            ConsoleColor tempFg = Console.ForegroundColor;
            Console.ResetColor();

            Console.SetCursorPosition(0, lineNum);
            Console.Write(this.whiteSpaceWidth);

            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor = tempFg;
            Console.BackgroundColor = tempBg;
        }

        /// <summary>
        /// Prints header with banner.
        /// </summary>
        private void PrintHeader()
        {
            ConsoleColor tempBg = Console.BackgroundColor;
            ConsoleColor tempFg = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(0, 0);
            Console.Write(this.whiteSpaceWidth);
            Console.SetCursorPosition(Console.BufferWidth / 2 - (this.banner.Length / 2), 0);
            Console.Write(this.banner);

            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor = tempFg;
            Console.BackgroundColor = tempBg;
        }

        /// <summary>
        /// Prints names (dealer and you) to seperate the left from the right.
        /// </summary>
        private void PrintNames()
        {
            ConsoleColor tempBg = Console.BackgroundColor;
            ConsoleColor tempFg = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;

            string left = "DEALER";
            string right = "YOU";
            Console.SetCursorPosition(Console.BufferWidth / 2 - left.Length - 3, 2);
            Console.Write(left);
            Console.SetCursorPosition(Console.BufferWidth / 2 + right.Length - 1, 2);
            Console.Write(right);

            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor = tempFg;
            Console.BackgroundColor = tempBg;
        }

        /// <summary>
        /// Prints line in the middle.
        /// </summary>
        private void PrintMidLine()
        {
            ConsoleColor tempBg = Console.BackgroundColor;
            ConsoleColor tempFg = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 2; i < Console.BufferHeight - 4; i++)
            {
                Console.SetCursorPosition(Console.BufferWidth / 2 - 1, i);
                Console.Write("|");
            }

            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor = tempFg;
            Console.BackgroundColor = tempBg;
        }

        /// <summary>
        /// Prints big cards on the console going out from the center.
        /// </summary>
        private void PrintCards(bool dealerVisible)
        {
            List<List<string>> playerCards = new List<List<string>>();
            List<List<string>> dealerCards = new List<List<string>>();

            foreach (Card card in player.Hand)
                playerCards.Add(card.AsStringArrayCard.ToList());

            if (dealerVisible)
            {
                foreach (Card card in dealer.Hand)
                    dealerCards.Insert(0, card.AsStringArrayCard.ToList());
            }
            else
            {
                foreach (Card card in dealer.Hand)
                    dealerCards.Insert(0, card.GetStringArrayHiddenCard.ToList());
            }

            int cardHeightStart = Console.BufferHeight / 2 - Card.Height / 2 - 1;
            int dealerCardWidthStart = Console.BufferWidth / 2 - Card.Width * dealer.Hand.Count - 10;
            int playerCardWidthStart = Console.BufferWidth / 2 + 10;

            for (int row = 0; row < Card.Height; row++)
            {
                Console.SetCursorPosition(dealerCardWidthStart, row + cardHeightStart);
                for (int i = 0; i < dealer.Hand.Count; i++)
                {
                    Console.Write(dealerCards[i][row]);
                }

                Console.SetCursorPosition(playerCardWidthStart, row + cardHeightStart);
                for (int i = 0; i < player.Hand.Count; i++)
                {
                    Console.Write(playerCards[i][row]);
                }
            }
        }

        /// <summary>
        /// Prints given message above the footer.
        /// </summary>
        private void PrintMessage(string msg, ConsoleColor color = ConsoleColor.Red)
        {
            ConsoleColor tempBg = Console.BackgroundColor;
            ConsoleColor tempFg = Console.ForegroundColor;
            Console.ResetColor();
            Console.ForegroundColor = color;

            this.EraseLine(Console.BufferHeight - 3);   // erase message line
            Console.SetCursorPosition(Console.BufferWidth / 2 - (msg.Length / 2), Console.BufferHeight - 3);
            Console.Write(msg);

            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor = tempFg;
            Console.BackgroundColor = tempBg;
        }

        /// <summary>
        /// Prints the footer of the game.
        /// </summary>
        private void PrintFooter(bool printBet = false, bool printInfo = false, string keysInfo = "")
        {
            ConsoleColor tempBg = Console.BackgroundColor;
            ConsoleColor tempFg = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(0, Console.BufferHeight - 2);
            Console.WriteLine(this.whiteSpaceWidth);

            if (printBet)
            {
                Console.SetCursorPosition(0, Console.BufferHeight - 2);
                Console.Write($" Bet: ${this.player.Bet}");
            }

            if (printInfo)
            {
                Console.SetCursorPosition(Console.BufferWidth / 2 - (keysInfo.Length / 2), Console.BufferHeight - 2);
                Console.Write(keysInfo);
            }

            Console.SetCursorPosition(Console.BufferWidth - string.Format("{0:N2}", this.player.TotalMoney).Length, Console.BufferHeight - 2);
            Console.Write($"${this.player.TotalMoney}");

            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor = tempFg;
            Console.BackgroundColor = tempBg;
        }

        /// <summary>
        /// Reads user input when placing a bet and verifies it.
        /// </summary>
        private void ReadBetFromUser()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            while (true)
            {
                uint bet;

                this.PrintFooter();
                Console.SetCursorPosition(0, Console.BufferHeight - 2);
                Console.Write(" Bet: $");
                Console.SetCursorPosition(7, Console.BufferHeight - 2);

                try
                {
                    string? betStr = Console.ReadLine() ?? throw (new Exception());
                    bet = uint.Parse(betStr);
                }
                catch (Exception)
                {
                    this.PrintMessage("Please enter a number.");
                    continue;
                }

                if (bet < 1 || bet > player.TotalMoney)
                {
                    this.PrintMessage($"Please enter a valid amount. You have ${player.TotalMoney} left.");
                    continue;
                }

                player.Bet = bet;
                break;
            }

            this.PrintMessage("");
            Console.SetCursorPosition(0, 1);
            Console.ResetColor();
        }

        /// <summary>
        /// Resets dealer and player to continue. Waits for user to 
        /// decide to keep playing or quit and returns a boolean accordingly.
        /// </summary>
        private bool ResetAllAndWait()
        {
            dealer.Reset();
            player.Reset();
            this.PrintFooter(false, true, this.keysInfo2);

            Console.SetCursorPosition(0, 0);

            ConsoleKeyInfo userInput;
            while (true)
            {
                userInput = Console.ReadKey(true);
                if (userInput.Key.ToString().ToUpper() == "C")
                {
                    Console.SetCursorPosition(0, 1);
                    return false;
                }
                else if (userInput.Key.ToString().ToUpper() == "Q")
                {
                    Console.SetCursorPosition(0, 1);
                    return true;
                }
            }
        }

        /// <summary>
        /// Start blackjack main loop.
        /// </summary>
        public void Play()
        {

            // main loop
            while (true)
            {
                PrintLayout();

                // broke -> quit
                if (player.TotalMoney < 1)
                {
                    this.PrintMessage("You're broke. Get outta here!", ConsoleColor.White);
                    this.PrintFooter(false, true, this.keysInfo3);

                    Console.SetCursorPosition(0, 0);
                    while (true)
                    {
                        ConsoleKeyInfo userInput = Console.ReadKey(true);
                        if (userInput.Key.ToString().ToUpper() == "Q")
                        {
                            Console.SetCursorPosition(0, 1);
                            return;
                        }
                    }
                }

                // empty deck -> new deck
                if (this.CardDeck.Size == 0)
                    this.CardDeck = new Deck();

                // place bet
                this.ReadBetFromUser();
                this.PrintFooter(true, true, this.keysInfo1);

                // distribute cards
                dealer.AddCard(this.CardDeck.GetCard());
                dealer.AddCard(this.CardDeck.GetCard());
                player.AddCard(this.CardDeck.GetCard());
                player.AddCard(this.CardDeck.GetCard());
                this.PrintCards(false);

                // hit or stand loop
                do
                {
                    // user blackjack
                    if (player.Count == 21)
                        break;

                    // empty deck -> new deck
                    if (this.CardDeck.Size == 0)
                        this.CardDeck = new Deck();

                    // wait key
                    Console.SetCursorPosition(0, 0);
                    ConsoleKeyInfo playerMove = Console.ReadKey(true);
                    Console.SetCursorPosition(0, 1);

                    // check input
                    // hit
                    if (playerMove.Key.ToString().ToUpper() == "H")
                    {
                        player.AddCard(this.CardDeck.GetCard());
                        this.PrintCards(false);

                    }
                    // stand
                    else if (playerMove.Key.ToString().ToUpper() == "S")
                    {
                        break;
                    }
                    // quit
                    else if (playerMove.Key.ToString().ToUpper() == "Q")
                    {
                        Console.Clear();
                        return;
                    }
                }
                while (player.Count < 21);

                // user over 21
                if (player.Count > 21)
                {
                    this.PrintMessage("You Lost.", ConsoleColor.White);
                    player.TotalMoney += player.Bet * this.FactorLose;

                    if (ResetAllAndWait()) return;
                    continue;
                }
                // user blackjack
                else if (player.Count == 21)
                {
                    this.PrintMessage("BLACKJACK. You Won!!", ConsoleColor.White);
                    player.TotalMoney += player.Bet * this.FactorBlackjack;

                    if (ResetAllAndWait()) return;
                    continue;
                }

                // reveal dealer cards
                this.PrintCards(true);

                // distribute cards to dealer when necessary
                while (17 > dealer.Count)
                {
                    // empty deck -> new deck
                    if (this.CardDeck.Size == 0)
                        this.CardDeck = new Deck();

                    dealer.AddCard(this.CardDeck.GetCard());
                    this.PrintCards(true);

                }

                // dealer blackjack
                if (dealer.Count == 21)
                {
                    this.PrintMessage("Dealer BLACKJACK. You Lost.", ConsoleColor.White);
                    player.TotalMoney += player.Bet * this.FactorLose;
                }
                // player higher (below 21) OR dealer over 21
                else if (player.Count > dealer.Count || dealer.Count > 21)
                {
                    this.PrintMessage("You Won!!", ConsoleColor.White);
                    player.TotalMoney += player.Bet * this.FactorWin;
                }
                // dealer higher
                else if (player.Count < dealer.Count)
                {
                    this.PrintMessage("You Lost.", ConsoleColor.White);
                    player.TotalMoney += player.Bet * this.FactorLose;
                }
                // tie
                else
                {
                    this.PrintMessage("Tie.", ConsoleColor.White);
                    player.TotalMoney += player.Bet * this.FactorTie;
                }

                // reset dealer and player and wait for user to decide to keep plaing or not
                if (ResetAllAndWait()) return;
            }
        }
    }
}