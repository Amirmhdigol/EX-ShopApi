using Common.Domain.Bases;
using Common.Domain.Exceptions;
using Common.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public PhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.IsText() || value.Length < 11 || value.Length > 11)
                throw new InvalidDomainDataException("Phone number is Invalid");

            Value = value;
        }
        public string Value { get; set; }

    }
}
