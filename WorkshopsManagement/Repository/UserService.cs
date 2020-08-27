using DatabaseServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopsManagement.Models;

namespace WorkshopsManagement.Services
{

    public class UserService
    {
        List<Account> accounts = new List<Account>();
        List<Question> questions = new List<Question>();
        List<Workshops> workshops = new List<Workshops>();

        readonly WebServiceDatabaseSoapClient service = new WebServiceDatabaseSoapClient(WebServiceDatabaseSoapClient.EndpointConfiguration.WebServiceDatabaseSoap);

        private static UserService instance = null;
        public static UserService createUserService()
        {
            if (instance == null)
            {
                instance = new UserService();
                return instance;
            }
            return instance;
        }

        private UserService()
        {
            addToAccounts();
            addToQuestions();
            addToWorkshops();
        }

        public WebServiceDatabaseSoapClient getService()
        {
            return service;
        }

        public int nr = 0;
        public int nrRepetitii = 0;
        public int nrDeSters = 0;
        public int PasswordCheck(string pass)
        {
            if (pass.Length < 6) //conditions for a password that have lower than 6 characters
            {
                if (pass.Length < 3)
                {
                    return nr += (6 - pass.Length);
                }

                if (pass.Length == 3)
                {
                    if (pass[0] == pass[1] && pass[1] == pass[2]) return 4;
                    else return 3;
                }
                else if (pass.Length == 4)
                {
                    if ((pass[0] == pass[1] && pass[1] == pass[2]) || pass[1] == pass[2] && pass[2] == pass[3]) return 3;
                    else if (!pass.Any(char.IsDigit) && !pass.All(char.IsLower) && !pass.Any(char.IsUpper)) return 3;
                    else return 2;
                }
                else
                {
                    if (!pass.Any(char.IsDigit) && !pass.Any(char.IsLower) && !pass.Any(char.IsUpper)) return 3;
                    else if (pass.Any(char.IsUpper) && pass.Any(char.IsLower) && pass.Any(char.IsDigit)) if (pass[0] != pass[1] && pass[1] != pass[2] || pass[1] == pass[2] && pass[2] == pass[3] || pass[2] == pass[3] && pass[3] == pass[4]) return 1;
                        else return 2;
                    else return 2;
                }
            }
            for (int i = 0; i < pass.Length - 2; i++) // verify how many group of repeated characters are in string 
            {
                if (pass[i] == pass[i + 1] && pass[i + 1] == pass[i + 2])
                {

                    if (i < pass.Length - 3) if (pass[i] == pass[i + 1] && pass[i + 1] == pass[i + 2] && pass[i + 2] == pass[i + 3])
                        {
                            if (i < pass.Length - 4)
                            {
                                if (pass[i] == pass[i + 1] && pass[i + 1] == pass[i + 2] && pass[i + 2] == pass[i + 3] && pass[i + 3] == pass[i + 4])
                                {
                                    nrRepetitii++;
                                    i += 2;
                                }
                            }
                            else
                            {
                                nrRepetitii++;
                                i += 2;
                            }
                        }
                        else
                        {
                            nrRepetitii++;
                        }
                    else
                    {
                        nrRepetitii++;
                    }
                }
            }
            if (pass.Length > 20) // conditions for the password that have more than 20 characters
            {
                nrDeSters = pass.Length - 20;
                if (nrRepetitii > nrDeSters) if (nrRepetitii >= nrDeSters + 3) return nrRepetitii;
                    else if (nrRepetitii >= nrDeSters + 2 && !pass.Any(char.IsUpper) && !pass.Any(char.IsLower) && !pass.Any(char.IsDigit)) return nrRepetitii + 1;
                    else if ((!pass.Any(char.IsUpper) && !pass.Any(char.IsLower)) || (!pass.Any(char.IsDigit) && !pass.Any(char.IsLower)) || (!pass.Any(char.IsDigit) && !pass.Any(char.IsUpper))) return nrRepetitii;
                    else if ((nrRepetitii >= nrDeSters + 1) && (!pass.Any(char.IsUpper) && !pass.Any(char.IsLower) && !pass.Any(char.IsDigit))) return nrDeSters + 2;
                    else if ((!pass.Any(char.IsUpper) && !pass.Any(char.IsDigit)) || (!pass.Any(char.IsLower) && !pass.Any(char.IsUpper)) || (!pass.Any(char.IsDigit) && !pass.Any(char.IsLower))) return nrRepetitii + 1;
                    else return nrRepetitii;
                else if (nrRepetitii < nrDeSters && !pass.Any(char.IsUpper) && !pass.Any(char.IsLower) && !pass.Any(char.IsDigit)) return nrDeSters + 3;
                else if ((!pass.Any(char.IsUpper) && !pass.Any(char.IsLower)) || (!pass.Any(char.IsDigit) && !pass.Any(char.IsLower)) || (!pass.Any(char.IsDigit) && !pass.Any(char.IsLower))) return nrDeSters + 2;
                else return nrDeSters + 3;
            }
            if (nrRepetitii > 0) // conditions if we have group of repeated characters and missing characters
            {
                if (nrRepetitii == 1)
                {
                    if ((!pass.Any(char.IsUpper) && !pass.Any(char.IsLower)) || (!pass.Any(char.IsLower) && !pass.Any(char.IsDigit)) || (!pass.Any(char.IsDigit) && !pass.Any(char.IsUpper))) return 2;
                    if (!pass.Any(char.IsUpper) && !pass.Any(char.IsLower) && !pass.Any(char.IsDigit)) return 3;

                    return 1;
                }
                else if (nrRepetitii == 2)
                {
                    if (!pass.Any(char.IsUpper) && !pass.Any(char.IsLower) && !pass.Any(char.IsDigit)) return 3;
                    return 2;
                }
                else
                {
                    return nrRepetitii;
                }
            }
            if (!pass.Any(char.IsDigit)) nr++;//see if there are at least one digit
            if (!pass.Any(char.IsLower)) nr++;//see if there are at least one lowercase letter
            if (!pass.Any(char.IsDigit)) nr++;//see if there are at least one uppercase letter
            return nr + nrRepetitii;
        }

