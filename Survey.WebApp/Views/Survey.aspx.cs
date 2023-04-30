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
        private void PushQuestion(Question paramQuestion) 
        {
            if (Session["StackQuestions"] == null)
            {
                Stack<Question> stackQuestion = new Stack<Question>();
                stackQuestion.Push(paramQuestion);
                Session["StackQuestions"] = stackQuestion;
            }
            else 
            {
                Stack<Question> stackQuestion = (Stack<Question>)Session["StackQuestions"];
                stackQuestion.Push(paramQuestion);
                Session["StackQuestions"] = stackQuestion;
            }            
        }
        private Question PeekQuestion()
        {
            if (Session["StackQuestions"] == null)
            {
                throw new Exception();                
            }
            else
            {
                Stack<Question> stackQuestion = (Stack<Question>)Session["StackQuestions"];
                return stackQuestion.Peek();                
            }
        }
        private void PopQuestion() 
        {
            if (Session["StackQuestions"] == null)
            {
                throw new Exception();
            }
            else
            {
                Stack<Question> stackQuestion = (Stack<Question>)Session["StackQuestions"];
                stackQuestion.Pop();
            }
        }
        protected void ButtonPrevious_Click(object sender, EventArgs e)
        {

        }
        protected void ButtonNext_Click(object sender, EventArgs e)
        {            
            Question actualQuestion = (Question)Session["ActualQuestion"];
            PushQuestion(actualQuestion);
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
            Question question = PeekQuestion();
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