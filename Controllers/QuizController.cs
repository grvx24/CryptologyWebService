using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CryptoWebService.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using CryptoWebService.Models.Quiz;
using CryptoWebService.Models.Quiz.Dto;
using CryptoWebService.Models.Quiz.ViewModel;

namespace CryptoWebService.Controllers
{

    public class QuizController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuizController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Route("quiz/{categoryName?}/{quizNumber?}")]
        public async Task<IActionResult> quiz(string categoryName, int? quizNumber)
        {
            if (String.IsNullOrEmpty(categoryName))
            {
                IEnumerable<CategoryDto> Categories = _context.Category
                    .Select(cat =>new CategoryDto(){
                        Id = cat.Id,
                        CategoryName = cat.CategoryName,
                        SecondCategoryName = changeCategoryName(cat.CategoryName.Replace(" ", String.Empty)),
                        NumberOfQuizzes = _context.Quiz.Where(q => q.CategoryId== cat.Id).Count()
                    }).ToList();

                return View("QuizzesCategory", Categories);
            }
            else
            {
                categoryName = changeCategoryName(categoryName);

                var QuizzesInCategory = _context.Quiz.Where(q => q.Category.CategoryName.Replace(" ", String.Empty) == categoryName)
                    .Select(q => new QuizDto()
                    {
                        Id = q.Id,
                        QuizName = q.QuizName,
                        QuizNumber = q.QuizNumber,
                        NumberOfQuestions = _context.Question.Where(question => question.QuizId == q.Id).Count()
                    }).ToList();

                if (QuizzesInCategory.Count == 0) return NotFound();

                if (quizNumber == null || quizNumber == 0)
                {
                    var viewModel = new QuizzesListViewModel()
                    {
                        CategoryName = _context.Category.Where(c => c.CategoryName.Replace(" ", String.Empty) == categoryName).Select(c => c.CategoryName).FirstOrDefault(),
                        SecondCategoryName = categoryName,
                        Quizzes = QuizzesInCategory
                    };
                    return View("QuizzesList", viewModel);
                }
                else
                {  
                    List<QuestionViewModel> questionViewModels = new List<QuestionViewModel>();
                    List<AnswerViewModel> answerViewModels;

                    var quiz = QuizzesInCategory.Where(q => q.QuizNumber == quizNumber).FirstOrDefault();
                    if (quiz == null) return NotFound();

                    var Questions = _context.Question.Where(question => question.QuizId == quiz.Id).ToList();

                    for (int i = 0; i < Questions.Count(); i++)
                    {
                        List<Answer> answers = _context.Answer.Where(a => a.QuestionId == Questions[i].Id).ToList();
                        answerViewModels = new List<AnswerViewModel>();
                        for (int j = 0; j<answers.Count(); j++)
                        {
                            answerViewModels.Add(new AnswerViewModel()
                            {
                                AnswerContent = answers[j].Content
                            });
                        }

                        questionViewModels.Add(new QuestionViewModel()
                        {
                            QuestionContent = Questions[i].Content,
                            Answers = answerViewModels,
                            Number = i+1,
                            AmountOfQuestionsInQuiz = Questions.Count()
                        });
                    }

                    QuizViewModel quizViewModel = new QuizViewModel();
                    quizViewModel.QuizName = quiz.QuizName;
                    quizViewModel.Questions = questionViewModels;

                    return View("Quiz", quizViewModel);
                }
            }
        }


        [HttpPost]
        public IActionResult CheckQuiz(UserAnswersDto userAnswers)
        {

            return View("Quiz");
        }

        private string changeCategoryName(string categoryName)
        {
            if (categoryName == "funkcjeskrotu")
            {
                categoryName = "funkcjeskrótu";
            }
            else if (categoryName == "szyfrowanieobrazow")
            {
                categoryName = "szyfrowanieobrazów";
            }

            return categoryName;
        }
    }
}
