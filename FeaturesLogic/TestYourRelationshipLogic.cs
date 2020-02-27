using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Facebook;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using System.Collections.Generic;


namespace FacebookApp
{
    class TestYourRelationshipLogic : Feature, IFeature, ITestYourRelationshipLogic
    {
        User m_LoggedInUser;
        PictureBox m_Q1PictureBox;
        PictureBox m_Q2PictureBox;
        Label m_Q1Label;
        Label m_Q2Label;
        Label m_Q3Label;
        List<RadioButton> m_RadioButtons; // 9  each 3 are the options for the question 
        Button m_SubmitButton;
        GroupBox m_Q1GroupBox;
        GroupBox m_Q2GroupBox;
        GroupBox m_Q3GroupBox;
        String m_Q1RightAnswer;
        String m_Q2RightAnswer;
        String m_Q3RightAnswer;
        List<String> m_Q1Options;
        List<String> m_Q2Options;
        List<String> m_Q3Options;
        GroupBox m_GeneralGroupBox;
        Button m_ShareButton;
        List<QuestionControl> m_QuestionControls;

        Boolean m_AllowToShareResults;
        int m_CorrectAnswers;

        public Boolean m_EnoughFriendsToPlay;
        public Boolean m_EnoughLikePhotos;


        public TestYourRelationshipLogic(PictureBox i_Q1Picture, PictureBox i_Q2Picture, Label i_Q1Label,
            Label i_Q2Label, Label i_Q3Label, List<RadioButton> i_RadioButtons, Button i_SubmitButton,
            GroupBox i_Q1GroupBox, GroupBox i_Q2GroupBox, GroupBox i_Q3GroupBox, GroupBox i_GeneralGroupBox, Button i_ShareButton)
        {
            m_LoggedInUser = UserInApp.Instace.LoggedInUser;
            m_AllowToShareResults = false;
            m_EnoughFriendsToPlay = true;
            m_EnoughLikePhotos = true;

            m_Q1PictureBox = i_Q1Picture;
            m_Q2PictureBox = i_Q2Picture;
            m_Q1Label = i_Q1Label;
            m_Q2Label = i_Q2Label;
            m_Q3Label = i_Q3Label;
            m_RadioButtons = i_RadioButtons;
            m_SubmitButton = i_SubmitButton;
            m_Q1GroupBox = i_Q1GroupBox;
            m_Q2GroupBox = i_Q2GroupBox;
            m_Q3GroupBox = i_Q3GroupBox;
            m_GeneralGroupBox = i_GeneralGroupBox;
            m_ShareButton = i_ShareButton;
            m_QuestionControls = ReturnListofTagQuestions();
        }
        public void HideButtons()
        {
           
            m_Q1PictureBox.Visible = false;
            m_Q2PictureBox.Visible = false;
            m_Q1Label.Visible = false;
            m_Q2Label.Visible = false;
            m_Q3Label.Visible = false;
            m_Q1GroupBox.Visible = false;
            m_Q2GroupBox.Visible = false;
            m_Q3GroupBox.Visible = false;
            m_SubmitButton.Visible = false;
            m_GeneralGroupBox.Visible = false;
            m_ShareButton.Visible = false;
            foreach (RadioButton Option in m_RadioButtons)
            {
                Option.Visible = false;
            }
        }

        public void ShowButtons()
        {

            m_GeneralGroupBox.Visible = true;
            QustionControlFilter = new QuestionToPresent(m_QuestionControls, 1);
            QustionControlFilter.ShowButtons();
            QustionControlFilter = new QuestionToPresent(m_QuestionControls, 2);
            QustionControlFilter.ShowButtons();
            QustionControlFilter = new QuestionToPresent(m_QuestionControls, 3);
            QustionControlFilter.ShowButtons();       
            m_SubmitButton.Visible = true;
            m_ShareButton.Visible = true;
        }
        public void FetchData()
        {
            if (m_LoggedInUser.Friends.Count >= 3)
            {

                //      MessageBox.Show("You dont have enough friends to play the game");
                m_EnoughFriendsToPlay = true;
                //return;
                //       HideButtons();
            }
            m_EnoughLikePhotos = FetchQ1();
            if (!m_EnoughLikePhotos || !m_EnoughFriendsToPlay) return;
            Thread Question2Fetcher = new Thread(FetchQ2);
            Thread Question3Fetcher = new Thread(FetchQ3);
            Question2Fetcher.Start();
            Question3Fetcher.Start();


        }
        private Boolean FetchQ1()
        {
            Photo RandomLikedPhotoUserTaggedIn = GetRandomLikedPhoto();

            if (RandomLikedPhotoUserTaggedIn == null)
            {
                return false;
            }

            m_Q1PictureBox.Image = RandomLikedPhotoUserTaggedIn.ImageNormal;
            FetchQ1Options(RandomLikedPhotoUserTaggedIn);
            return true;
        }

