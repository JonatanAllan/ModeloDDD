using ModeloAplicacao.Interfaces.ServicosApp;
using ModeloAplicacao.ServicosApp;
using ModeloDados.Repositorios;
using ModeloDominio.Interfaces.Repositorios;
using ModeloDominio.Interfaces.Servicos;
using ModeloDominio.Servicos;
using ModeloRecursos.Interfaces.ServicosExternos;
using ModeloRecursos.ServicosExternos;
using ModeloTarefa.Interfaces.Tarefas;
using ModeloTarefa.Tarefas;
using SimpleInjector;

namespace ModeloTransversal
{
    public class Bootstrapper
    {
        public static void RegisterServices(Container container)
        {
            // Crons
            container.Register<IExemploTarefa, ExemploTarefa>(Lifestyle.Scoped);

            // Recursos
            container.Register<IExemploServicosExternos, ExemploServicosExternos>(Lifestyle.Scoped);

            // Aplicacao
            container.Register<IExemploServicosApp, ExemploServicosApp>(Lifestyle.Scoped);

            // Dominio
            container.Register<IExemploServicos, ExemploServicos>(Lifestyle.Scoped);

            // Infra
            container.Register<IExemploRepositorio, ExemploRepositorio>(Lifestyle.Scoped);
        }
    }
}