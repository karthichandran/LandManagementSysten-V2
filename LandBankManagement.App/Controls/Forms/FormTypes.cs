﻿#region copyright
// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************
#endregion

using System;

using Windows.UI.Xaml;

namespace LandBankManagement.Controls
{
    public interface IFormControl
    {
        event EventHandler<FormVisualState> VisualStateChanged;

        FormEditMode Mode { get; }
        FormVisualState VisualState { get; }

        bool IsEnabled { get; }

        bool Focus(FocusState value);

        void SetVisualState(FormVisualState visualState);
    }

    public enum TextDataType
    {
        String,
        Integer,
        Decimal,
        Double
    }

    public enum FormEditMode
    {
        Auto,
        ReadOnly,
        ReadWrite
    }

    public enum FormVisualState
    {
        Idle,
        Ready,
        Focused,
        Disabled
    }
}
