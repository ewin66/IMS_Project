using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Viktor.IMS.Presentation.UI;

namespace Viktor.IMS.Presentation.Infrastructure
{
    public class FormData
    {
        public BaseForm Form { get; set; }
        public bool HasBarcodeScannedEvent { get; set; }

        public FormData(BaseForm form, bool hasBarcodeScannedEvent)
        {
            this.Form = form;
            this.HasBarcodeScannedEvent = hasBarcodeScannedEvent;
        }
    }
}
