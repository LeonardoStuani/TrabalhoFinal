using AluguelCarros.Dominio.Features.Alugueis;
using AluguelCarros.Dominio.Features.Enderecos;
using AluguelCarros.Dominio.Features.Clientes;
using AluguelCarros.Dominio.Features.Carros;
using System;
using AluguelCarros.Dominio.Enums;

namespace AluguelCarros.Comuns.Testes.Base
{
    public static class ConstrutorObjeto
    {
        public static Endereco ObterEndereco()
        {
            return new Endereco
            {
                Id = 0,
                Bairro = "Centro",
                Cep = "12345678",
                Cidade = "Lages",
                Estado = "SC",
                Numero = 446,
                Rua = "Rua do centro"
            };
        }

        public static Cliente ObterCliente()
        {
            return new Cliente
            {
                Id = 0,
                CPF = "665565664",
                DataNascimento = DateTime.Now.AddYears(-18),
                NomeCompleto = "Leonardo Godoi",
                Telefone = "12345687",
                Endereco = ObterEndereco()
            };
        }

        public static Carro ObterCarro()
        {
            return new Carro
            {
                Id = 0,
                Ano = 2017,
                Marca = "Ford",
                Modelo = "Ka",
                Placa = "ILQ - 2255",
                PrecoPorHora = 125.88
            };
        }

        public static Aluguel ObterAluguel()
        {
            return new Aluguel
            {
                Id = 0,
                Carro = ObterCarro(),
                Cliente = ObterCliente(),
                DataDevolucao = DateTime.Now,
                DataEntrada = DateTime.Now.AddDays(-1),
                HorasTotal = 15,
                Pagamento = EPagamento.Dinheiro,
                ValorTotal = 500.99
            };
        }
    }
}
