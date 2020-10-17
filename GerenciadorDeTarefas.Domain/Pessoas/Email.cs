using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace GerenciadorDeTarefas.Domain.Pessoas

{
    [Owned]
    public class Email
    {
        []
        public string Value { get; set; }

        [NotMapped]
        private bool IsValid { get => GetIsvalid(); }

        public bool GetIsvalid()
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(Value);
                return addr.Address == Value;
            }
            catch
            {
                return false;
            }
        }


    }
}
