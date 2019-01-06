using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CryptoWebService.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using CryptoWebService.Models.Quiz;

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
                var Categories = _context.Category.ToList();

                return View("QuizzesCategory", Categories);
            }
            else
            {
                categoryName = changeCategoryName(categoryName);

                var QuizzesInCategory = _context.Quiz.Where(q => q.Category.CategoryName.Replace(" ", String.Empty) == categoryName).ToList();

                if (QuizzesInCategory.Count == 0) return NotFound();

                if (quizNumber == null || quizNumber == 0)
                {
                    return View("QuizzesList", QuizzesInCategory);
                }
                else
                {  
                    List<QuestionViewModel> questionViewModels = new List<QuestionViewModel>();
                    List<AnswerViewModel> answerViewModels;

                    var quiz = QuizzesInCategory.Where(q => q.Quiznumber == quizNumber).FirstOrDefault();
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
        public IActionResult CheckQuiz(UserAnswers userAnswers)
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
