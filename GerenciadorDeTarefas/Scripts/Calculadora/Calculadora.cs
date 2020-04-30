using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Scripts.Calculadora
{
    public interface ICalculadora
    {
        Task<int> Somar(int a, int b);
        Task<int> Subtrair(int a, int b);
        Task<int> Multiplicar(int a, int b);
        Task<int> Dividir(int a, int b);
    }
    class Calculadora : ICalculadora
    {
        public Task<int> Somar(int a, int b)
        {
            return Task.FromResult(a + b);
        }
        public Task<int> Subtrair(int a, int b)
        {
            return Task.FromResult(a - b);
        }
        public Task<int> Multiplicar(int a, int b)
        {
            return Task.FromResult(a * b);
        }
        public Task<int> Dividir(int a, int b)
        {
            return Task.FromResult(a / b);
        }
    }
}
