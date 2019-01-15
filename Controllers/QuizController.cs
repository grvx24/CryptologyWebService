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
using CryptoWebService.Helpers;
using CryptoWebService.Models.Quiz.Model;
using System.Text;
using System.Data.SqlClient;

namespace CryptoWebService.Controllers
{

    public class QuizController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string connectionString;

        public QuizController(ApplicationDbContext context)
        {
            _context = context;
            connectionString = "Server=(localdb)\\mssqllocaldb;Database=QuizDatabase;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        [Route("quiz/{categoryName?}/{quizNumber?}")]
        public async Task<IActionResult> quiz(string categoryName, int? quizNumber,bool? edit)
        {
            if (String.IsNullOrEmpty(categoryName))
            {
                IEnumerable<CategoryDto> Categories = _context.Category
                    .Select(cat => new CategoryDto()
                    {
                        Id = cat.Id,
                        CategoryName = cat.CategoryName,
                        SecondCategoryName = changeCategoryName(cat.CategoryName.Replace(" ", String.Empty)),
                        NumberOfQuizzes = _context.Quiz.Where(q => q.CategoryId == cat.Id).Count()
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

                if (quizNumber == null || quizNumber == 0)
                {
                    var categoryDB = _context.Category.Where(c => c.CategoryName.Replace(" ", String.Empty) == categoryName).FirstOrDefault();
                    if (categoryDB == null) return NotFound();
                    var viewModel = new QuizzesListViewModel()
                    {
                        CategoryName = categoryDB.CategoryName,
                        SecondCategoryName = categoryName,
                        CategoryId = categoryDB.Id,
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
                        for (int j = 0; j < answers.Count(); j++)
                        {
                            answerViewModels.Add(new AnswerViewModel()
                            {
                                AnswerId = answers[j].Id,
                                AnswerContent = answers[j].Content,
                                IsCorrect = ((edit == null) || edit.Value == false) ? null : (bool?)answers[j].Correct
                            });
                        }

                        questionViewModels.Add(new QuestionViewModel()
                        {
                            QuestionContent = Questions[i].Content,
                            Answers = (edit == null ||edit.Value == false) ?  RandomHelper.ShuffleList( answerViewModels): answerViewModels,
                            QuestionId = Questions[i].Id,
                            CanEdit = edit == null ? false : edit.Value,
                            AmountOfQuestionsInQuiz = Questions.Count()
                        });
                    }


                    QuizViewModel quizViewModel = new QuizViewModel();
                    quizViewModel.QuizId = quiz.Id;
                    quizViewModel.QuizName = quiz.QuizName;
                    quizViewModel.AmountOfQuestions = questionViewModels.Count();
                    quizViewModel.Questions = (edit == null || edit.Value == false) ? RandomHelper.ShuffleList(questionViewModels): questionViewModels.OrderByDescending(q => q.QuestionId).ToList();
                    quizViewModel.CanEdit = edit == null ? false : edit.Value;

                    return View("Quiz", quizViewModel);
                }
            }
        }

        [HttpPost]
        [Route("quiz/Checkquiz")]
        public IActionResult Checkquiz([FromBody] UserAnswersDto userAnswers)
        {
            try
            {
                if (userAnswers == null) return Json(new { Result = false, Message = "ERROR - Dane nie zostały przesłane." });

                var quiz = _context.Quiz.Where(q => q.Id == userAnswers.QuizId).FirstOrDefault();
                if (quiz == null) return Json(new { Result = false, Message = "ERROR - Quiz o podanym Id nie istnieje." });

                int Points = 0;
                var questionsDB = _context.Question.Where(q => q.QuizId == quiz.Id);
                foreach (var item in userAnswers.QuestionAnswers)
                {
                    var questionDB = questionsDB.Where(q => q.Id == item.QuestionId).FirstOrDefault();
                    bool CorrectAnswerOnQuestion = true;
                    if (questionDB != null)
                    {
                        var qAnswersDB = _context.Answer.Where(q => q.QuestionId == questionDB.Id);
                        foreach (var itemAnswer in qAnswersDB)
                        {
                            var x = item.SelectedAnswersId.Where(q => q == itemAnswer.Id);
                            if (((x == null || x.Count() == 0) && itemAnswer.Correct)
                                || (x != null && x.Count() > 0 && !itemAnswer.Correct))
                            {
                                CorrectAnswerOnQuestion = false;
                                break;
                            }
                        }
                        if (CorrectAnswerOnQuestion) Points += 1;
                    }
                    else
                    {
                        return Json(new { Result = false, Message = "ERROR - Pytanie o podanym Id nie istnieje." });
                    }
                }

                float score = (float)Points / (float)questionsDB.Count();

                if (score > 0)
                {
                    score *= 100;
                }

                return Json(new { Result = true, score = score });
            }
            catch (Exception e)
            {
                return Json(new { Result = false, Message = "Nieznany bład", ErrorMessage = e.Message });
            }

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


        [HttpPost]
        [Route("quiz/DeleteQuiz")]
        public IActionResult DeleteQuiz([FromBody] int quizID)
        {
            try
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    if (quizID != null)
                    {
                        var quiz = _context.Quiz.Where(q => q.Id == quizID).FirstOrDefault();
                        if (quiz != null)
                        {
                            int questionsInQuiz = _context.Question.Where(q => q.QuizId == quiz.Id).Count();

                            if (questionsInQuiz == 0)
                            {
                                _context.Quiz.Remove(quiz);
                                _context.SaveChanges();
                                return Json(new { Result = true, Message = "Quiz został usunięty." });
                            }
                        }
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                Console.Write("Delete Quiz Error_" + e.Message);
            }
            return Json(new { Result = false, Message = "Nie udało się usunąć quizu" });
        }

        [HttpPost]
        [Route("quiz/DeleteQuestion")]
        public IActionResult DeleteQuestion([FromBody] int questionId)
        {
            try
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    if (questionId != null)
                    {
                        var question = _context.Question.Where(q => q.Id == questionId).FirstOrDefault();
                        if (question != null)
                        {
                            _context.Answer.RemoveRange(_context.Answer.Where(q => q.QuestionId == questionId));
                            _context.SaveChanges();
                            _context.Question.Remove(_context.Question.Where(q => q.Id == questionId).SingleOrDefault());
                            _context.SaveChanges();

                            return Json(new { Result = true, Message = "Pytanie zostało usunięte." });
                        }
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                Console.Write("Delete Question Error_" + e.Message);
            }
            return Json(new { Result = false, Message = "Nie udało się usunąć pytania." });
        }

        [HttpPost]
        [Route("quiz/CreateQuiz")]
        public IActionResult CreateQuiz([FromBody] QuizModel quizModel)
        {
            try
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    if (quizModel != null && !String.IsNullOrEmpty(quizModel.QuizName))
                    {
                        int maxNumer = _context.Quiz.Select(q => q.QuizNumber).Max();

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlTransaction transaction = connection.BeginTransaction("UpdateQuestion");
                            SqlCommand command = new SqlCommand();
                            try
                            {
                                command = connection.CreateCommand();
                                command.CommandText = "INSERT INTO Quiz (QuizName,CategoryId,QuizNumber) VALUES (@v1,@v2,@v3)";
                                command.Parameters.AddWithValue("@v1", quizModel.QuizName);
                                command.Parameters.AddWithValue("@v2", quizModel.CategoryId);
                                command.Parameters.AddWithValue("@v3", maxNumer + 1);
                                command.Transaction = transaction;
                                command.Connection = connection;

                                command.ExecuteNonQuery();
                                transaction.Commit();
                                connection.Close();
                                return Json(new { Result = true, Message = "Quiz został utworzony." });
                            }
                            catch (Exception )
                            {
                                try
                                {
                                    transaction.Rollback();
                                }
                                catch (Exception ex2)
                                {
                                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                                    Console.WriteLine("  Message: {0}", ex2.Message);
                                }
                            }
                        }
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                Console.Write("Create Quiz Error_" + e.Message);
            }
            return Json(new { Result = false, Message = "Nie udało się utworzyć quizu." });
        }

        [HttpPost]
        [Route("quiz/CreateQuestion")]
        public IActionResult CreateQuestion([FromBody] QuestionModel questionModel)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                if(questionModel != null)
                {
                    if(String.IsNullOrEmpty(questionModel.QuestionContent)) return Json(new { Result = false, Message = "Treść pytania nie może być pusta." });
                    if (String.IsNullOrEmpty(questionModel.Answers[0].AnswerContent)) return Json(new { Result = false, Message = "Treść odpowiedzi 1 nie może być pusta." });
                    if (String.IsNullOrEmpty(questionModel.Answers[1].AnswerContent)) return Json(new { Result = false, Message = "Treść odpowiedzi 2 nie może być pusta." });
                    if (String.IsNullOrEmpty(questionModel.Answers[2].AnswerContent)) return Json(new { Result = false, Message = "Treść odpowiedzi 3 nie może być pusta." });
                    if (String.IsNullOrEmpty(questionModel.Answers[3].AnswerContent)) return Json(new { Result = false, Message = "Treść odpowiedzi 4 nie może być pusta." });
                    if (questionModel.Answers.Select(a => a.IsCorrect).Where(q => q == true).Count() <= 0) return Json(new { Result = false, Message = "Chociaż jedna odpowiedź musi być poprawna." });

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlTransaction transaction = connection.BeginTransaction("UpdateQuestion");
                        SqlCommand[] commands = new SqlCommand[5];
                        try
                        {
                            commands[0] = connection.CreateCommand();
                            commands[0].CommandText = "INSERT INTO Question (Content,QuizId) VALUES (@v1,@v2);SELECT CAST(scope_identity() AS int)";
                            commands[0].Parameters.AddWithValue("@v1", questionModel.QuestionContent);
                            commands[0].Parameters.AddWithValue("@v2", questionModel.QuizId);
                            commands[0].Transaction = transaction;
                            commands[0].Connection = connection;

                            var InsertedQuestionId = Convert.ToInt32(commands[0].ExecuteScalar());

                            for (int i = 0; i < 4; i++)
                            {
                                commands[i + 1] = connection.CreateCommand();
                                commands[i + 1].CommandText = "INSERT INTO Answer (Content,Correct,QuestionId) VALUES (@v1,@v2,@v3)";
                                commands[i + 1].Parameters.AddWithValue("@v1", questionModel.Answers[i].AnswerContent);
                                commands[i + 1].Parameters.AddWithValue("@v2", questionModel.Answers[i].IsCorrect);
                                commands[i + 1].Parameters.AddWithValue("@v3", InsertedQuestionId);
                                commands[i + 1].Transaction = transaction;
                                commands[i + 1].Connection = connection;
                            }

                            for (int i = 1; i < 5; i++)
                            {
                                commands[i].ExecuteNonQuery();
                            }

                            transaction.Commit();
                            connection.Close();
                            return Json(new { Result = true, Message = "Pytanie zostało utworzone." });
                        }
                        catch (Exception )
                        {
                            try
                            {
                                transaction.Rollback();
                            }
                            catch (Exception ex2)
                            {
                                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                                Console.WriteLine("  Message: {0}", ex2.Message);
                            }
                        }
                    }
                }
            }
            else
            {
                return Unauthorized();
            }
            return Json(new { Result = false, Message = "Nie udało się utworzyć pytania." });
        }

