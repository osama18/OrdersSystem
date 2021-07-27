using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem
{
    public enum LogEvent
    {
        FailedCreateOrde,
        FailedUpdateContactDetails,
        FailedUpdateContactToError,
        FailedUpdatePaymentDetails,
        FailedUpdateDeliveryDetails,
        FailedUUpdateDeliveryDetails,
        FailedUpdatePaymentDetailsError,
        FailedUpdateDeliveryDetailsError
    }
}
