/*
Copyright (c) 2022, Icii Technologies LLC
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree. 
*/

namespace LiveDemo_Histogram;

public class ResetTime : VerilogAutomation
{
    protected override Dependencies Dependencies => Dependencies.None;

	protected override void ApplyAutomation()
	{
		string time = Items[0, "Time"] | Item.Required;

		IVariable variable = NextVariable();
		variable.AdditionalProperties["ResetTime"] = time;
	}
}