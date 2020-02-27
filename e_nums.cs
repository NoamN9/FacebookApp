using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    public enum e_FormState
    {
        MainMenuState = 0,
        PostSomethingState = 1,
        UploadPhotoState = 2,
        FetchFriendsState = 3,
        FetchLikePagesState = 4,
        TestYourRelationshipState = 5,
        SportMatchMakerState = 6,
        SportsMatchMakerCustomize = 7,

    }
    public enum eGenderToPlayWith
    {
        Male,
        Female,
        Both,
        Null,
    }
    public enum eSportToPlay
    {
        Football,
        BasketBall,
        Tennis,
        Null
    }
    }
