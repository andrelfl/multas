﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace multas.Models
{
    public class Multas
    {
        [Key]
        public int ID { get; set; }
        public string Infracao { get; set; }
        public string LocalDaMulta { get; set; }
        public decimal ValorMulta { get; set; }
        public DateTime DataDaMulta { get; set; }

        [ForeignKey("Agente")]
        public int AgenteFK { get; set; }
        public virtual Agentes Agente { get; set; }

        [ForeignKey("Condutor")]
        public int CondutorFK { get; set; }
        public virtual Condutores Condutor { get; set; }

        [ForeignKey("Viatura")]
        public int ViaturaFK { get; set; }
        public virtual Viaturas Viatura { get; set; }
    }
}