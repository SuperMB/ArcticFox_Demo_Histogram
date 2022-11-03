/*
Copyright (c) 2022, Icii Technologies LLC
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree. 
*/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LiveDemo_Histogram;

public class StateMachine : VerilogAutomation
{
    protected override Dependencies Dependencies => Module.PrimaryClockSet;

	protected override void ApplyAutomation()
	{
		if(Items.Count < 2)
		{
			Error("StateMachine requires at least 2 items, the state signal name in position 0 and 1 or more states that follow");
		}

		string stateSignal = Items[0] | Item.Required;
		List<string> states = new List<string>();
		Dictionary<string, List<string>> conditions =new Dictionary<string, List<string>>();
		Dictionary<string, List<string>> resultingStates =new Dictionary<string, List<string>>();
		
		bool inCondition = true;
		for(int i = 1; i < Items.Count; i++)
		{
			if(Items[i] == ":") { }
			else if(Items[i] == "=>")
				inCondition = false;
			else if(Items[i] == ",") 
			{
				inCondition = true;
				conditions[states.Last()].Add("");
				resultingStates[states.Last()].Add("");
			}
			else if(i < Items.Count - 1 && Items[i+1] == ":")
			{
				inCondition = true;
				states.Add(Items[i]);
				conditions[states.Last()] = new List<string>{ "" };
				resultingStates[states.Last()] = new List<string>{ "" };
			}
			else if(inCondition)
			{
				conditions[states.Last()][conditions[states.Last()].Count - 1] += Items[i] + " "; 
			}
			else
			{
				resultingStates[states.Last()][resultingStates[states.Last()].Count - 1] += Items[i] + " "; 
			}
		}
		
		CodeAfterAutomation += @$"
reg [{Math.Ceiling(Math.Log2(states.Count)) - 1}:0] {stateSignal};";
		for(int i = 0; i < states.Count; i++)
			CodeAfterAutomation += @$"
parameter {states[i]} = {i};";

		
		CodeAfterAutomation += @$"
		
always@(posedge {Module.PrimaryClock}) begin
	{IfReset($"{stateSignal} <= {states[0]};")} {(ResetInUse ? "begin" : "")}"; 
	

		CodeAfterAutomation += @$" 
		case({stateSignal})";
	
		for(int i = 0; i < states.Count; i++)	
		{
			CodeAfterAutomation += @$"
			{states[i]} : begin
				{(0, conditions[states[i]].Count - 1).ForIfElseStack(
					j => conditions[states[i]][j].Trim(),
					j => $"{stateSignal} <= {resultingStates[states[i]][j].Trim()};",
					$"{stateSignal} <= {states[i]};",
					"\t\t\t\t"
				)}
			end";
		} 

		CodeAfterAutomation += @$"
			default : {stateSignal} <= {stateSignal};
		endcase";

		CodeAfterAutomation += @$"
	{(ResetInUse ? "end" : "")}
end";
	}
}