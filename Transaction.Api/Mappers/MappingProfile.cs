using System;
using AutoMapper;
using Transaction.Api.Data.Entities;
using Transaction.Api.Extensions;
using Transaction.Api.Models;
using Transaction.Api.Types;

namespace Transaction.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TransactionModel, AccountTransaction >()
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(o => o.AccountNumber))
                .ForAllMembers(opts => opts.Ignore());
            
            CreateMap<TransactionResult, TransactionResultModel>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(o => o.Balance.Amount.ToString("N")))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(o => o.Balance.Currency.ToString())); 
            
            CreateMap<AccountSummaryEntity, AccountSummary>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(o => new Money(o.Balance, o.Currency.TryParseEnum<Currency>())));

            CreateMap<AccountTransaction, AccountTransactionEntity>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(o => DateTime.UtcNow))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(o => o.TransactionType == TransactionType.Deposit ? StringResources.DepositDescription : StringResources.WithdrawDescription))
                .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(o => o.TransactionType.ToString()))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(o => o.Amount.Amount));

            CreateMap<AccountSummary, AccountSummaryEntity>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(o => o.Balance.Amount))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(o => o.Balance.Currency.ToString()))
                .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(o => o.CustomerNumber))
                .ForMember(dest => dest.AccountTransactions, opt => opt.Ignore());

            CreateMap<AccountTransactionEntity, TransactionResult>()
                .ForMember(dest => dest.IsSuccessful, opt => opt.MapFrom(o => o.TransactionId > 0))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(o => o.TransactionId > 0 ? StringResources.TransactionSuccessfull : StringResources.TransactionFailed))
                .ForMember(dest => dest.Balance, opt => opt.Ignore());
            
            CreateMap<AccountSummary, TransactionResult>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(o => o.Balance))
                .ForMember(dest => dest.IsSuccessful, opt => opt.MapFrom(o => true))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(o => StringResources.TransactionSuccessfull));

            CreateMap<AccountTransactionEntity, TransactionInquiryResult>()
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(o => o.AccountNumber))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(o => o.Date))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(o => o.Amount))
                .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(o => o.TransactionType));
        }
    }
}