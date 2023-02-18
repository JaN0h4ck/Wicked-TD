public interface Currency {
    int GetBalance();
    /// <summary>
    /// Addiert einen Betrag zum Kontostand
    /// </summary>
    /// <param name="amount"></param>
    /// <returns> "true", wenn der Kontostand danach >= 0, ansonsten "false" </returns>
    bool AddBalance(int amount);
    /// <summary>
    /// Subtrahiert einen Betrag vom Kontostand
    /// </summary>
    /// <param name="amount"></param>
    /// <returns> "true", wenn der Kontostand danach >= 0, ansonsten "false" </returns>
    bool SubstractBalance(int amount);

    void enableUnlimitedMoney();

    void disableUnlimitedMoney();
}
