using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizApi.Configuration;
using QuizApi.Data.Db;
using QuizApi.Data.Db.Enteties;
using QuizApi.Helpers;
using QuizApi.Models;
using QuizApi.Services;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QuizApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class QuizController : ControllerBase
    {
        private readonly QuizService _quizService;

        public QuizController(QuizService quizService) 
        {
            _quizService = quizService;           
        }
     
        [HttpGet]  
        public async Task<ActionResult<QuizzesResponse>> Quizzes()
        {
            var quizzes = await _quizService.GetQuizzesAsync(User.GetUserId());

            return new QuizzesResponse { Quizzes = quizzes };
        }

        [HttpGet]       
        public async Task<ActionResult<QuizResponse>> Quiz([Required][FromQuery] long id)
        {
            var quiz = await _quizService.GetQuizAsync(id);

            if (quiz == null)
                return NotFound();

            return new QuizResponse { Quiz = quiz };
        }

        [HttpPost]
        public async Task<ActionResult<TakePostResponse>> Take([FromBody] TakeRequest request)
        {
            var take = await _quizService.AddTakeAsync(request.QuizId, User.GetUserId());
         
            return new TakePostResponse { TakeId = take.Id, Result = take?.Result };
        }

        [HttpPost]
        public async Task<ActionResult<Result>> Responses([FromBody]QuizResponsesRequest request, [FromServices] IMapper mapper)
        {
            var responses = mapper.Map<Response[]>(request);
            await _quizService.AddResponsesAsync(responses);

            var result = await _quizService.AddResultAsync(request.TakeId);

            return result;
        }

        //[HttpPost]
        //public async Task<ActionResult<Result>> Result(ResultPostRequest request)
        //{
        //    var result = await _quizService.AddResultAsync(request.TakeId);

        //    return result;
        //}

        //[HttpGet]
        //public async Task<ActionResult<Result>> Result([FromQuery][Required] long takeId)
        //{
        //    var result = await _quizService.GetResultAsync(takeId);

        //    if (result == null)
        //        return NotFound();

        //    return result;
        //}

        [HttpGet]
        public async Task<ActionResult<OptionsResponse>> Options([Required][FromQuery]long questionId, [FromServices]IMapper mapper)
        {
            var options = await _quizService.GetOptions(questionId);

            if (options.Count() == 0)
                return NotFound();

            var optionViews = mapper.Map<List<OptionView>>(options);

            return new OptionsResponse { Options = optionViews };
        }
    }
}
