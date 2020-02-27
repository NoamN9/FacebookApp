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
using System.Globalization;


namespace FacebookApp
{
    class SportsMatchMakerLogic: Feature, IFeature, ISportsMatchMaker
    {
        private eGenderToPlayWith m_GenderToMatch;
        private eSportToPlay m_SportToPlay;
        private int m_MinAge;
        private int m_MaxAge;
        private City m_UserLocation;
        internal List<User> m_MatchesFound;
        internal Boolean m_FoundMatch = false;
        e_FormState state;

        private User m_LoggedInUser;
       
        Label m_From;
        Label m_To;
        Label m_IntrestedIn;
        Label m_ChooseSport;
        GroupBox m_GeneralGroupBox;
        GroupBox m_GenderGroupBox;
        GroupBox m_SportGroupBox;
        NumericUpDown m_MinValue;
        NumericUpDown m_MaxValue;
        RadioButton m_WomanRadioButton;
        RadioButton m_MaleRadioButton;
        RadioButton m_FootballRadioButton;
        RadioButton m_BasketballRadioButton;
        RadioButton m_TennisRadioButton;
        Button m_FindMatchButton;
        Button m_GetMatchInfo;

        public SportsMatchMakerLogic()
        {
            m_LoggedInUser = UserInApp.Instace.LoggedInUser;
        }

        public SportsMatchMakerLogic(Label i_From,Label i_To,Label i_IntrestedIn, Label i_ChooseSport , GroupBox i_GeneralGroupBox,
            GroupBox i_GenderGroupBox, GroupBox i_SportGroupBox, NumericUpDown i_MinValue, NumericUpDown i_MaxValue, RadioButton i_MaleRadioButton,
            RadioButton i_WomanRadioButton,RadioButton i_FootballRadioButton, RadioButton i_BasketballRadioButton,RadioButton  i_TennisRadioButton,
            Button i_SubmitButton,Button i_GetMatchInfo)
        {
            state = e_FormState.SportsMatchMakerCustomize;
            m_LoggedInUser = UserInApp.Instace.LoggedInUser; 
            m_From = i_From;
            m_To = i_To;
            m_IntrestedIn = i_IntrestedIn;
            m_ChooseSport = i_ChooseSport;
            m_GeneralGroupBox = i_GeneralGroupBox;
            m_GenderGroupBox = i_GenderGroupBox;
            m_SportGroupBox = i_SportGroupBox;
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
            m_MaleRadioButton = i_MaleRadioButton;
            m_WomanRadioButton = i_WomanRadioButton;
            m_FootballRadioButton = i_FootballRadioButton;
            m_BasketballRadioButton = i_BasketballRadioButton;
            m_TennisRadioButton = i_TennisRadioButton;
            m_FindMatchButton = i_SubmitButton;
            m_GetMatchInfo = i_GetMatchInfo;

        }
        public void FindMatch()
        {
            if ((m_WomanRadioButton.Checked || m_MaleRadioButton.Checked) && (m_BasketballRadioButton.Checked || m_FootballRadioButton.Checked ||
                m_TennisRadioButton.Checked))
            {
                CheckAnswers();
                GetLogicAndFindMatches(m_LoggedInUser, Convert.ToInt32(m_MinValue.Value), Convert.ToInt32(m_MaxValue.Value), m_SportToPlay, m_GenderToMatch);

            }
            else
            {
                MessageBox.Show("Please select gender or sport");
            }
        }

        public void GetLogicAndFindMatches(User i_LoggedInUser, int i_MinAge, int i_MaxAge, eSportToPlay i_SportToPlay, eGenderToPlayWith i_GenderToMatch)
        {
            m_GenderToMatch = i_GenderToMatch;
            m_SportToPlay = i_SportToPlay;
            m_MinAge = i_MinAge;
            m_MaxAge = i_MaxAge;       
            m_UserLocation = i_LoggedInUser.Location; //return null
            m_MatchesFound = new List<User>();

            generatePotentialMatches();
        }

        private void generatePotentialMatches()
        {
            if (!validateAgeRange())
            {
                MessageBox.Show("Invalid age range");
            }
            else
            {
                bool foundMatch = false;
                foreach (User friend in m_LoggedInUser.Friends)
                {
                    if (isMatch(friend))
                    {
                        foundMatch = true;
                        m_MatchesFound.Add(friend);
                    }
                    
                }
                
                if (!foundMatch)
                {                    
                    MessageBox.Show("No potential players found, better luck in the future");
                    HideButtons();                    
                }
                else
                {
                    m_FoundMatch = true;
                    MessageBox.Show("List Of Matches: " + m_MatchesFound.ToString());
                    
                }

            }
        }

