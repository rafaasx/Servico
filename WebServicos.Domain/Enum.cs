using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebServicos.Domain
{
    public static class Enum
    {
        public enum TipoServico
        {
            [Display(Name = "Concerto eletrônico")]
            ConcertoEletronico = 1,
            [Display(Name = "Serviços Gerais")]
            ServicosGerais = 2,
            [Display(Name = "Manutenção Hidráulica")]
            ManutencaoHidraulica = 3,
            [Display(Name = "Instalação Elétrica")]
            InstalacaoEletrica = 4,
            [Display(Name = "Jardinagem")]
            Jardinagem = 5,



        }
    }
}