        [HttpPost]
        [Route("quiz/UpdateQuestion")]
        public IActionResult UpdateQuestion([FromBody] QuestionModel questionModel)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                if (questionModel != null)
                {
                    if (String.IsNullOrEmpty(questionModel.QuestionContent)) return Json(new { Result = false, Message = "Treść pytania nie może być pusta." });
                    if (String.IsNullOrEmpty(questionModel.Answers[0].AnswerContent)) return Json(new { Result = false, Message = "Treść odpowiedzi 1 nie może być pusta." });
                    if (String.IsNullOrEmpty(questionModel.Answers[1].AnswerContent)) return Json(new { Result = false, Message = "Treść odpowiedzi 2 nie może być pusta." });
                    if (String.IsNullOrEmpty(questionModel.Answers[2].AnswerContent)) return Json(new { Result = false, Message = "Treść odpowiedzi 3 nie może być pusta." });
                    if (String.IsNullOrEmpty(questionModel.Answers[3].AnswerContent)) return Json(new { Result = false, Message = "Treść odpowiedzi 4 nie może być pusta." });
                    if (questionModel.Answers.Select(a => a.IsCorrect).Where(q => q == true).Count() <= 0) return Json(new { Result = false, Message = "Chociaż jedna odpowiedź musi być poprawna." });

                    using (SqlConnection connection = new SqlConnection(connectionString))                   
                    {
                        connection.Open();
                        SqlTransaction transaction = connection.BeginTransaction("UpdateQuestion");
                        SqlCommand[] commands = new SqlCommand[5];

                        commands[0] = connection.CreateCommand();
                        commands[0].CommandText = "UPDATE Question SET Content = @v1 WHERE Id = @v2";
                        commands[0].Parameters.AddWithValue("@v1", questionModel.QuestionContent);
                        commands[0].Parameters.AddWithValue("@v2", questionModel.QuestionId);
                        commands[0].Transaction = transaction;
                        commands[0].Connection = connection;
                        
                        for(int i = 0; i < 4; i++)
                        {
                            commands[i+1] = connection.CreateCommand();
                            commands[i+1].CommandText = "UPDATE Answer SET Content = @v1,Correct = @v2 WHERE Id = @v3";
                            commands[i+1].Parameters.AddWithValue("@v1", questionModel.Answers[i].AnswerContent);
                            commands[i+1].Parameters.AddWithValue("@v2", questionModel.Answers[i].IsCorrect);
                            commands[i+1].Parameters.AddWithValue("@v3", questionModel.Answers[i].AnswerId);
                            commands[i+1].Transaction = transaction;
                            commands[i+1].Connection = connection;
                        }

                        try
                        {                           
                            for (int i = 0; i < 5; i++)
                            {
                                commands[i].ExecuteNonQuery();
                            }
                            transaction.Commit();
                            connection.Close();

                            return Json(new { Result = true, Message = "Pytanie zostało zmodyfikowane." });
                        }
                        catch (Exception )
                        {
                            try
                            {
                                transaction.Rollback();
                            }
                            catch (Exception ex2)
                            {
                                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                                Console.WriteLine("  Message: {0}", ex2.Message);
                            }
                        } 
                    } 
                }
            }
            else
            {
                return Unauthorized();
            }
            return Json(new { Result = false, Message = "Nie udało się zmodyfikować pytania." });
        }
    }
}
