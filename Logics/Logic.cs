namespace HiLoSimulations.Logics
{
    /// <summary>
    /// The abstract class for the logic, which is what the simulations are based on. How accurate certain strategies are, compared to others. 
    /// </summary>
    public abstract class Logic
    {
        /// <summary>
        /// The function that decides whether to guess higher or lower for the guess, based on certain things. This is what defines a logic,
        /// or a strategy. 
        /// </summary>
        /// <param name="tableCard">The current card on the table.</param>
        /// <param name="cardValues">The cards left in the deck.</param>
        /// <param name="usedCards">The cards that are no longer in the deck.</param>
        /// <returns></returns>
        public abstract int Guess(int tableCard, int[] cardValues, int[] usedCards);
    }
}
