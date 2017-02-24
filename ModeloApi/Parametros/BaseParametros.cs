using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RestSharp;

namespace ModeloApi.Parametros
{
    public abstract class BaseParametros<TEntidade>
    {
        public virtual Expression<Func<TEntidade, bool>> Expressao()
        {
            return null;
        }

        public virtual List<Parameter> Lista()
        {
            return null;
        }

        public Parameter CriarParametro(string nome, object valor, ParameterType tipo = ParameterType.QueryString) 
            => new Parameter
            {
                Name = nome,
                Value = valor,
                Type = tipo
            };
    }
}