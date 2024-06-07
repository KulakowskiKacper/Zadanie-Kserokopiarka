using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopierZadanie
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;

        public int Counter { get; private set; } = 0;

        public void PowerOn()
        {
            if (GetState() == IDevice.State.off)
            {
                Counter++;
            }
            base.PowerOn();
        }

        public void PowerOff()
        {
            base.PowerOff();
        }

        public void Print(in IDocument document)
        {
            if (GetState() == IDevice.State.on)
            {
                Console.WriteLine($"{DateTime.UtcNow} Print: {document.GetFileName()}");
                PrintCounter++;
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            if (GetState() == IDevice.State.on)
            {
                document = new ImageDocument("Scan" + ScanCounter + "." + formatType.ToString().ToLower());
                Console.WriteLine($"{DateTime.UtcNow} Scan: {document.GetFileName()}");
                ScanCounter++;
            }
            else
            {
                document = null;
            }
        }
        public void ScanAndPrint()
        {
            if (GetState() == IDevice.State.on)
            {
                IDocument document;
                Scan(out document, IDocument.FormatType.JPG);
                Print(document);
            }
        }
    }
}
