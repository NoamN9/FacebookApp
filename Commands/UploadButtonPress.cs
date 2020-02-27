using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    class UploadButtonPress : ICommand
    {
        UploadPhotoLogic m_UploadPhoto;

        public UploadButtonPress(UploadPhotoLogic i_UploadPhoto)
        {
            m_UploadPhoto = i_UploadPhoto;
        }
        public void Execute()
        {
            m_UploadPhoto.m_UploadButton_Click();
        }
    }
}
