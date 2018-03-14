using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WebServicos.Domain.Enum;

namespace WebServicos.Domain
{
    public class Servico
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
        [Required]
        [Display(Name = "Tipo Serviço")]
        public TipoServico TipoServico { get; set; }
        [Display(Name = "Cliente")]
        [ForeignKey("Cliente")]
        public int Cliente_ID { get; set; }
        public Cliente Cliente { get; set; }
        [Display(Name = "Fornecedor")]
        [ForeignKey("Fornecedor")]
        public int Fornecedor_ID { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}