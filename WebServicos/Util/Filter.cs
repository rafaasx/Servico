using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebServicos.Models.Enum;

namespace WebServicos.Util
{
    public class Filter
    {
        public int Cliente { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public TipoServico TipoServico { get; set; }
        public decimal ValMin { get; set; }
        public decimal ValMax { get; set; }
    }
}