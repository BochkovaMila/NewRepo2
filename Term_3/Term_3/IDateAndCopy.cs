using System;
using System.Collections.Generic;
using System.Text;

namespace Term_3
{
    public interface IDateAndCopy
    {
        object DeepCopy();
        DateTime Date { get; set; }
    }
}
