namespace Domain.Enums
{
    public enum Status
    {
        WaitingForPayment = 1,
        WaitingForConfirmation = 2,
        OnProccess = 3,
        OnDelivery = 4,
        Arrived = 5,
        ItemReceived = 6,
        PaymentSlipInvalid = 7
    }
}
