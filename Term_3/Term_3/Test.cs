using System;
using System.Collections.Generic;
using System.Text;

namespace Term_3
{
    public class Test : IDateAndCopy
    {
        public string subject_name;
        public bool pass_or_fail;

        public DateTime Date { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Test(string subject_nameValue, bool pass_or_failValue)
        {
            subject_name = subject_nameValue;
            pass_or_fail = pass_or_failValue;
        }
        public Test() : this("Biology", true) { }

        public override string ToString() //формированиe строки со значениями всех свойств класса
        {
            if (pass_or_fail is true)
            {
                return subject_name + " is passed";
            }
            else
            {
                return subject_name + " is failed";
            }          
        }

        internal object DeepCopy()
        {
            return new Test { subject_name = this.subject_name, pass_or_fail = this.pass_or_fail };
        }

        object IDateAndCopy.DeepCopy()
        {
            throw new NotImplementedException();
        }
    }
}
