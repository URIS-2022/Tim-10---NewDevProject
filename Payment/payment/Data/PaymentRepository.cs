using AutoMapper;
using payment.Entities;
using payment.Models;

namespace payment.Data
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentContext context;
        private readonly IMapper mapper;

        public PaymentRepository(PaymentContext context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public Payment CreatePayment(Entities.Payment payment)
        {
            payment.paymentId = Guid.NewGuid();
            var noviEntitet = context.Payments.Add(payment);

            //return mapper.Map<UplataConfirmationDto>(uplata);
            return mapper.Map<Payment>(noviEntitet.Entity);
        }

        public void DeletePayment(Guid paymentId)
        {
            Entities.Payment payment = GetPaymentById(paymentId);
            context.Payments.Remove(payment);
        }

        public List<Entities.Payment> GetPayments()
        {
            return context.Payments.ToList();
        }

        public Entities.Payment GetPaymentById(Guid paymentId)
        {
            return context.Payments.FirstOrDefault(u => u.paymentId == paymentId);
        }

        public PaymentConfirmationDto UpdatePayment(Entities.Payment payment)
        {
            throw new NotImplementedException();
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene ce biti perzistirane
        }




    }
}
