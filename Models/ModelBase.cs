﻿using CommunityToolkit.Mvvm.ComponentModel;
using log4net;
using log4net.Repository.Hierarchy;

namespace Marimo.MauiBlazor.Models;

public  class ModelBase : ObservableObject
{
    protected ILog Log { get; } 
    public ModelBase()
    {
        Log = LogManager.GetLogger(GetType());
    }
}