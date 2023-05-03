using Survey.WebApp.Domain;
using Survey.WebApp.Domain.Enuns;
using Survey.WebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Survey.WebApp.Views
{
    public partial class Survey : System.Web.UI.Page
    {
        private static List<Answer> listAnswers = new List<Answer>();
        private static Stack<Question> stackQuestions = new Stack<Question>();
        private static bool lastQuestion;
        private static bool changeFlow;        
        private static bool changeToQuestion12;
        private static bool changeToQuestion13;
        private static bool hasSportAndTravel;
        private static bool hasOnlySport;
        private static bool hasOnlyTravel;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FirstQuestion"] == null)
            {
                Question actualQuestion = GetQuestionById(1);
                Session["ActualQuestion"] = actualQuestion;
                RenderQuestion(actualQuestion);                
                Session["FirstQuestion"] = false;
            }
            else if ((bool)Session["FirstQuestion"] == false)
            {                
                //RenderQuestion((Question)Session["ActualQuestion"]);
            }
            else 
            {
                Session["FirstQuestion"] = false;
            }            
        }
        private void RenderQuestion(Question paramQuestion) 
        {
            QuestionText.Text = paramQuestion.Id+" - "+ paramQuestion.Text;
            LabelWarning.Text = String.Empty;
            RenderQuestionType(paramQuestion);                   
        }
        /// <summary>
        ///  aqui vai um resumo das operações que o metodo faz (em ingles)
        /// </summary>
        /// <param name="paramQuestion"></param>
        /// <exception cref="Exception"></exception>
        private void RenderQuestionType(Question paramQuestion)
        {
            switch (paramQuestion.Type)
            {
                case QuestionType.Text:
                    TextBoxForRender.Visible = true;
                    RadioButtonListForRender.Visible = false;
                    CheckBoxListForRender.Visible = false;
                    TextBoxForRender.Text = "";
                    break;
                case QuestionType.Radio:
                    TextBoxForRender.Visible = false;
                    RadioButtonListForRender.Visible = true;
                    CheckBoxListForRender.Visible = false;
                    RadioButtonListForRender.Items.Clear();
                    paramQuestion.Options.ForEach(op => {
                        var item = new ListItem
                        {
                            Text = " " + op.Text,
                            Value = op.Text.Trim()
                        };
                        
                        RadioButtonListForRender.Items.Add(item);
                    });                    
                    break;
                case QuestionType.Checkbox:
                    TextBoxForRender.Visible = false;
                    RadioButtonListForRender.Visible = false;
                    CheckBoxListForRender.Visible = true;
                    CheckBoxListForRender.Items.Clear();
                    paramQuestion.Options.ForEach(op => {
                        var item = new ListItem
                        {
                            Text = " "+op.Text,
                            Value = op.Text.Trim()
                        };
                        CheckBoxListForRender.Items.Add(item);
                    });                    
                    break;                
                default:
                    throw new Exception("Not knowed question type");
            }
        }                
        private Question GetQuestionById(int paramId)
        {
            Question returnQuestion = new Question();
            returnQuestion = DatabaseOperationsService.GetQuestionById(paramId);                
            return returnQuestion;
        }
        protected void ButtonPrevious_Click(object sender, EventArgs e)
        {
            if (stackQuestions.Count()>=1) 
            {
                Question actualQuestion = stackQuestions.Pop();
                Session["ActualQuestion"] = actualQuestion;
                if(listAnswers.Exists(x => x.QuestionId == actualQuestion.Id))
                    listAnswers.RemoveAll(x => x.QuestionId == actualQuestion.Id);
                RenderQuestion(actualQuestion);
            }
        }
        protected void ButtonSkip_Click(object sender, EventArgs e)
        {
            Question actualQuestion = (Question)Session["ActualQuestion"];
            Question nextQuestion;
            if (actualQuestion.Required == 0)
            {
                LabelWarning.Text = "This Question is Required!";
            }
            else 
            {
                stackQuestions.Push(actualQuestion);                
                nextQuestion = GetQuestionById((int)actualQuestion.NextId);
                Session["ActualQuestion"] = nextQuestion;
                RenderQuestion(nextQuestion);
            }
        }
        protected void ButtonNext_Click(object sender, EventArgs e)
        {
            Question actualQuestion = (Question)Session["ActualQuestion"];
            Question nextQuestion;
            if (actualQuestion.Required == 0)
            {
                bool canContinue1, canContinue2, canContinue3 = true;
                var textBoxForRender = TextBoxForRender.Text;
                canContinue1 = textBoxForRender == string.Empty ? false : true;
                var radioButtonListForRender = RadioButtonListForRender.SelectedValue;
                canContinue2 = radioButtonListForRender == string.Empty ? false : true;
                var checkBoxListForRender = CheckBoxListForRender.Items;
                canContinue3 = checkBoxListForRender.Count == 0 ? false : true;
                if (canContinue1 || canContinue2 || canContinue3)
                {
                    stackQuestions.Push(actualQuestion);
                    bool canNext = GetAnswerQuestion();
                    if (canNext)
                    {
                        nextQuestion = GetQuestionById((int)actualQuestion.NextId);
                        Session["ActualQuestion"] = nextQuestion;
                        RenderQuestion(nextQuestion);
                    }
                }
                else
                    LabelWarning.Text = "This Question is Required!";
            }
            else
            {
                stackQuestions.Push(actualQuestion);
                bool canNext = GetAnswerQuestion();
                if (!changeFlow)
                {
                    if (canNext)
                    {
                        nextQuestion = GetQuestionById((int)actualQuestion.NextId);
                        Session["ActualQuestion"] = nextQuestion;
                        RenderQuestion(nextQuestion);
                    }
                    else 
                    {
                        ButtonPrevious.Visible = false;
                        ButtonNext.Visible = false;
                        ButtonSkip.Visible = false;
                    }
                }
                else
                {
                    if (hasOnlySport)
                    {
                        hasOnlySport = false;
                        nextQuestion = GetQuestionById(11);
                        Session["ActualQuestion"] = nextQuestion;
                        RenderQuestion(nextQuestion);
                    }
                    if (hasOnlyTravel)
                    {
                        hasOnlyTravel = false;
                        nextQuestion = GetQuestionById(12);
                        Session["ActualQuestion"] = nextQuestion;
                        RenderQuestion(nextQuestion);
                    }
                    if (hasSportAndTravel)
                    {
                        hasSportAndTravel = false;
                        nextQuestion = GetQuestionById(11);
                        Session["ActualQuestion"] = nextQuestion;
                        RenderQuestion(nextQuestion);
                    }
                    if (changeToQuestion12)
                    {
                        changeToQuestion12 = false;
                        nextQuestion = GetQuestionById(11);
                        Session["ActualQuestion"] = nextQuestion;
                        RenderQuestion(nextQuestion);
                    }
                    if (changeToQuestion13)
                    {
                        QuestionTitle.Visible = false;
                        changeToQuestion13 = false;
                        nextQuestion = GetQuestionById(13);
                        Session["ActualQuestion"] = nextQuestion;
                        RenderQuestion(nextQuestion);
                    }
                }
                changeFlow = false;
            }
        }
      
        private bool GetAnswerQuestion() 
        {
            bool canNext = true;
            Question question = stackQuestions.Peek();           
            if (TextBoxForRender.Visible && TextBoxForRender.Text != string.Empty) 
            {
                Answer answer = new Answer
                {
                    QuestionId = question.Id,
                    Text = TextBoxForRender.Text,
                    RespondentId = (int)Session["RespondentId"]
                };
                listAnswers.Add(answer);
                canNext = true;
            }
            if (RadioButtonListForRender.Visible && RadioButtonListForRender.SelectedValue != string.Empty)
            {
                switch (question.Id) 
                {
                    case 13:
                        foreach (ListItem item in RadioButtonListForRender.Items)
                        {
                            if (item.Selected)
                            {
                                Answer answer = new Answer
                                {
                                    Text = item.Value,
                                    QuestionId = question.Id,
                                    RespondentId = (int)Session["RespondentId"]
                                };
                                listAnswers.Add(answer);
                                if (answer.Text == "yes") 
                                {
                                    QuestionArea.Visible = false;
                                    RegisterForm.Visible = true;                                                                      
                                }
                            }
                        }
                        lastQuestion = true;
                        QuestionTitle.Visible = false;
                        canNext = false;
                        break;
                    default:
                        foreach (ListItem item in RadioButtonListForRender.Items)
                        {
                            if (item.Selected)
                            {
                                Answer answer = new Answer
                                {
                                    Text = item.Value,
                                    QuestionId = question.Id,
                                    RespondentId = (int)Session["RespondentId"]
                                };
                                listAnswers.Add(answer);
                            }
                        }
                        canNext = true;
                        break;
                }
                
            }
            if (CheckBoxListForRender.Visible && CheckBoxListForRender.Items.Count != 0) 
            {                
                switch (question.Id) 
                {
                    case 7:
                        List<Answer> temporaryListAnswersCase7 = new List<Answer>();
                        foreach (ListItem item in CheckBoxListForRender.Items)
                        {
                            if (item.Selected)
                            {
                                Answer answer = new Answer
                                {
                                    Text = item.Value,
                                    QuestionId = question.Id,
                                    RespondentId = (int)Session["RespondentId"]
                                };
                                temporaryListAnswersCase7.Add(answer);
                            }
                        }
                        if (temporaryListAnswersCase7.Count <= 4)
                        {
                            foreach (Answer answer in temporaryListAnswersCase7)
                            {
                                listAnswers.Add(answer);
                            }
                            canNext = true;
                        }
                        else
                        {
                            temporaryListAnswersCase7.Clear();
                            LabelWarning.Text = "Choose a maximum of 4";
                            canNext = false;
                        }
                        break;
                    case 9:
                        List<Answer> temporaryListAnswersCase9 = new List<Answer>();
                        foreach (ListItem item in CheckBoxListForRender.Items)
                        {
                            if (item.Selected)
                            {
                                Answer answer = new Answer
                                {
                                    Text = item.Value,
                                    QuestionId = question.Id,
                                    RespondentId = (int)Session["RespondentId"]
                                };
                                temporaryListAnswersCase9.Add(answer);
                            }
                        }
                        if (temporaryListAnswersCase9.Count <= 2)
                        {
                            bool hasNone = HasNone(temporaryListAnswersCase9);
                            if (hasNone)
                            {
                                foreach (Answer answer in temporaryListAnswersCase9)
                                {
                                    listAnswers.Add(answer);
                                }
                                changeFlow = true;
                                changeToQuestion13 = true;                                
                            }
                            else
                            {
                                foreach (Answer answer in temporaryListAnswersCase9)
                                {
                                    listAnswers.Add(answer);
                                }
                            }
                            canNext = true;
                        }
                        else
                        {
                            temporaryListAnswersCase9.Clear();
                            LabelWarning.Text = "Choose a maximum of 2";
                            canNext = false;
                        }
                        break;
                    case 10:
                        List<Answer> temporaryListAnswersCase10 = new List<Answer>();
                        foreach (ListItem item in CheckBoxListForRender.Items)
                        {
                            if (item.Selected)
                            {
                                Answer answer = new Answer
                                {
                                    Text = item.Value,
                                    QuestionId = question.Id,
                                    RespondentId = (int)Session["RespondentId"]
                                };
                                temporaryListAnswersCase10.Add(answer);
                            }
                        }
                        bool hasSport = HasSport(temporaryListAnswersCase10);
                        bool hasTravel = HasTravel(temporaryListAnswersCase10);
                        if (hasSport && hasTravel)
                        {
                            changeFlow = true;
                            hasSportAndTravel = true;
                        }
                        else if (hasTravel && !hasSport)
                        {
                            changeFlow = true;
                            hasOnlyTravel = true;
                        }
                        else if (hasSport && !hasTravel)
                        {
                            changeFlow = true;
                            hasOnlySport = true;
                        }
                        foreach (Answer answer in temporaryListAnswersCase10)
                        {
                            listAnswers.Add(answer);
                        }
                        canNext = true;
                        break;
                    case 11:
                        foreach (ListItem item in CheckBoxListForRender.Items)
                        {
                            if (item.Selected)
                            {
                                Answer answer = new Answer
                                {
                                    Text = item.Value,
                                    QuestionId = question.Id,
                                    RespondentId = (int)Session["RespondentId"]
                                };
                                listAnswers.Add(answer);
                            }
                        }
                        if (hasSportAndTravel)
                        {
                            changeFlow = true;
                            changeToQuestion12 = true;
                            hasSportAndTravel = false;
                        }
                        else if (hasOnlySport) 
                        { 
                        
                        }
                        canNext = true;
                        break;
                    case 12:
                        List<Answer> temporaryListAnswersCase12 = new List<Answer>();
                        foreach (ListItem item in CheckBoxListForRender.Items)
                        {
                            if (item.Selected)
                            {
                                Answer answer = new Answer
                                {
                                    Text = item.Value,
                                    QuestionId = question.Id,
                                    RespondentId = (int)Session["RespondentId"]
                                };
                                temporaryListAnswersCase12.Add(answer);
                            }
                        }
                        if (temporaryListAnswersCase12.Count >= 2)
                        {
                            foreach (Answer answer in temporaryListAnswersCase12)
                            {
                                listAnswers.Add(answer);
                            }
                            canNext = true;
                        }
                        else
                        {
                            temporaryListAnswersCase12.Clear();
                            LabelWarning.Text = "Choose at least 2";
                            canNext = false;
                        }
                        break;
                    default:
                        foreach (ListItem item in CheckBoxListForRender.Items)
                        {
                            if (item.Selected)
                            {
                                Answer answer = new Answer
                                {
                                    Text = item.Value,
                                    QuestionId = question.Id,
                                    RespondentId = (int)Session["RespondentId"]
                                };
                                listAnswers.Add(answer);
                            }
                        }
                        break;
                }
            }                                    
            return canNext;
        }
        private bool HasNone(List<Answer> paramListAnswers) 
        {
            bool hasNone = false;
            foreach (Answer answer in paramListAnswers) 
            {
                if (answer.Text.Trim() == "None") 
                {
                    hasNone = true;
                }
            }
            return hasNone;
        }
        private bool HasSport(List<Answer> paramListAnswers)
        {
            bool hasSport = false;
            foreach (Answer answer in paramListAnswers)
            {
                if (answer.Text.Trim() == "Sport")
                {
                    hasSport = true;
                }
            }
            return hasSport;
        }
        private bool HasTravel(List<Answer> paramListAnswers)
        {
            bool hasTravel = false;
            foreach (Answer answer in paramListAnswers)
            {
                if (answer.Text.Trim() == "Travel")
                {
                    hasTravel = true;
                }
            }
            return hasTravel;
        }

        protected void ButtonSaveAnswers_Click(object sender, EventArgs e)
        {
            SaveAnswers(listAnswers);
        }
        protected void ButtonSaveAnswersRegisterForm_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Id = DatabaseOperationsService.IncrementId("registers");
            register.GivenName = TextBoxGivenName.Text != String.Empty ? TextBoxGivenName.Text : null;
            register.LastName = TextBoxLastName.Text != String.Empty ? TextBoxLastName.Text : null;
            register.DateBirth = TextBoxDateBirth.Text != String.Empty ? TextBoxDateBirth.Text : null;
            register.PhoneNumber = TextBoxPhoneNumber.Text != String.Empty ? TextBoxPhoneNumber.Text : null;
            register.RespondentId = (int)Session["RespondentId"];
            DatabaseOperationsService.AddRegister(register);
            SaveAnswers(listAnswers);            
        }
        private void SaveAnswers(List<Answer> paramListAnswers) 
        {
            DatabaseOperationsService.SaveAnswers(paramListAnswers);
            Response.Redirect("/Default.aspx");
        }
    }
}