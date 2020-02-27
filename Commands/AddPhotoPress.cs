using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookApp
{
    class AddPhotoPress : ICommand
    {
        UploadPhotoLogic m_UploadPhoto;
        
        public AddPhotoPress(UploadPhotoLogic i_UploadPhoto)
        {
            m_UploadPhoto = i_UploadPhoto;
        }
        public void Execute()
        {
            m_UploadPhoto.ShowButtons();
        }
    }
}
