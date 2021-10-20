using System;
using vtbbook.Application.Domain;
using vtbbook.Application.Service.Models;
using vtbbook.Core.DataAccess.Models;

namespace vtbbook.Application.Service
{
    public class SomeService : ISomeService
    {
        private readonly ISomeDomain _someDomain;

        public SomeService(ISomeDomain someDomain)
        {
            _someDomain = someDomain;
        }

        public Guid Some(SomeModel someModel)
        {
            var dbSome = _someDomain.Add(new DbSome 
            { 
                SomeText = someModel.SomeText,
                SomeSender = someModel.SomeSender
            });

            return dbSome?.Id ?? Guid.Empty;
        }
    }
}