        private bool isMatch(User i_Friend)
        {
            bool genderMatch = false;
            if (m_GenderToMatch == eGenderToPlayWith.Both)
            {
                genderMatch = true;
            }
            else
            {
                //check if works
                genderMatch = (m_GenderToMatch.ToString() == i_Friend.Gender.ToString());
            }
            CultureInfo cultureDateTime = new CultureInfo("en-US");
            DateTime birthdayUser = Convert.ToDateTime(i_Friend.Birthday, cultureDateTime);
            int userBirthYear = birthdayUser.Year;
            int todaysYear = DateTime.Now.Year;
            int userAge = todaysYear - userBirthYear;
            bool ageMatch = (userAge >= m_MinAge && userAge <= m_MaxAge);
            bool locationMatch = (m_UserLocation == i_Friend.Location);
            return (genderMatch && ageMatch && locationMatch);
        }

        private bool validateAgeRange() => m_MinAge <= m_MaxAge;

        public  void HideButtons()
        {
            if(state == e_FormState.SportsMatchMakerCustomize)
            {
                m_From.Visible = false;
                m_To.Visible = false;
                m_IntrestedIn.Visible = false; 
                m_ChooseSport.Visible = false; 
                m_GeneralGroupBox.Visible = false; 
                m_GenderGroupBox.Visible = false; 
                m_SportGroupBox.Visible = false; 
                m_MinValue.Visible = false; 
                m_MaxValue.Visible = false; 
                m_WomanRadioButton.Visible = false; 
                m_MaleRadioButton.Visible = false;
                m_FootballRadioButton.Visible = false; 
                m_BasketballRadioButton.Visible = false; 
                m_TennisRadioButton.Visible = false;
                m_FindMatchButton.Visible = false;
                m_GetMatchInfo.Visible = false;
            }
             
        }   
        public  void ShowButtons()
        {
            m_SportGroupBox.Visible = true;
            m_From.Visible = true;
            m_To.Visible = true;
            m_IntrestedIn.Visible = true;
            m_ChooseSport.Visible = true;
            m_GeneralGroupBox.Visible = true;
            m_GenderGroupBox.Visible = true;
            m_MinValue.Visible = true;
            m_MaxValue.Visible = true;
            m_WomanRadioButton.Visible = true;
            m_MaleRadioButton.Visible = true;
            m_FootballRadioButton.Visible = true;
            m_BasketballRadioButton.Visible = true;
            m_TennisRadioButton.Visible = true;
            m_FindMatchButton.Visible = true;
            m_GetMatchInfo.Visible = true;
        }
        public void m_FindMatchButton_Click()
        {
            FindMatch();
             
        }
        private RadioButton GetCheckedRadioButton(List <RadioButton> i_RadioButtons)
        {
            foreach(RadioButton radiobutton in i_RadioButtons)
            {
                if (radiobutton.Checked)
                {
                    return radiobutton;
                }
                
            }
            return null;
        }
        private eGenderToPlayWith GetEnumGender(RadioButton i_SelectedRadioButton)
        {
            switch (i_SelectedRadioButton.Text)
            {
                case "Male":
                    return eGenderToPlayWith.Male;                    
                case "Woman":
                    return eGenderToPlayWith.Female;
                case "Both":
                    return eGenderToPlayWith.Both;

            }
            return eGenderToPlayWith.Null;
        }
        private eSportToPlay GetEnumSport(RadioButton i_SelectedRadioButton)
        {
            switch (i_SelectedRadioButton.Text)
            {
                case "Basketball":
                    return eSportToPlay.BasketBall;
                case "Football":
                    return eSportToPlay.Football;
                case "Tennis":
                    return eSportToPlay.Tennis;

            }
            return eSportToPlay.Null;
        }
        private void CheckAnswers()
        {
            List<RadioButton> Gender = new List<RadioButton>();
            List<RadioButton> Sports = new List<RadioButton>();

            Gender.Add(m_WomanRadioButton);
            Gender.Add(m_MaleRadioButton);
            RadioButton SelectedGender = GetCheckedRadioButton(Gender);

            Sports.Add(m_BasketballRadioButton);
            Sports.Add(m_FootballRadioButton);
            Sports.Add(m_TennisRadioButton);
            RadioButton SelectedSport = GetCheckedRadioButton(Sports);

            m_GenderToMatch = GetEnumGender(SelectedGender);
            m_SportToPlay = GetEnumSport(SelectedSport);
        }
        private void PresenMatches()
        {
            MessageBox.Show(m_MatchesFound.ToString());
        }
        public void m_GetMatchInfo_Click()
        {
            if (!m_FoundMatch)
            {
                MessageBox.Show("Sorry cant give you match info because no match found have been found");
               
            }
            else
            {
                MessageBox.Show("Here is your match Email send him message! " + m_MatchesFound.ElementAt(0).Email);
                HideButtons();
            }
        }
    }

   
}

