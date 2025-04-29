namespace Backend.Model
{
    public class Transaction(Guid userId, int amount, bool isWithdrawal)
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; } = userId;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public int Amount { get; set; } = amount;
        public bool IsWithdrawal { get; set; } = isWithdrawal;
    }
}
