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


        [Route("quiz/{categoryName?}/{id?}")]
        public async Task<IActionResult> quiz(string categoryName, int? id)
        {
            if (String.IsNullOrEmpty(categoryName))
            {
                // widok wszystkich dostępnych kategorii
                //pobierz liste kategori
                // var Categories = new List<Category>
                //var qs = new QuizService(new ApplicationDbContext(this.con));


                var Categories = _context.Category.ToList();

                return View("QuizzesCategory", Categories);
            }
            else
            {
                if(id == null || id == 0)
                {
                    if (categoryName == "funkcjeskrotu")
                    {
                        categoryName = "funkcjeskrótu";
                    }
                    else if (categoryName == "szyfrowanieobrazow")
                    {
                        categoryName = "szyfrowanieobrazów";
                    }
                    var Quizzes = _context.Quiz.Where(quiz => quiz.Category.CategoryName.Replace(" ",String.Empty) == categoryName).ToList();
                    return View("QuizzesList", Quizzes);
                }
                else
                {  
                    List<QuestionViewModel> questionViewModels = new List<QuestionViewModel>();
                    List<AnswerViewModel> answerViewModels; 

                    var quiz = _context.Quiz.Where(q => q.Id == id).FirstOrDefault();
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


        public List<String> GetCategories()
        {
            return _context.Category.Select(c => c.CategoryName).ToList();
        }
    }
}