        public async Task<string> login(string username, string password)
        {
            var response = await service.loginAsync(new loginRequest(new loginRequestBody()
            {
                username = username,
                password = password
            }));
            return response.Body.loginResult;
        }

        private async void addToQuestions()
        {
            var response = await service.getQuestionsAsync(new getQuestionsRequest(new getQuestionsRequestBody()));
            List<string> tempList = response.Body.getQuestionsResult;

            foreach (string question in tempList)
            {
                string[] tempArray = question.Split(' ');
                questions.Add(new Question(Convert.ToInt32(tempArray[0]), tempArray[1],
                                    tempArray[2], Convert.ToInt32(tempArray[3])));
            }
        }

        private async void addToWorkshops()
        {
            var response = await service.getWorkshopsAsync(new getWorkshopsRequest(new getWorkshopsRequestBody()));
            List<string> tempList = response.Body.getWorkshopsResult;

            foreach(string workshop in tempList)
            {
                string[] tempArray = workshop.Split(' ');
                workshops.Add(new Workshops(tempArray[0], tempArray[1], Convert.ToDateTime(tempArray[2]),
                                    tempArray[3], tempArray[4]));
            }
        }

        private async void addToAccounts()
        {

            var response = await service.GetUsersAsync(new GetUsersRequest(new GetUsersRequestBody()));
            List<string> tempList = response.Body.GetUsersResult;

            foreach (string user in tempList)
            {
                string[] tempArray = user.Split(' ');
                accounts.Add(new Account(Convert.ToInt32(tempArray[0]), tempArray[1],
                    tempArray[2], tempArray[3], tempArray[4], tempArray[5]));
            }
        }

        public List<Account> getAccounts() { return accounts; }
        public List<Question> getQuestions() { return questions; }

        public async Task<char> add(Account account)
        {
            accounts.Add(account);
            await service.addUserAsync(new addUserRequest(new addUserRequestBody()
            {
                id = account.getId(),
                username = account.username,
                password = account.password,
                firstName = account.firstName,
                lastName = account.lastName,
                email = account.email
            }));
            return ' ';
        }

        public async void update(string finduser, Account account)
        {
            await service.updateUserAsync(new updateUserRequest(new updateUserRequestBody()
            {
                findUsername = finduser,
                username = account.username,
                password = account.password,
                firstName = account.firstName,
                lastName = account.lastName,
                email = account.email
            }));
            accounts.Clear();
            addToAccounts();
        }

        public async void delete(Account account)
        {
            await service.deleteUserAsync(new deleteUserRequest(new deleteUserRequestBody()
            {
                username = account.username
            }));
            accounts.Remove(account);
        }

        public async void addQuestion(Question question)
        {
            questions.Add(question);
            await service.addQuestionAsync(new addQuestionRequest(new addQuestionRequestBody()
            {
                id = question.id,
                question = question.name,
                username = question.username,
                qid = question.questionId
            }));
        }

        public async void deleteQuestion(Question question)
        {
            await service.deleteQuestionAsync(new deleteQuestionRequest()
            {
                qid = question.questionId
            });
            questions.Remove(question);
        }

        public async void updateQuestion(Question question)
        {
            await service.updateQuestionAsync(new updateQuestionRequest(new updateQuestionRequestBody()
            {
                id = question.id,
                question = question.name,
                user = question.username
            }));
            questions.Clear();
            addToQuestions();
        }

        public async Task<bool> addAnswer(Answer answer)
        {
            bool ok = false;
            foreach (Question question in questions)
            {
                if (question.questionId == answer.questionId)
                {
                    questions.Remove(question);
                    question.addToAnswerList(answer);
                    questions.Add(question);
                    ok = true;
                }
            }

            if (ok)
            {
                await service.addAnswerAsync(new addAnswerRequest(new addAnswerRequestBody()
                {
                    id = answer.id,
                    answer = answer.name,
                    user = answer.username,
                    qid = answer.questionId
                }));
                return ok;
            }
            return ok;
        }

        public async void deleteAnswer(Answer answer)
        {
            foreach(Question question in questions)
                if(question.questionId==answer.questionId)
                    foreach(Answer answer1 in question.getAnswerList())
                        if(answer.id==answer1.id) question.getAnswerList().Remove(answer1);


            await service.deleteAnswerAsync(new deleteAnswerRequest()
            {
                id = answer.id
            });
        }

        public async void updateAnswer(Answer answer)
        {
            await service.updateAnswerAsync(new updateAnswerRequest(new updateAnswerRequestBody()
            {
                id = answer.id,
            }));
        }

        public async void addWorkshop(Workshops workshop)
        {
            workshops.Add(workshop);
            await service.addWorkshopAsync(new addWorkshopRequest(new addWorkshopRequestBody()
            {
                name=workshop.name,
                speakerName=workshop.speakerName,
                date=workshop.date,
                description=workshop.description,
                imagePath=workshop.imagePath
            }));
        }
    }
}
