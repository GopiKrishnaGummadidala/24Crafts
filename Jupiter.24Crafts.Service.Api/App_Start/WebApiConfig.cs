using Jupiter._24Crafts.Business.Logic.Account;
using Jupiter._24Crafts.Business.Logic.Account.Interface;
using Jupiter._24Crafts.Business.Logic.Opportunities;
using Jupiter._24Crafts.Business.Logic.Opportunities.Interface;
using Jupiter._24Crafts.Data.UnitOfWork.Account;
using Jupiter._24Crafts.Data.UnitOfWork.Account.Interface;
using Jupiter._24Crafts.Data.UnitOfWork.Opportunities;
using Jupiter._24Crafts.Data.UnitOfWork.Opportunities.Interface;
using Jupiter._24Crafts.Service.Api.Filter;
using Jupiter._24Crafts.Service.Api.Handler;
using Jupiter._24Crafts.Service.Api.Ioc_Container;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Jupiter._24Crafts.Data.UnitOfWork.Portfolio.Interface;
using Jupiter._24Crafts.Data.UnitOfWork.Portfolio;
using Jupiter._24Crafts.Business.Logic.Portfolio.Interface;
using Jupiter._24Crafts.Business.Logic.Portfolio;
using Jupiter._24Crafts.Data.UnitOfWork.Common.Interface;
using Jupiter._24Crafts.Data.UnitOfWork.Common;
using Jupiter._24Crafts.Business.Logic.Common.Interface;
using Jupiter._24Crafts.Business.Logic;
using Jupiter._24Crafts.Business.Logic.Common;
using System.Web.Http.Filters;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.IO.Compression;

namespace Jupiter._24Crafts.Service.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType<IAccountUnitOfWork, AccountUnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IAccountBLL, AccountBLL>(new HierarchicalLifetimeManager());
            container.RegisterType<ITokenBLL, TokenBLL>(new HierarchicalLifetimeManager());

            container.RegisterType<IOpportunitiesUnitOfWork, OpportunitiesUnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IOpportunitiesBLL, OpportunitiesBLL>(new HierarchicalLifetimeManager());

            container.RegisterType<IPortfolioUnitOfWork, PortfolioUnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IPortfolioBLL, PortfolioBLL>(new HierarchicalLifetimeManager());

            container.RegisterType<ILogExceptionUnitOfWork, LogExceptionUnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<ILogException, LogException>(new HierarchicalLifetimeManager());

            container.RegisterType<ICountryUnitOfWork, CountryUnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<ICountryBLL, CountryBLL>(new HierarchicalLifetimeManager());

            container.RegisterType<ICityUnitOfWork, CityUnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<ICityBLL, CityBLL>(new HierarchicalLifetimeManager());

            container.RegisterType<IStateUnitOfWork, StateUnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IStateBLL, StateBLL>(new HierarchicalLifetimeManager());

            container.RegisterType<ICommonUnitOfWork, CommonUnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<ICommonBLL, CommonBLL>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            // Web API configuration and services

            // Web API routes

            config.Filters.Add(new ExceptionHandlingAttribute());
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.MessageHandlers.Add(new WrappingHandler());
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EnableGzip : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            if (context.Response == null) return;
            var acceptEncoding = context.Response.RequestMessage.Headers.AcceptEncoding.ToList();
            if (acceptEncoding.Count == 0)
                return;
            var acceptedEncoding = acceptEncoding.First().Value;
            if (!acceptedEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase)
                && !acceptedEncoding.Equals("deflate", StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            context.Response.Content = new CompressedContent(context.Response.Content, acceptedEncoding);
        }
    }

    public class CompressedContent : HttpContent
    {
        private readonly string _encodingType;
        private readonly HttpContent _originalContent;

        public CompressedContent(HttpContent content, string encodingType = "gzip")
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            _originalContent = content;
            _encodingType = encodingType.ToLowerInvariant();

            foreach (var header in _originalContent.Headers)
            {
                Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
            Headers.ContentEncoding.Add(encodingType);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            Stream compressedStream = null;
            switch (_encodingType)
            {
                case "gzip":
                    compressedStream = new GZipStream(stream, CompressionMode.Compress, true);
                    break;
                case "deflate":
                    compressedStream = new DeflateStream(stream, CompressionMode.Compress, true);
                    break;
                default:
                    compressedStream = stream;
                    break;
            }

            return _originalContent.CopyToAsync(compressedStream).ContinueWith(tsk =>
            {
                if (compressedStream != null)
                {
                    compressedStream.Dispose();
                }
            });
        }
    }
}
