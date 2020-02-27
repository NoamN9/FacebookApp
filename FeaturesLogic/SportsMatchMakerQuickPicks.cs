using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;

namespace FacebookApp
{
    class SportsMatchMakerQuickPicks : Feature, ISportsMatchMaker
    {
        private User m_LoggedInUser;
        private SportsMatchMakerLogic m_FindMatchesLogic;

        List<User> m_Matches = new List<User>();
        private eGenderToPlayWith m_GenderToMatch;
        private eSportToPlay m_SportToPlay;
        private int m_MinAge;
        private int m_MaxAge;
        private City m_UserLocation;
        private List<User> m_MatchesFound;
        private Boolean m_FoundMatch = false;
        int m_UserAge;

        public SportsMatchMakerQuickPicks()
        {
            m_LoggedInUser = UserInApp.Instace.LoggedInUser;
            m_FindMatchesLogic = new SportsMatchMakerLogic();
            CultureInfo cultureDateTime = new CultureInfo("en-US");
            DateTime birthdayUser = Convert.ToDateTime(m_LoggedInUser.Birthday, cultureDateTime);
            int BirthYear = birthdayUser.Year;
            int todaysYear = DateTime.Now.Year;
            m_UserAge = todaysYear - BirthYear;
        }

        //play football with people of your same age and gender
        internal void FirstQuickPick()
        {
            if (m_LoggedInUser.Gender == User.eGender.male)
            {
                m_GenderToMatch = eGenderToPlayWith.Male;
            }
            else
            {
                m_GenderToMatch = eGenderToPlayWith.Female;
            }
            m_SportToPlay = eSportToPlay.Football;
            m_MinAge = m_UserAge - 1;
            m_MaxAge = m_UserAge + 1;
            m_UserLocation = m_LoggedInUser.Location;
            FindMatch();
        }

        //play tennis with the opposite gender
        internal void SecondQuickPick()
        {
            if (m_LoggedInUser.Gender == User.eGender.male)
            {
                m_GenderToMatch = eGenderToPlayWith.Female;
            }
            else
            {
                m_GenderToMatch = eGenderToPlayWith.Male;
            }
            m_SportToPlay = eSportToPlay.Tennis;
            m_MinAge = m_UserAge - 1;
            m_MaxAge = m_UserAge + 1;
            m_UserLocation = m_LoggedInUser.Location;
            FindMatch();
        }


        public void FindMatch()
        {
            m_FindMatchesLogic.GetLogicAndFindMatches(m_LoggedInUser, m_MinAge, m_MaxAge, m_SportToPlay, m_GenderToMatch);
            m_MatchesFound = m_FindMatchesLogic.m_MatchesFound;
            m_FoundMatch = m_FindMatchesLogic.m_FoundMatch;
        }
    }
}
