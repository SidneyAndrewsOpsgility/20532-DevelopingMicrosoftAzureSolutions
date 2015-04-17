using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Contoso.Events.ViewModels
{
    public class VerificationViewModel
    {
        public VerificationViewModel()
        { }

        public bool Verify()
        {
            IPAddress parsedIPAddress;
            if (IPAddress.TryParse(this.IPAddressInput, out parsedIPAddress))
            {
                this.IPAddress = parsedIPAddress;
                return true;
            }
            else
            {
                this.ErrorMessage = "Invalid IP Address";
                return false;
            }
        }

        [Display(Name = "IP Address")]
        public string IPAddressInput { get; set; }

        public IPAddress IPAddress { get; private set; }

        public string ErrorMessage { get; private set; }
    }
}