        private void FetchQ1Options(Photo i_RandomLikedPhotoUserTaggedIn)
        {
            m_Q1Options = new List<String>();

            m_Q1RightAnswer = GetQ1RightAnswer(i_RandomLikedPhotoUserTaggedIn);
            m_Q1Options.Add(m_Q1RightAnswer);

            String WrongOptionQ1a = RandomizeFriend().Name;
            while (m_Q1Options.Contains(WrongOptionQ1a))
            {
                WrongOptionQ1a = RandomizeFriend().Name;
            }
            m_Q1Options.Add(WrongOptionQ1a);

            String WrongOptionQ1b = RandomizeFriend().Name;
            while (m_Q1Options.Contains(WrongOptionQ1b))
            {
                WrongOptionQ1b = RandomizeFriend().Name;
            }
            m_Q1Options.Add(WrongOptionQ1b);

            Shuffle(m_Q1Options);
            m_RadioButtons.ElementAt(0).Text = m_Q1Options.ElementAt(0);
            m_RadioButtons.ElementAt(1).Text = m_Q1Options.ElementAt(1);
            m_RadioButtons.ElementAt(2).Text = m_Q1Options.ElementAt(2);
        }
        private void FetchQ2()
        {
            User RandomFriend = RandomizeFriend();
            m_Q2PictureBox.Image = RandomFriend.ImageNormal;
            m_Q2Options = new List<String>();
            m_Q2RightAnswer = RandomFriend.Name;
            m_Q2Options.Add(m_Q2RightAnswer);

            String WrongOptionQ2a = RandomizeFriend().Name;
            while (m_Q2Options.Contains(WrongOptionQ2a))
            {
                WrongOptionQ2a = RandomizeFriend().Name;
            }
            m_Q2Options.Add(WrongOptionQ2a);

            String WrongOptionQ2b = RandomizeFriend().Name;
            while (m_Q2Options.Contains(WrongOptionQ2b))
            {
                WrongOptionQ2b = RandomizeFriend().Name;
            }
            m_Q2Options.Add(WrongOptionQ2b);

            Shuffle(m_Q2Options);
            //     m_RadioButtons.ElementAt(3).Text = m_Q2Options.ElementAt(0);
            //     m_RadioButtons.ElementAt(4).Text = m_Q2Options.ElementAt(1);
            //      m_RadioButtons.ElementAt(5).Text = m_Q2Options.ElementAt(2);

            m_RadioButtons.ElementAt(3).Invoke(new Action(() => m_RadioButtons.ElementAt(3).Text = m_Q2Options.ElementAt(0)));
            m_RadioButtons.ElementAt(4).Invoke(new Action(() => m_RadioButtons.ElementAt(4).Text = m_Q2Options.ElementAt(1)));
            m_RadioButtons.ElementAt(5).Invoke(new Action(() => m_RadioButtons.ElementAt(5).Text = m_Q2Options.ElementAt(2)));

        }

