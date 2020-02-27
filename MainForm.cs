using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace FacebookApp
{
    public partial class MainForm : Form
    {
        FacebookWrapper.ObjectModel.User m_LoggedInUser;
        PostSomethingLogic m_PostSomething;
        UploadPhotoLogic m_UploadPhoto;
        FetchFriendsLogic m_FetchFriends;
        FetchLikePagesLogic m_FetchLikePages;
        TestYourRelationshipLogic m_testYourRelationship;
        SportsMatchMakerLogic m_SportMatchMaker;

        ProxyTestYourReleationshipLogic m_ProxyTestYourReleationship;
        SportsMatchMakerFacade m_SportsMatchMakerFacade;

        Dictionary<e_FormState, IFeature> m_FeatureMap = new Dictionary<e_FormState, IFeature>();
        
        FeatureIterable m_FeatureIterable = new FeatureIterable();
        FeatureIterator m_FeatureIterator;

        IFeature m_LastFeature=null;
        public MainForm()
        {
            InitializeComponent();
            m_LoggedInUser = UserInApp.Instace.LoggedInUser; ;
            FetchInfo();
            FetchFeatures();
            AddFeaturesToMap();
            SetIteratorAndDataStructure();
            RunRelationshipOnBackroung();
        }
         
        private void PostSomethingButton_Click(object sender, EventArgs e)
        {
            HideLastFeature();
            ICommand PostCommand = new PostSomethingPress(m_PostSomething);
            PostCommand.Execute();
            m_LastFeature = m_FeatureIterator.GetFeature(e_FormState.PostSomethingState);            
        }

        private void SendPostButton_Click(object sender, EventArgs e)
        {
            ICommand SendButtonEvent = new SendButtonPress(m_PostSomething);
            SendButtonEvent.Execute();
             
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            LogoutFromMainWindow();
        }
        
        private void LogoutFromMainWindow()
        {
            FacebookWrapper.FacebookService.Logout(afterSuccessfullLogout);
            this.Hide();
            Form LoginForm = new LoginForm();
            LoginForm.ShowDialog();
            this.Close();
        }
        private void afterSuccessfullLogout()
        {
            MessageBox.Show("Logout successfully!");
        }

        private void AddPictureButton_Click(object sender, EventArgs e)
        {
            HideLastFeature();
            ICommand AddPicturePress = new AddPhotoPress(m_UploadPhoto);
            AddPicturePress.Execute();
            m_LastFeature = m_FeatureIterator.GetFeature(e_FormState.UploadPhotoState);
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            ICommand BrowseButtonPress = new BrowserPress(m_UploadPhoto);
            BrowseButtonPress.Execute();
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            ICommand UploadButtonPress = new UploadButtonPress(m_UploadPhoto);
            UploadButtonPress.Execute();
        }

        private void FetchFriendsButton_Click(object sender, EventArgs e)
        {
            HideLastFeature();
            ICommand FetchFriendsPress = new FetchFriendsPress(m_FetchFriends);
            FetchFriendsPress.Execute();
            m_LastFeature = m_FeatureIterator.GetFeature(e_FormState.FetchFriendsState);
        }

        private void PageLikesButton_Click(object sender, EventArgs e)
        {
            HideLastFeature();
            ICommand FetchLikePagesPages = new FetchLikePagesPress(m_FetchLikePages);
            FetchLikePagesPages.Execute();
            m_LastFeature = m_FeatureIterator.GetFeature(e_FormState.FetchLikePagesState);
        }

        private void TestYourRelationshipButton_Click(object sender, EventArgs e)
        {
            HideLastFeature();
            ICommand TestYourRelationshipPress = new TestYourRelationshipPress(m_ProxyTestYourReleationship);
            TestYourRelationshipPress.Execute();
            m_LastFeature = m_FeatureIterator.GetFeature(e_FormState.TestYourRelationshipState);
        }
        private List<RadioButton> AddRadioButtonsToList()                        
        {
            List<RadioButton> RadioButtons = new List<RadioButton>();
            
            RadioButtons.Add(m_RadioButton1a);
            RadioButtons.Add(m_RadioButton1b);
            RadioButtons.Add(m_RadioButton1c);
            RadioButtons.Add(m_RadioButton2a);
            RadioButtons.Add(m_RadioButton2b);
            RadioButtons.Add(m_RadioButton2c);
            RadioButtons.Add(m_RadioButton3a);
            RadioButtons.Add(m_RadioButton3b);
            RadioButtons.Add(m_RadioButton3c);

            return RadioButtons;
        }

        private void m_SubmitButton_Click(object sender, EventArgs e)
        {
            ICommand SubmitButtonPress = new SubmitButtonPress(m_testYourRelationship);
            SubmitButtonPress.Execute();
        }
        private void ShareButton_Click(object sender, EventArgs e)
        {

            ICommand ShareButtonPress = new ShareButtonPress(m_testYourRelationship);
            ShareButtonPress.Execute();
        }
        private void SportMakerButton_Click(object sender, EventArgs e)
        {
            HideLastFeature();
            ICommand SportButtonPress = new SportButtonPress(m_SportsMatchMakerFacade);
            SportButtonPress.Execute();
            m_LastFeature = m_FeatureIterator.GetFeature(e_FormState.SportMatchMakerState);
        }

        private void FindMatchButton_Click(object sender, EventArgs e)
        {
            ICommand FindMatchPress = new FindMatchPress(m_SportMatchMaker);
            FindMatchPress.Execute();

        }
       
        private void GetMatchInfoButton_Click(object sender,EventArgs e)
        {
            ICommand MatchInfoPress = new MatchInfoPress(m_SportMatchMaker);
            MatchInfoPress.Execute();
        }

        private void FetchInfo()
        {
            pictureBox1.Image = m_LoggedInUser.ImageNormal;
            WelcomeLabel.Text = "Welcome " + m_LoggedInUser.FirstName + "!";
         
        }
        private void FetchFeatures()
        {
            m_PostSomething = new PostSomethingLogic();
            m_PostSomething.TextBox = PostingTextBox;
            m_PostSomething.SendButton = SendPostButton;

            m_UploadPhoto = new UploadPhotoLogic(UploadButton, BrowseButton, PictureBoxToUpload);
            m_FetchFriends = new FetchFriendsLogic(FriendsListBox, FriendDetailsPanel, userBindingSource);
            m_FetchLikePages = new FetchLikePagesLogic(PageLikesListBox);
            m_testYourRelationship = new TestYourRelationshipLogic(m_Q1ProfilePicture, m_Q2FriendPicture, Question1Label,
             Question2Label, Question3Label, AddRadioButtonsToList(), m_SubmitButton,
              GroupBox1, GroupBox2, GroupBox3, TestYourRGroupBox, ShareButton);
            m_SportMatchMaker = new SportsMatchMakerLogic(FromLabel, ToLabelButton, IntrestedToPlayLabel, ChooseSportLabel,
               MatchingSportGroupBox, GenderGroupBox, SportGroupBox, FromNumbericButton, ToNumbericButton, RadioButtonMale, RadioButtonFemale,
               FootballRadioButton, BasketballRadioButton, TennisRadioButton, FindMatchButton, GetMatchInfoButton);

            m_ProxyTestYourReleationship = new ProxyTestYourReleationshipLogic(m_testYourRelationship);
            m_SportsMatchMakerFacade = new SportsMatchMakerFacade(FasadeSportGroupBox, QuickMatchLabel, FasadeFootballButton, FasadeTennisButton,
             CustomizeLabel,LoadCustomizeButton);
                
        }

        private void RunRelationshipOnBackroung()
        {
            m_ProxyTestYourReleationship.FetchData();  
            
        }

        private void LoadCustomizeButton_Click(object sender, EventArgs e)
        {
            HideLastFeature();
            ICommand CustomizePress = new SettingButtonPress(m_SportMatchMaker);
            CustomizePress.Execute();
            m_LastFeature = m_FeatureIterator.GetFeature(e_FormState.SportsMatchMakerCustomize);

        }

        private void FasadeFootballButton_Click(object sender, EventArgs e)
        {
            ICommand QuickFootballMatch = new FootballQuickMatchPress(m_SportsMatchMakerFacade);
            QuickFootballMatch.Execute();
        }

        private void FasadeTennisButton_Click(object sender, EventArgs e)
        {
            ICommand QuickTennisMatch = new TennisQuickMatchPress(m_SportsMatchMakerFacade);
            QuickTennisMatch.Execute();
        }
        private void AddFeaturesToMap()
        {
            m_FeatureMap.Add(e_FormState.FetchFriendsState,m_FetchFriends);
            m_FeatureMap.Add(e_FormState.FetchLikePagesState, m_FetchLikePages);
            m_FeatureMap.Add(e_FormState.PostSomethingState, m_PostSomething);
            m_FeatureMap.Add(e_FormState.UploadPhotoState, m_UploadPhoto);
            m_FeatureMap.Add(e_FormState.TestYourRelationshipState, m_testYourRelationship);
            m_FeatureMap.Add(e_FormState.SportMatchMakerState, m_SportsMatchMakerFacade);
            m_FeatureMap.Add(e_FormState.SportsMatchMakerCustomize, m_SportMatchMaker);
            

        }
        private void SetIteratorAndDataStructure()
        {
            m_FeatureIterator = m_FeatureIterable.GetEnumrator();
            m_FeatureIterator.FeatureMap = m_FeatureMap;
        }
       
        private void HideLastFeature()
        {
            if (m_LastFeature != null)
            {
                m_LastFeature.HideButtons();
            }

        }

    }   
        

}
