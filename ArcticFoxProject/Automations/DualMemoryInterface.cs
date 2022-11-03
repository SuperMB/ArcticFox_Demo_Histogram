/*
Copyright (c) 2022, Icii Technologies LLC
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree. 
*/

namespace LiveDemo_Histogram;

public class DualMemoryInterface : VerilogAutomation
{
    protected override Dependencies Dependencies => Dependencies.None;

	protected override void ApplyAutomation()
	{
		string name = Items[0, "Name"] | Item.Required; 
		int addressWidth = Items[1, "AddressWidth"] | Item.Required; 
		int dataWidth = Items[2, "DataWidth"] | Item.Required; 

		CodeAfterAutomation += @$"
reg {name}WriteA;
reg [{addressWidth - 1}:0] {name}AddressA;
reg [{dataWidth - 1}:0] {name}DataInA;
reg {name}WriteB;
reg [{addressWidth - 1}:0] {name}AddressB;
reg [{dataWidth - 1}:0] {name}DataInB;

wire [{dataWidth - 1}:0] {name}DataOutA;
wire [{dataWidth - 1}:0] {name}DataOutB;";

		if(Items.Contains("PreviousAddressA"))
			CodeAfterAutomation += $@"
//[Previous]
reg [{addressWidth - 1}:0] p1_{name}AddressA;";

		if(Items.Contains("PreviousAddressB"))
			CodeAfterAutomation += $@"
//[Previous]
reg [{addressWidth - 1}:0] p1_{name}AddressB;";

	}
}