        private void FetchQ3()
        {
            User RandomFriend = RandomizeFriend();
            m_Q3Options = new List<String>();
            String GuessBirthday = RandomFriend.Birthday;
            m_Q3Label.Invoke(new Action(() => m_Q3Label.Text = String.Format("Guess which friend have birthday in {0}", GuessBirthday)));
            // m_Q3Label.Text = String.Format("Guess which friend have birthday in {0}",GuessBirthday);

            m_Q3RightAnswer = RandomFriend.Name;
            m_Q3Options.Add(m_Q3RightAnswer);

            String WrongOptionQ3a = RandomizeFriend().Name;
            while (m_Q3Options.Contains(WrongOptionQ3a))
            {
                WrongOptionQ3a = RandomizeFriend().Name;
            }
            m_Q3Options.Add(WrongOptionQ3a);

            String WrongOptionQ3b = RandomizeFriend().Name;
            while (m_Q3Options.Contains(WrongOptionQ3b))
            {
                WrongOptionQ3b = RandomizeFriend().Name;
            }
            m_Q3Options.Add(WrongOptionQ3b);

            Shuffle(m_Q3Options);
            //   m_RadioButtons.ElementAt(6).Text = String.Format("{0}", m_Q3Options.ElementAt(0));
            //   m_RadioButtons.ElementAt(7).Text = String.Format("{0}", m_Q3Options.ElementAt(1));
            //   m_RadioButtons.ElementAt(8).Text = String.Format("{0}", m_Q3Options.ElementAt(2));
            m_RadioButtons.ElementAt(6).Invoke(new Action(() => m_RadioButtons.ElementAt(6).Text = m_Q3Options.ElementAt(0)));
            m_RadioButtons.ElementAt(7).Invoke(new Action(() => m_RadioButtons.ElementAt(7).Text = m_Q3Options.ElementAt(1)));
            m_RadioButtons.ElementAt(8).Invoke(new Action(() => m_RadioButtons.ElementAt(8).Text = m_Q3Options.ElementAt(2)));
        }
        private Photo RandomizePhoto()
        {
            Random rnd = new Random();
            return m_LoggedInUser.PhotosTaggedIn[rnd.Next(0, m_LoggedInUser.PhotosTaggedIn.Count)];
        }
        private Photo GetRandomLikedPhoto()
        {
            int Limit = 100000;

            Photo PhotoToReturn = RandomizePhoto();
            while (PhotoToReturn.LikedBy.Count == 0)
            {

                PhotoToReturn = RandomizePhoto();
                if (Limit == 0)
                {
                    return null;
                }

                Limit--;
            }

            return PhotoToReturn;
        }
        private String GetQ1RightAnswer(Photo i_PhotoUserTaggedIn)
        {
            string RandomName = i_PhotoUserTaggedIn.LikedBy.ElementAt(0).Name; //should be random number

            if (RandomName == null)
            {
                return "Nobody like's it";
            }

            return RandomName;
        }
        private User RandomizeFriend()
        {

            Random rnd = new Random();
            return m_LoggedInUser.Friends[rnd.Next(0, m_LoggedInUser.Friends.Count)];


        }

        public void Shuffle(List<string> i_AnswersList)
        {
            Random rnd = new Random();

            int n = i_AnswersList.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                string value = i_AnswersList[k];
                i_AnswersList[k] = i_AnswersList[n];
                i_AnswersList[n] = value;

            }
        }
        public void m_SubmitButton_Click()
        {
            if ((m_RadioButtons.ElementAt(0).Checked || m_RadioButtons.ElementAt(1).Checked || m_RadioButtons.ElementAt(2).Checked)
                && (m_RadioButtons.ElementAt(3).Checked || m_RadioButtons.ElementAt(4).Checked || m_RadioButtons.ElementAt(5).Checked)
                && (m_RadioButtons.ElementAt(6).Checked || m_RadioButtons.ElementAt(7).Checked || m_RadioButtons.ElementAt(8).Checked))
            {
                int RightAnswers = CheckQuizAnswers();
                ShowResult(RightAnswers);

            }
            else
            {
                MessageBox.Show("Please answer all questions");
            }
        }
        private int CheckQuizAnswers()
        {
            int CountRightAnswers = 0;

            List<RadioButton> Q1Options = new List<RadioButton>();
            List<RadioButton> Q2Options = new List<RadioButton>();
            List<RadioButton> Q3Options = new List<RadioButton>();

            Q1Options.Add(m_RadioButtons.ElementAt(0));
            Q1Options.Add(m_RadioButtons.ElementAt(1));
            Q1Options.Add(m_RadioButtons.ElementAt(2));

            Q2Options.Add(m_RadioButtons.ElementAt(3));
            Q2Options.Add(m_RadioButtons.ElementAt(4));
            Q2Options.Add(m_RadioButtons.ElementAt(5));

            Q3Options.Add(m_RadioButtons.ElementAt(6));
            Q3Options.Add(m_RadioButtons.ElementAt(7));
            Q3Options.Add(m_RadioButtons.ElementAt(8));

            foreach (RadioButton Option in Q1Options)
            {
                if (Option.Checked)
                {
                    if (Option.Text == m_Q1RightAnswer)
                    {
                        CountRightAnswers++;
                    }

                }
            }

            foreach (RadioButton Option in Q2Options)
            {
                if (Option.Checked)
                {
                    if (Option.Text == m_Q2RightAnswer)
                    {
                        CountRightAnswers++;
                    }

                }
            }

            foreach (RadioButton Option in Q3Options)
            {
                if (Option.Checked)
                {
                    if (Option.Text == m_Q3RightAnswer)
                    {
                        CountRightAnswers++;
                    }

                }
            }

            return CountRightAnswers;
        }
        private void ShowResult(int i_CorrectAnswers)
        {
            m_CorrectAnswers = i_CorrectAnswers;


            switch (i_CorrectAnswers)
            {
                case 0:
                    MessageBox.Show("You got 0/3 :(");
                    break;
                case 1:
                    MessageBox.Show("You got 1/3 :(");
                    break;
                case 2:
                    MessageBox.Show("You got 2/3 not bad ;)");
                    break;
                case 3:
                    MessageBox.Show("You got 3/3 what a friend! :)");
                    break;
            }
            m_AllowToShareResults = true;

        }
        public void ShareButton_click()
        {
            if (!m_AllowToShareResults)
            {
                MessageBox.Show("Please finish your quiz ");
            }
            else
            {
                try
                {
                    m_LoggedInUser.PostStatus("Checkout my quiz result i got" + m_CorrectAnswers + "from 3");
                    MessageBox.Show("Posted!");
                    HideButtons();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Thread.Sleep(2000);
                    HideButtons();
                }
            }
        }

