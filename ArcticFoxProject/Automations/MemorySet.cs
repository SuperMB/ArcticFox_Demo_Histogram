/*
Copyright (c) 2022, Icii Technologies LLC
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree. 
*/

namespace LiveDemo_Histogram;

public class MemorySet : VerilogAutomation
{
    protected override Dependencies Dependencies => Dependencies.None;
	public override string ShortcutName => ">";

	protected override void ApplyAutomation()
	{
		string name = MemoryBlock.CurrentName;
		string suffix = MemoryBlock.CurrentSuffix;

		if(Items.Count == 1 && Items[0] == "0")
		{
			CodeAfterAutomation += @$"
begin
	{name}Write{suffix} <= 0;
	{name}Address{suffix} <= 0;
	{name}DataIn{suffix} <= 0;
end"; 
		}
		else if(Items.Count == 1 && Items[0] == "Hold")
		{
			CodeAfterAutomation += @$"
begin
	{name}Write{suffix} <= 0;
	{name}Address{suffix} <= {name}Address;
	{name}DataIn{suffix} <= {name}DataIn;
end"; 
		}
		else if(Items.Count == 1 && Items[0] == "HoldAddress")
		{
			CodeAfterAutomation += @$"
begin
	{name}Write{suffix} <= 0;
	{name}Address{suffix} <= {name}Address;
	{name}DataIn{suffix} <= 0;
end"; 
		}
		else if(Items.Count == 1 && Items[0] == "HoldValue")
		{
			CodeAfterAutomation += @$"
begin
	{name}Write{suffix} <= 0;
	{name}Address{suffix} <= 0;
	{name}DataIn{suffix} <= {name}DataIn;
end"; 
		}
		else if(Items.Contains("=>"))
		{
			string address = "";
			string value = "";

			int position = 0;
			for(; position < Items.Count; position++)
				if(Items[position] == "=>")
					break;
				else
					address += Items[position] + " ";
					
			position++;
			for(; position < Items.Count; position++)
				value += Items[position] + " ";
			
			address = address.Trim();
			value = value.Trim();
			
			CodeAfterAutomation += @$"
begin
	{name}Write{suffix} <= {1};
	{name}Address{suffix} <= {address};
	{name}DataIn{suffix} <= {value};
end"; 
		}
		else //if(Items.Count == 2)
		{
			string address = "";
			for(int position = 0; position < Items.Count; position++)
				address += Items[position] + " ";
			address = address.Trim();

			CodeAfterAutomation += @$"
begin
	{name}Write{suffix} <= 0;
	{name}Address{suffix} <= {address};
	{name}DataIn{suffix} <= 0;
end"; 
		}
		// else
		// {
		// 	Error("Cannot determine action to take!");
		// }

	}
}