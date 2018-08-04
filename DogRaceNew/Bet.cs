namespace DogRaceNew
{
    public class Bet
    {
        public int Amount;
        public int Dog;
        public Guy Bettor;

        public string GetDescription()
        {
            return Bettor.Name + " has bet " + Amount.ToString() + " on dog number " + (Dog + 1).ToString();
        }

        public void Payout(int Winner)
        {
            if (Dog == Winner)
                Bettor.Collect(Amount * 2);
            else
                Bettor.Collect(0 - Amount);
        }
    }
}