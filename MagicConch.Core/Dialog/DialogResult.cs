﻿using MagicConch.Core.Parameter;

namespace MagicConch.Core.Dialog
{
    public class DialogResult : IDialogResult
    {
        public bool Success { get; set; }
        public Parameters Parameters { get; set; }

        public DialogResult(bool success = false, Parameters? parameters = null)
        {
            Success = success;
            Parameters = parameters ?? new Parameters();
        }
    }
}
