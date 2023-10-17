using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using QuizApi.Data.Db.Enteties;
using QuizApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuizApi.AutoMapper
{
    public class AppAutoMapperProfile : Profile
    {
        public AppAutoMapperProfile()
        {
            CreateMap<Option, OptionView >();
            CreateMap<QuizResponsesRequest, Response[]>()
                .ConvertUsing(x => x.OptionIds.Select(y => new Response { TakeId = x.TakeId, OptionId = y }).ToArray());
                
        }
    }
}
