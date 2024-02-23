﻿using SatisfactoryProductionManager.Model.Elements;
using System;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager.ViewModel.ButtonModels
{
    public class RequestButtonVM : ObjectButtonVM<ResourceRequest>
    {
        public string Count { get => InnerObject.CountPerMinute.ToString(); }


        public RequestButtonVM(ResourceRequest request)
        {
            InnerObject = request;
            ImageSource = new BitmapImage(new Uri($"../Assets/Resources/{request.Resource}.png", UriKind.Relative));
        }
    }
}
