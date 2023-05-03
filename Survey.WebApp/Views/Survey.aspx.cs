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
        private static bool changeFlow;        
        private static bool changeToQuestion12;
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
                            Text = " "+op.Text,
                            Value = op.Text
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
                            Value = op.Text
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
            if (stackQuestions.Count != 0 && listAnswers.Count != 0) 
            {
                Question actualQuestion = stackQuestions.Pop();
                Session["ActualQuestion"] = actualQuestion;
                listAnswers.RemoveAll(x => x.QuestionId == actualQuestion.Id);
                RenderQuestion(actualQuestion);
                LabelQuantidadeAnswers.Text = "Quantidade de respostas = " + listAnswers.Count();
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
                }
                else 
                {
                    if (hasOnlySport) 
                    {
                        nextQuestion = GetQuestionById(11);
                        Session["ActualQuestion"] = nextQuestion;
                        RenderQuestion(nextQuestion);                        
                    }
                    if (hasOnlyTravel) 
                    {
                        nextQuestion = GetQuestionById(12);
                        Session["ActualQuestion"] = nextQuestion;
                        RenderQuestion(nextQuestion);
                    }
                    if (hasSportAndTravel)
                    {
                        nextQuestion = GetQuestionById(11);
                        Session["ActualQuestion"] = nextQuestion;
                        RenderQuestion(nextQuestion);                        
                    }
                    if (changeToQuestion12) 
                    {
                        nextQuestion = GetQuestionById(11);
                        Session["ActualQuestion"] = nextQuestion;
                        RenderQuestion(nextQuestion);
                    }
                }
                changeFlow = false;
            }            
        }
        protected void ButtonSaveAnswers_Click(object sender, EventArgs e)
        {
            var answersToSave = (List<Answer>)Session["ListAnswers"];
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
                foreach (ListItem item in RadioButtonListForRender.Items)
                {
                    if (item.Selected)
                    {
                        Answer answer = new Answer
                        {
                            Text = item.Text,
                            QuestionId = question.Id,
                            RespondentId = (int)Session["RespondentId"]
                        };                        
                        listAnswers.Add(answer);                        
                    }
                }
                canNext = true;
            }
            if (question.Id == 7)
            {
                if (CheckBoxListForRender.Visible && CheckBoxListForRender.Items.Count != 0)
                {
                    List<Answer> temporaryListAnswers = new List<Answer>();
                    foreach (ListItem item in CheckBoxListForRender.Items)
                    {
                        if (item.Selected)
                        {
                            Answer answer = new Answer
                            {
                                Text = item.Text,
                                QuestionId = question.Id,
                                RespondentId = (int)Session["RespondentId"]
                            };
                            temporaryListAnswers.Add(answer);
                        }
                    }
                    if (temporaryListAnswers.Count <= 4)
                    {
                        foreach (Answer answer in temporaryListAnswers)
                        {
                            listAnswers.Add(answer);
                        }
                        canNext = true;
                    }
                    else
                    {
                        temporaryListAnswers.Clear();
                        LabelWarning.Text = "Choose a maximum of 4";
                        canNext = false;
                    }
                }
            }
            else if (question.Id == 9)
            {
                if (CheckBoxListForRender.Visible && CheckBoxListForRender.Items.Count != 0)
                {
                    List<Answer> temporaryListAnswers = new List<Answer>();
                    foreach (ListItem item in CheckBoxListForRender.Items)
                    {
                        if (item.Selected)
                        {
                            Answer answer = new Answer
                            {
                                Text = item.Text,
                                QuestionId = question.Id,
                                RespondentId = (int)Session["RespondentId"]
                            };
                            temporaryListAnswers.Add(answer);
                        }
                    }
                    if (temporaryListAnswers.Count <= 2)
                    {
                        bool hasNone = HasNone(temporaryListAnswers);
                        if (hasNone)
                        {
                            foreach (Answer answer in temporaryListAnswers)
                            {
                                listAnswers.Add(answer);
                            }
                            changeFlow = true;
                            Question nextQuestion = GetQuestionById(13);
                            Session["ActualQuestion"] = nextQuestion;
                            RenderQuestion(nextQuestion);
                        }
                        else
                        {
                            foreach (Answer answer in temporaryListAnswers)
                            {
                                listAnswers.Add(answer);
                            }
                        }
                        canNext = true;
                    }
                    else
                    {
                        temporaryListAnswers.Clear();
                        LabelWarning.Text = "Choose a maximum of 2";
                        canNext = false;
                    }
                }
            }
            else if (question.Id == 10)
            {
                if (CheckBoxListForRender.Visible && CheckBoxListForRender.Items.Count != 0)
                {
                    List<Answer> temporaryListAnswers = new List<Answer>();
                    foreach (ListItem item in CheckBoxListForRender.Items)
                    {
                        if (item.Selected)
                        {
                            Answer answer = new Answer
                            {
                                Text = item.Text,
                                QuestionId = question.Id,
                                RespondentId = (int)Session["RespondentId"]
                            };
                            temporaryListAnswers.Add(answer);
                        }
                    }
                    bool hasSport = HasSport(temporaryListAnswers);
                    bool hasTravel = HasTravel(temporaryListAnswers);
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
                    foreach (Answer answer in temporaryListAnswers)
                    {
                        listAnswers.Add(answer);
                    }
                    canNext = true;
                }
            }
            else if (question.Id == 11)
            {
                if (CheckBoxListForRender.Visible && CheckBoxListForRender.Items.Count != 0)
                {
                    foreach (ListItem item in CheckBoxListForRender.Items)
                    {
                        if (item.Selected)
                        {
                            Answer answer = new Answer
                            {
                                Text = item.Text,
                                QuestionId = question.Id,
                                RespondentId = (int)Session["RespondentId"]
                            };
                            listAnswers.Add(answer);
                        }
                    }
                }
                if (hasSportAndTravel) 
                {
                    changeFlow = true;
                    changeToQuestion12 = true;
                    hasSportAndTravel = false;
                }                
                canNext = true;
            }
            else if (question.Id == 12) 
            {
                if (CheckBoxListForRender.Visible && CheckBoxListForRender.Items.Count != 0) 
                {
                    List<Answer> temporaryListAnswers = new List<Answer>();
                    foreach (ListItem item in CheckBoxListForRender.Items)
                    {
                        if (item.Selected)
                        {
                            Answer answer = new Answer
                            {
                                Text = item.Text,
                                QuestionId = question.Id,
                                RespondentId = (int)Session["RespondentId"]
                            };
                            temporaryListAnswers.Add(answer);
                        }
                    }
                    if (temporaryListAnswers.Count >= 2)
                    {
                        foreach (Answer answer in temporaryListAnswers)
                        {
                            listAnswers.Add(answer);
                        }                        
                        canNext = true;
                    }
                    else 
                    {
                        temporaryListAnswers.Clear();
                        LabelWarning.Text = "Choose at least 2";
                        canNext = false;
                    }                        
                }                
            }
            else
            {
                if (CheckBoxListForRender.Visible && CheckBoxListForRender.Items.Count != 0)
                {
                    foreach (ListItem item in CheckBoxListForRender.Items)
                    {
                        if (item.Selected)
                        {
                            Answer answer = new Answer
                            {
                                Text = item.Text,
                                QuestionId = question.Id,
                                RespondentId = (int)Session["RespondentId"]
                            };
                            listAnswers.Add(answer);
                        }
                    }
                }
                canNext = true;
            }            
            LabelQuantidadeAnswers.Text = "Quantidade de respostas = " + listAnswers.Count();
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
    }
}