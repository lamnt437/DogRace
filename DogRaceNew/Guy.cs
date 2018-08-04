using System.Windows.Forms;

namespace DogRaceNew
{
    public class Guy
    {
        public string Name;
        public int Cash;
        public Bet MyBet = null;
        public Label MyLabel;
        public RadioButton MyRadioButton;

        public void UpdateLabel()
        {
            MyLabel.Text = MyBet.GetDescription();
        }

        public bool PlaceBet(int BetAmount, int DogToWin)
        {
            if (BetAmount <= Cash)
            {
                MyBet = new Bet() { Amount = BetAmount, Dog = DogToWin, Bettor = this };
                UpdateLabel();
                return true;
            }
            MessageBox.Show(Name + " doesn't have enough in cash to bet this amount!");
            return false;
        }

        public void ClearBet()
        {
            MyBet = null;
            MyLabel.Text = Name + " hasn't placed bet yet!";
        }

        public void Collect(int Amount)
        {
            Cash += Amount;
            MessageBox.Show(Name + " has received " + Amount.ToString() + "!");
        }
    }
}