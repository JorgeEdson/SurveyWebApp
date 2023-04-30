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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FirstQuestion"] == null)
            {
                Question actualQuestion = GetQuestionById(1);
                Session["ActualQuestion"] = actualQuestion;
                RenderQuestion(actualQuestion);
                ButtonPrevious.Visible = false;
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

            if (stackQuestions.Count == 0)
            {
                ButtonPrevious.Visible = false;
            }
            else 
            {
                ButtonPrevious.Visible = true;
            }
        }

        private void RenderQuestion(Question paramQuestion) 
        {
            QuestionText.Text = paramQuestion.Text;
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
                            Text = op.Text,
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
                            Text = op.Text,
                            Value = op.Text
                        };
                        CheckBoxListForRender.Items.Add(item);
                    });                    
                    break;                
                default:
                    throw new Exception("Not knowed question type");
            }
        }        
        private void SetActualQuestion(Question paramQuestion) 
        {
            Session["ActualQuestion"] = paramQuestion;
        }
        private Question GetQuestionById(int paramId)
        {
            Question returnQuestion = new Question();
            returnQuestion = DatabaseOperationsService.GetQuestionById(paramId);                
            return returnQuestion;
        }
        protected void ButtonPrevious_Click(object sender, EventArgs e)
        {
            Question actualQuestion = stackQuestions.Pop();
            Session["ActualQuestion"] = actualQuestion;
            listAnswers.RemoveAll(x => x.QuestionId == actualQuestion.Id);
            RenderQuestion(actualQuestion);
            LabelQuantidadeAnswers.Text = "Quantidade de respostas = " + listAnswers.Count();
        }
        protected void ButtonNext_Click(object sender, EventArgs e)
        {            
            Question actualQuestion = (Question)Session["ActualQuestion"];
            stackQuestions.Push(actualQuestion);            
            GetAnswerQuestion();
            Question nextQuestion = GetQuestionById((int)actualQuestion.NextId);
            Session["ActualQuestion"] = nextQuestion;
            RenderQuestion(nextQuestion);
        }

        protected void ButtonSaveAnswers_Click(object sender, EventArgs e)
        {
            var answersToSave = (List<Answer>)Session["ListAnswers"];
        }
        private void GetAnswerQuestion() 
        {
            Question question = stackQuestions.Peek();
            if (TextBoxForRender.Visible) 
            {
                Answer answer = new Answer
                {
                    QuestionId = question.Id,
                    Text = TextBoxForRender.Text,
                };
                listAnswers.Add(answer);
            }
            if (RadioButtonListForRender.Visible)
            {
                foreach (ListItem item in RadioButtonListForRender.Items)
                {
                    if (item.Selected)
                    {
                        Answer answer = new Answer
                        {
                            Text = item.Text,
                            QuestionId = question.Id
                        };                        
                        listAnswers.Add(answer);
                    }
                }
            }
            if (CheckBoxListForRender.Visible)
            {
                foreach (ListItem item in CheckBoxListForRender.Items)
                {
                    if (item.Selected)
                    {
                        Answer answer = new Answer
                        {
                            Text = item.Text,
                            QuestionId = question.Id
                        };                        
                        listAnswers.Add(answer);
                    }
                }
            }
            LabelQuantidadeAnswers.Text = "Quantidade de respostas = " + listAnswers.Count();
        }
    }
}