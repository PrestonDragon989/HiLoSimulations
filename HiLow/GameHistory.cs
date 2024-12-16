namespace HiLoSimulations.HiLow
{
    /// <summary>
    /// Event in a game of HiLo. This includes the table card, the drawn card, & the guess.
    /// This doesn't hold any deck data, so it isn't comprehensive to a whole game, just a single turn/event.
    /// </summary>
    public struct Event
    {
        public int tableCard;
        public int drawnCard;
        public int guess;

        /// <summary>
        /// Static function to simply create an event based on given info.
        /// </summary>
        /// <param name="tableCard">The card on the table.</param>
        /// <param name="drawnCard">The card drawn from the deck.</param>
        /// <param name="guess">The guess (Higher, Lower).</param>
        /// <returns></returns>
        public static Event CreateEvent(int tableCard, int drawnCard, int guess)
        {
            Event e = new()
            {
                tableCard = tableCard,
                drawnCard = drawnCard,
                guess = guess
            };

            return e;
        }

        /// <summary>
        /// Gets the outcome of the table card to the draw card.
        /// </summary>
        /// <returns>Higher, Lower, or Tie based on the card pulled.</returns>
        public readonly int GetOutcome()
        {
            if (tableCard == drawnCard)
            {
                return Utils.TIE;
            } else if (tableCard > drawnCard)
            {
                return Utils.HIGHER;
            } else
            {
                return Utils.LOWER;
            }
        }

        /// <summary>
        /// The outcome of the guess for the event.
        /// </summary>
        /// <returns>Either Win, Lose, or Tie.</returns>
        public readonly int DidWin()
        {
            int outcome = GetOutcome();
            if (outcome == Utils.TIE)
            {
                return Utils.TIE;
            } else if (outcome == guess)
            {
                return Utils.WIN;
            } else
            {
                return Utils.LOSE;
            }
        }
    }

    /// <summary>
    /// Game history class. This holds a full game, from the deck, to every event until that deck is empty.
    /// The start deck is an array of integers, and the events are in a list.
    /// </summary>
    public class GameHistory
    {
        private readonly int[] _startDeck;
        private readonly List<Event> _events = new();

        public GameHistory(int[] startDeck)
        {
            _startDeck = startDeck;
        }

        /// <summary>
        /// Adds an event to the history list.
        /// </summary>
        /// <param name="tableCard">The card on the table.</param>
        /// <param name="drawnCard">The card drawn.</param>
        /// <param name="guess">The guess for the card.</param>
        public void AddEvent(int tableCard, int drawnCard, int guess)
        {
            _events.Add(Event.CreateEvent(tableCard, drawnCard, guess));
        }

        /// <summary>
        /// Adding a previous event.
        /// </summary>
        /// <param name="e">The given event (Table card, drawn card, guess).</param>
        public void AddEvent(Event e)
        {
            _events.Add(e);
        }

        /// <summary>
        /// Clears all game events.
        /// </summary>
        public void ClearEvents()
        {
            _events.Clear();
        }

        /// <summary>
        /// Prints entire game history. (Deck, Events)
        /// </summary>
        public void PrintHistory()
        {
            int index = 0;
            Console.WriteLine("Shuffled Deck: " + string.Join(", ", _startDeck));
            foreach (Event e in _events)
            {
                index++;
                Console.WriteLine(index.ToString() + ": Table Card - " + e.tableCard + "  Dealt Card - " + e.drawnCard + "  Guess - " + e.guess);
            }
        }

        /// <summary>
        /// Getter for the starting deck.
        /// </summary>
        public int[] StartDeck { get { return _startDeck; } }
        
        
        /// <summary>
        /// Getter for the list of events.
        /// </summary>
        public List<Event> Events { get { return _events; } }
    }
}
