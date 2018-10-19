using AutoMapper;
using server.data.Tables;
using server.Enums;
using server.services.DTOs;
using System;
using System.Reflection;

namespace server.services
{
    public class MappingConfigurations
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(Assembly.GetAssembly(typeof(MappingConfigurations)));
            });
        }

        public class TransactionsToTransactionDTO : Profile
        {
            public TransactionsToTransactionDTO()
            {
                CreateMap<Transactions, TransactionDTO>()
                    .ForMember(d => d.Type, opt => opt.ResolveUsing(resolver => GetStringType(resolver.Type)));
            }

            private static string GetStringType(TRANSACTION_TYPE type)
            {
                return Enum.GetName(typeof(TRANSACTION_TYPE), type).Replace('_', ' ');
            }
        }

        public class TransactionDTOToTransactions : Profile
        {
            public TransactionDTOToTransactions()
            {
                CreateMap<TransactionDTO, Transactions>()
                    .ForMember(d => d.Type, opt => opt.ResolveUsing(resolve => GetEnumType(resolve.Type)));
            }

            private static TRANSACTION_TYPE GetEnumType(string type)
            {
                return (TRANSACTION_TYPE)Enum.Parse(typeof(TRANSACTION_TYPE), type.Replace(' ', '_'));
            }
        }
    }
}
