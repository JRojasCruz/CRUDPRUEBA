using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;

namespace CRUDPRUEBA.Models
{
    public partial class SpTrabajadores
    {
        [Column("Id")]
        public int Id { get; set; }

        public string? TipoDocumento { get; set; }

        public string? NumeroDocumento { get; set; }

        public string? Nombres { get; set; }

        public string? Sexo { get; set; }
        public int? IdDepartamento { get; set; }

        public int? IdProvincia { get; set; }

        public int? IdDistrito { get; set; }
        public string? NombreDepartamento { get; set; }

        public string? NombreProvincia { get; set; }

        public string? NombreDistrito { get; set; }
    }
}
