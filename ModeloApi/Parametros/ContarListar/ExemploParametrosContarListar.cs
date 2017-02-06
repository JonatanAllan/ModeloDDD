using ModeloApi.Recursos;
using ModeloDominio.Entidades;
using System;
using System.Linq.Expressions;

namespace ModeloApi.Parametros.ContarListar
{
    public class ExemploParametrosContarListar : BaseParametrosContarListar<Exemplo>
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public override Expression<Func<Exemplo, bool>> Expressao()
        {
            var condicoes = PredicateBuilder.True<Exemplo>();

            if (Descricao != null)
            {
                condicoes = condicoes.And(x => x.Descricao.Contains(Descricao));
            }

            if (Nome != null)
            {
                condicoes = condicoes.And(x => x.Nome.Contains(Nome));
            }

            return condicoes.Body.NodeType == ExpressionType.Constant ? null : condicoes;
        }
    }
}