﻿using UnityEngine;

namespace MVC.Views
{
    public abstract class TransitionHandler : MonoBehaviour
    {
        public abstract void OnShow();
        public abstract void OnHide();
    }
}

