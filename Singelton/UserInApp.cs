using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    class UserInApp
    {
        public FacebookWrapper.ObjectModel.User LoggedInUser { get; }

        private static UserInApp s_This;

        //app ID - 2466563750140078
        private UserInApp()
        {
            FacebookWrapper.LoginResult result = FacebookWrapper.FacebookService.Login("2466563750140078",
                "publish_to_groups",
                "public_profile",
                "user_birthday",
                "user_friends",
                "user_events",
                "user_hometown",
                "user_likes",
                "user_location",
                "user_photos",
                "user_posts",
                "user_tagged_places",
                "user_videos",
                "read_page_mailboxes",
                "manage_pages",
                "publish_pages");

            if (!string.IsNullOrEmpty(result.AccessToken))
            {
                LoggedInUser = result.LoggedInUser;
            }
            else
            {
                throw new Exception(result.ErrorMessage);
            }
        }

        public static UserInApp Instace
        {
            get
            {
                if (s_This == null)
                {
                    s_This = new UserInApp();
                }

                return s_This;
            }
        }


    }
}
