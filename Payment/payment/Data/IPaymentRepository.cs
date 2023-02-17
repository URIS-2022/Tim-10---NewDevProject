﻿using payment.Models;

namespace payment.Data
{
    public interface IPaymentRepository
    {
        List<Entities.Payment> GetPayments();

        Entities.Payment GetPaymentById(Guid paymentId);

        PaymentConfirmationDto CreatePayment(Entities.Payment payment);

        PaymentConfirmationDto UpdatePayment(Entities.Payment payment);

        void DeletePayment(Guid paymentId);

        bool SaveChanges();

    }
}
