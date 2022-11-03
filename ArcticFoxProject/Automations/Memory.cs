/*
Copyright (c) 2022, Icii Technologies LLC
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree. 
*/

namespace LiveDemo_Histogram;

public class Memory : VerilogAutomation
{
    protected override Dependencies Dependencies => Module.PrimaryClockSet;

	protected override void ApplyAutomation()
	{
		string name = Items[0] | Item.Required;

		CodeAfterAutomation += @$"
//[MemoryInterface gamma]

BRAM_16K_8BitWidth memory{name} (
    .clk({Module.PrimaryClock}),
    .address({name}Address),
    .dataIn({name}DataIn),
    .write({name}Write),

    .dataOut({name}DataOut),
);
		";
	}
}