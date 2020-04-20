using System.Threading.Tasks;

namespace Aiwins.Rocket.Sms {
    public interface ISmsSender {
        Task SendAsync (SmsMessage smsMessage);
    }
}