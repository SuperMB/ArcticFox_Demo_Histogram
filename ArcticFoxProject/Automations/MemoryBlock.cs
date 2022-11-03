/*
Copyright (c) 2022, Icii Technologies LLC
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree. 
*/

namespace LiveDemo_Histogram;

public class MemoryBlock : VerilogAutomation
{
    protected override Dependencies Dependencies => Dependencies.None;

	protected override void ApplyAutomation()
	{
		CurrentName = Items[0] | Item.Required;
		if(Items.Count > 1)
			CurrentSuffix = Items[1];
		else
			CurrentSuffix = "";

	}

	private static bool _currentNameSet = false;
	private static string _currentName = ""; 
	public static string CurrentName 
	{ 
		get
		{
			if(!_currentNameSet)
				throw new Exception("Trying to access MemoryBlock.CurrentName, but it has not yet been set. Please use the MemoryBlock automation.");
			
			return _currentName;
		} 
		set
		{
			_currentNameSet = true;
			_currentName = value;
		} 
	}
	
	private static bool _currentSuffixSet = false;
	private static string _currentSuffix = ""; 
	public static string CurrentSuffix
	{ 
		get
		{
			if(!_currentSuffixSet)
				throw new Exception("Trying to access MemoryBlock.CurrentSuffix, but it has not yet been set. Please use the MemoryBlock automation.");
			
			return _currentSuffix;
		} 
		set
		{
			_currentSuffixSet = true;
			_currentSuffix = value;
		} 
	}
}