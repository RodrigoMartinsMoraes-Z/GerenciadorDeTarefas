using System;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Scripts.Calculadora
{
    public class Calcular
    {
        private readonly ICalculadora _calculadora;

        public Calcular(ICalculadora calculadora)
        {
            _calculadora = calculadora;
        }

        public async Task<int> Calculo(int a, int b, Operacao operacao)
        {
            switch (operacao)
            {
                case Operacao.soma:
                    return await _calculadora.Somar(a, b);
                case Operacao.subtracao:
                    return await _calculadora.Subtrair(a, b);
                case Operacao.multiplicacao:
                    return await _calculadora.Multiplicar(a, b);
                case Operacao.divisao:
                    return await _calculadora.Dividir(a, b);
            }
            throw new Exception("Erro ao utilizar a calculadora");
        }
    }
}
