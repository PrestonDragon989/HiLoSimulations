namespace HiLoSimulations.Logics
{
    /// <summary>
    /// Placeholder class. Has a single function called guess, that just guesses randomly between Higher and Lower. 
    /// Meant to be either a placeholder, or a control.
    /// </summary>
    public class PlaceholderLogic : Logic
    {
        /// <summary>
        /// Picks a random guess from higher or lower. This is meant to be a control, or a place holder.
        /// </summary>
        /// <param name="tableCard">The card on the table.</param>
        /// <param name="cardValues">The cards still in the deck.</param>
        /// <param name="usedCards">The cards no longer in the deck.</param>
        /// <returns></returns>
        public override int Guess(int tableCard, int[] cardValues, int[] usedCards)
        {
            return Utils.random.Next(Utils.HIGHER, Utils.LOWER + 1);
        }
    }
}
