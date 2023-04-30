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
        //static Stack<Question> _stackQuestions = 
        //static List<Answer> _listAnswer =         

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FirstQuestion"] == null)
            {
                Question actualQuestion = GetQuestionById(1);
                Session["ActualQuestion"] = actualQuestion;
                RenderQuestion(actualQuestion);
            }
            else 
            {
                RenderQuestion((Question)Session["ActualQuestion"]);
            }
            Session["FirstQuestion"] = false;
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
                    TextBox textbox = new TextBox
                    {
                        ID = "textbox"                        
                    };
                    QuestionOptionsPlaceHolder.Controls.Clear();
                    QuestionOptionsPlaceHolder.Controls.Add(textbox);
                    break;
                case QuestionType.Radio:
                    var radioBtnList = new RadioButtonList
                    {
                        ID = "radiobuttonList"
                    };
                    paramQuestion.Options.ForEach(op => {
                        var item = new ListItem
                        {
                            Text = op.Text,
                            Value = op.Text
                        };
                        radioBtnList.Items.Add(item);
                    });
                    QuestionOptionsPlaceHolder.Controls.Clear();
                    QuestionOptionsPlaceHolder.Controls.Add(radioBtnList);
                    break;
                case QuestionType.Checkbox:
                    var checkBoxList = new CheckBoxList
                    {
                        ID = "checkboxList"
                    };
                    paramQuestion.Options.ForEach(op => {
                        var item = new ListItem
                        {
                            Text = op.Text,
                            Value = op.Text
                        };
                        checkBoxList.Items.Add(item);
                    });
                    QuestionOptionsPlaceHolder.Controls.Clear();
                    QuestionOptionsPlaceHolder.Controls.Add(checkBoxList);
                    break;
                case QuestionType.Dropdown:
                    var dropdownList = new DropDownList
                    {
                        ID = "dropdownList"
                    };
                    paramQuestion.Options.ForEach(op => {
                        var item = new ListItem
                        {
                            Text = op.Text,
                            Value = op.Text
                        };
                        dropdownList.Items.Add(item);
                    });
                    QuestionOptionsPlaceHolder.Controls.Clear();
                    QuestionOptionsPlaceHolder.Controls.Add(dropdownList);
                    break;
                default:
                    throw new Exception("Not knowed question type");
            }
        }
        private void GetAnswerQuestion()
        {
            List<Answer> listAnswers = new List<Answer>();
            if (Session["ListAnswers"] == null) 
            {
                Session["ListAnswers"] = new List<Answer>();
            }
            else 
            {
                listAnswers = (List<Answer>)Session["ListAnswers"];                
            }

            TextBox textBox = (TextBox)QuestionOptionsPlaceHolder.FindControl("textbox");
            CheckBoxList checkBoxList = (CheckBoxList)QuestionOptionsPlaceHolder.FindControl("checkboxList");
            RadioButtonList radioButtonList = (RadioButtonList)QuestionOptionsPlaceHolder.FindControl("radiobuttonList");
            DropDownList dropDownList = (DropDownList)QuestionOptionsPlaceHolder.FindControl("dropdownList");

            Question question = PeekQuestion();

            if (textBox != null)
            {
                Answer answer = new Answer
                {
                    QuestionId = question.Id,                    
                    Text = textBox.Text,
                    RespondentId = Session[]
                };
                listAnswers.Add(answer);
            }
            if (checkBoxList != null)
            {
                foreach (ListItem item in checkBoxList.Items)
                {
                    if (item.Selected)
                    {
                        Answer answer = new Answer
                        {
                            Text = item.Text,
                            QuestionId = question.Id
                        };
                        //if (checkBoxList.Attributes["next_question_id"] != null)
                        //{
                        //    if (!(nextQuestions.Contains(int.Parse(checkBoxList.Attributes["next_question_id"]))))
                        //    {
                        //        nextQuestions.Push(int.Parse(checkBoxList.Attributes["next_question_id"]));
                        //    }
                        //}
                        listAnswers.Add(answer);
                    }
                }
            }
            if (radioButtonList != null)
            {
                foreach (ListItem item in radioButtonList.Items)
                {
                    if (item.Selected)
                    {
                        Answer answer = new Answer
                        {
                            Text = item.Text,
                            QuestionId = question.Id
                        };

                        //if (radioButtonList.Attributes["next_question_id"] != null)
                        //{
                        //    if (!(nextQuestions.Contains(int.Parse(radioButtonList.Attributes["next_question_id"]))))
                        //    {
                        //        nextQuestions.Push(int.Parse(radioButtonList.Attributes["next_question_id"]));
                        //    }
                        //}
                        listAnswers.Add(answer);
                    }
                }
            }
            if (dropDownList != null)
            {
                foreach (ListItem item in dropDownList.Items)
                {
                    if (item.Selected)
                    {
                        Answer answer = new Answer
                        {
                            Text = item.Text,
                            QuestionId = question.Id
                        };

                        //if (dropDownList.Attributes["next_question_id"] != null)
                        //{
                        //    if (!(nextQuestions.Contains(int.Parse(dropDownList.Attributes["next_question_id"]))))
                        //    {
                        //        nextQuestions.Push(int.Parse(radioButtonList.Attributes["next_question_id"]));
                        //    }
                        //}
                        listAnswers.Add(answer);
                    }
                }
            }

            LabelQuantidadeAnswers.Text = "Quantidade de respostas = " + listAnswers.Count();
            Session["ListAnswers"] = listAnswers;
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
            Session["ActualQuestion"] = GetQuestionById((int)actualQuestion.NextId);
            



            //if (_actualQuestion.NextId == null) 
            //{
            //    ButtonSaveAnswers.Visible = true;
            //    ButtonNext.Visible = false;
            //}
            //else 
            //{
            //    var nextQuestion = GetNextQuestion((int)_actualQuestion.NextId);
            //    Session["NextQuestionId"] = nextQuestion.NextId;
            //    RenderQuestion(nextQuestion);
            //}            
        }

        protected void ButtonSaveAnswers_Click(object sender, EventArgs e)
        {
            var answersToSave = (List<Answer>)Session["ListAnswers"];
        }

        
    }
}