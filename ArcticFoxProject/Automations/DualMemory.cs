/*
Copyright (c) 2022, Icii Technologies LLC
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree. 
*/

namespace LiveDemo_Histogram;

public class DualMemory : VerilogAutomation
{
    protected override Dependencies Dependencies => Module.PrimaryClockSet;

	protected override void ApplyAutomation()
	{
		string name = Items[0, "Name"] | Item.Required; 
		int addressWidth = Items[1, "AddressWidth"] | Item.Required; 
		int dataWidth = Items[2, "DataWidth"] | Item.Required; 

        string additionalItems = "";

		if(Items.Contains("PreviousAddressA"))
            additionalItems += "-previousAddressA ";

		if(Items.Contains("PreviousAddressB"))
            additionalItems += "-previousAddressB ";

        additionalItems = additionalItems.Trim();


		CodeAfterAutomation += @$"
//[DualMemoryInterface -name {name} -addressWidth {addressWidth} -dataWidth {dataWidth} {additionalItems}]

BRAM_{Math.Pow(2,addressWidth)}_{dataWidth} memory{name} (
    .clka({Module.PrimaryClock}),
    .addra({name}AddressA),
    .dina({name}DataInA),
    .wea({name}WriteA),
    .clkb({Module.PrimaryClock}),
    .addrb({name}AddressB),
    .dinb({name}DataInB),
    .web({name}WriteB),

    .douta({name}DataOutA),
    .doutb({name}DataOutB)
);";
	}
}