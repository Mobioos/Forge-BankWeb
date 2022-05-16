namespace BankWeb.models.cards {

    public class DeferredCreditCard : ICreditCard {
        
        private int _withdrawCpt;
        private double _deferredWithdraw;
        public DeferredCreditCard() {
            this._withdrawCpt = 0;
            this._deferredWithdraw = 0;
        }

        public double Deposit(double amount) {
            return amount;
        }

        public double Withdraw(double amount) {
            this._withdrawCpt++;
            // We perform the withdraw every two calls
            if (_withdrawCpt % 2 == 0) {
                var toWithdraw = this._deferredWithdraw + amount;
                this._deferredWithdraw = 0;
                return toWithdraw;
            } else {
                this._deferredWithdraw += amount;
                return 0;
            }
        }
    }
}
