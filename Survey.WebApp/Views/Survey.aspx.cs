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
        Stack<Question> _stackQuestions = new Stack<Question>();
        List<Answer> _listAnswer = new List<Answer>();
        Respondent respondent = new Respondent();

        protected void Page_Load(object sender, EventArgs e)
        {
            var question = DatabaseOperationsService.GetQuestionById(1);
            QuestionText.Text = question.Text;
            QuestionTypeSelector(question);
            _stackQuestions.Push(question);
            LabelTestePilha.Text = "Quantidade na pilha: "+_stackQuestions.Count.ToString();
        }

        private void QuestionTypeSelector(Question question)
        {
            switch (question.Type)
            {
                case QuestionType.Text:
                    var textbox = new TextBox
                    {
                        ID = "textbox",
                        Text = question.Text
                    };
                    QuestionOptionsPlaceHolder.Controls.Add(textbox);
                    break;
                case QuestionType.Radio:
                    var radioBtnList = new RadioButtonList
                    {
                        ID = "radiobuttonList"
                    };
                    question.Options.ForEach(op => {
                        var item = new ListItem
                        {
                            Text = op.Text,
                            Value = op.Text
                        };

                        if (question.HasNext)
                            item.Attributes["next_question_id"] = question.NextId + ""; ;

                        radioBtnList.Items.Add(item);
                    });
                    QuestionOptionsPlaceHolder.Controls.Add(radioBtnList);
                    break;
                case QuestionType.Checkbox:
                    var checkBoxList = new CheckBoxList
                    {
                        ID = "checkboxList"
                    };
                    question.Options.ForEach(op => {
                        var item = new ListItem
                        {
                            Text = op.Text,
                            Value = op.Text
                        };

                        if (question.HasNext)
                            item.Attributes["next_question_id"] = question.NextId + ""; ;

                        checkBoxList.Items.Add(item);
                    });
                    QuestionOptionsPlaceHolder.Controls.Add(checkBoxList);
                    break;
                case QuestionType.Dropdown:
                    var dropdownList = new DropDownList
                    {
                        ID = "dropdownList"
                    };
                    question.Options.ForEach(op => {
                        var item = new ListItem
                        {
                            Text = op.Text,
                            Value = op.Text
                        };

                        if (question.HasNext)
                            item.Attributes["next_question_id"] = question.NextId + ""; ;

                        dropdownList.Items.Add(item);
                    });
                    QuestionOptionsPlaceHolder.Controls.Add(dropdownList);
                    break;
                default:
                    throw new Exception("Not knowed question type");
            }
        }
        private void GetAnswerQuestion()
        {
            var textBox = (TextBox)QuestionOptionsPlaceHolder.FindControl("textbox");
            var checkBoxList = (CheckBoxList)QuestionOptionsPlaceHolder.FindControl("checkboxList");
            var radioButtonList = (RadioButtonList)QuestionOptionsPlaceHolder.FindControl("radiobuttonList");
            var dropDownList = (DropDownList)QuestionOptionsPlaceHolder.FindControl("dropdownList");
            var question = _stackQuestions.Peek();
            if (textBox != null)
            {
                Answer answer = new Answer
                {
                    QuestionId = question.Id,
                    // AnswerId = 
                    Text = textBox.Text,
                };
                _listAnswer.Add(answer);
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
                        _listAnswer.Add(answer);
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
                        _listAnswer.Add(answer);
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
                        _listAnswer.Add(answer);
                    }
                }
            }
        }
        private void GetNextQuestion() 
        { 
        
        }
        

        protected void ButtonNext_Click(object sender, EventArgs e)
        {
            GetAnswerQuestion();
            LabelTesteLista.Text = "Quantidade de respostas: " + _listAnswer.Count;
        }
    }
}