        public List<QuestionControl> ReturnListofTagQuestions()
        {
            List<QuestionControl> QuestionsControllers = new List<QuestionControl>();

            QuestionControl Q1PictureBox = new QuestionControl( m_Q1PictureBox);
            Q1PictureBox.QuestionNumber = 1;
            QuestionsControllers.Add(Q1PictureBox);

            QuestionControl Q2PictureBox = new QuestionControl(m_Q2PictureBox);
            Q2PictureBox.QuestionNumber = 2;
            QuestionsControllers.Add(Q2PictureBox);

            QuestionControl Q1Label = new QuestionControl(m_Q1Label);
            Q1Label.QuestionNumber = 1;
            QuestionsControllers.Add(Q1Label);

            QuestionControl Q2Label = new QuestionControl(m_Q2Label);
            Q2Label.QuestionNumber = 2;
            QuestionsControllers.Add(Q2Label);

            QuestionControl Q3Label = new QuestionControl(m_Q3Label);
            Q3Label.QuestionNumber = 3;
            QuestionsControllers.Add(Q3Label);

            QuestionControl Q1GroupBox = new QuestionControl(m_Q1GroupBox);
            Q1GroupBox.QuestionNumber = 1;
            QuestionsControllers.Add(Q1GroupBox);

            QuestionControl Q2GroupBox = new QuestionControl(m_Q2GroupBox);
            Q2GroupBox.QuestionNumber = 2;
            QuestionsControllers.Add(Q2GroupBox);

            QuestionControl Q3GroupBox = new QuestionControl(m_Q3GroupBox);
            Q3GroupBox.QuestionNumber = 3;
            QuestionsControllers.Add(Q3GroupBox);

            QuestionControl RadioButtonQ1O1 = new QuestionControl(m_RadioButtons.ElementAt(0));
            RadioButtonQ1O1.QuestionNumber = 1;
            QuestionControl RadioButtonQ1O2 = new QuestionControl(m_RadioButtons.ElementAt(1));
            RadioButtonQ1O2.QuestionNumber = 1;
            QuestionControl RadioButtonQ1O3 = new QuestionControl(m_RadioButtons.ElementAt(2));
            RadioButtonQ1O3.QuestionNumber = 1;

            QuestionsControllers.Add(RadioButtonQ1O1);
            QuestionsControllers.Add(RadioButtonQ1O2);
            QuestionsControllers.Add(RadioButtonQ1O3);

            QuestionControl RadioButtonQ2O1 = new QuestionControl(m_RadioButtons.ElementAt(3));
            RadioButtonQ2O1.QuestionNumber = 2;
            QuestionControl RadioButtonQ2O2 = new QuestionControl(m_RadioButtons.ElementAt(4));
            RadioButtonQ2O2.QuestionNumber = 2;
            QuestionControl RadioButtonQ2O3 = new QuestionControl(m_RadioButtons.ElementAt(5));
            RadioButtonQ2O3.QuestionNumber = 2;

            QuestionsControllers.Add(RadioButtonQ2O1);
            QuestionsControllers.Add(RadioButtonQ2O2);
            QuestionsControllers.Add(RadioButtonQ2O3);


            QuestionControl RadioButtonQ3O1 = new QuestionControl(m_RadioButtons.ElementAt(6));
            RadioButtonQ3O1.QuestionNumber = 3;
            QuestionControl RadioButtonQ3O2 = new QuestionControl(m_RadioButtons.ElementAt(7));
            RadioButtonQ3O2.QuestionNumber = 3;
            QuestionControl RadioButtonQ3O3 = new QuestionControl(m_RadioButtons.ElementAt(8));
            RadioButtonQ3O3.QuestionNumber = 3;

            QuestionsControllers.Add(RadioButtonQ3O1);
            QuestionsControllers.Add(RadioButtonQ3O2);
            QuestionsControllers.Add(RadioButtonQ3O3);

            return QuestionsControllers;
        }
    }

}         
   



   




