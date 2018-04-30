using AcmeWebAPI.Data;
using AcmeWebAPI.Data.Entities;
using AcmeWebAPI.Models;
using AcmeWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AcmeWebAPI.Controllers
{
    
    public abstract class BaseApiController : ApiController
    {
        IAcmeRepository _repo;
        protected ILogger _logger;
        ModelFactory _modelFactory;
        EntityFactory _entityFactory;

        public BaseApiController(IAcmeRepository repo, ILogger logger)
        {
            _repo = repo;
            _logger = logger;
        }

        protected IAcmeRepository AcmeRepo
        {
            get
            {
                return _repo;
            }
        }

        protected ModelFactory AcmeModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(AcmeRepo);
                }
                return _modelFactory;
            }
        }
        protected EntityFactory AcmeEntityFactory
        {
            get
            {
                if (_entityFactory == null)
                {
                    _entityFactory = new EntityFactory();
                }
                return _entityFactory;
            }
        }

    }
}