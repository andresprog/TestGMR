/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: IIconable
 * ScriptType: Editor
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: Only Date: Sunday, January 5, 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using Cofradinn;
using UnityEngine;

namespace Cofradinn.Utilities.Editor.Hierarchy
{
    public interface IIconable
    {
        Icontype _IconType { get; }
    }
}