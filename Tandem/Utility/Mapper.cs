using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Tandem.Utility
{
    public class Mapper
    {
        private static IMapper _theMapper;

        public Mapper(MapperConfiguration config)
        {
            _theMapper = config.CreateMapper();
        }

        public static TDest MapFrom<TSrc, TDest>(TSrc obj)
        {
            return _theMapper.Map<TSrc, TDest>(obj);
        }
    }
}
