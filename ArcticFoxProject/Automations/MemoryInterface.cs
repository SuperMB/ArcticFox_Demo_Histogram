/*
Copyright (c) 2022, Icii Technologies LLC
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree. 
*/

namespace LiveDemo_Histogram;

public class MemoryInterface : VerilogAutomation
{
    protected override Dependencies Dependencies => Dependencies.None;

	protected override void ApplyAutomation()
	{
		string name = Items[0] | Item.Required; 

		CodeAfterAutomation += @$"
reg {name}Write;
reg [11:0] {name}Address;
reg [15:0] {name}DataIn;

wire [15:0] {name}DataOut;";
	}  
} 