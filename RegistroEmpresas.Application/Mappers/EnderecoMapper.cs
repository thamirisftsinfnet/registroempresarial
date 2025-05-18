using RegistroEmpresas.Application.DTOs;
using RegistroEmpresas.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Application.Mappers
{
    public static class EnderecoMapper
    {
        public static Endereco ParaEndereco(EnderecoDto dto)
        {
            if (dto == null) return null;

            return new Endereco(
                dto.Logradouro,
                dto.Bairro,
                dto.Cep,
                dto.Estado,
                dto.Cidade,
                dto.Numero
            );
        }
    }
}
