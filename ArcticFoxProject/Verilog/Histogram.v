/*
Copyright (c) 2022, Icii Technologies LLC
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree. 
*/

//[$SpectrumWidth 7]
module Histogram(
    //[Clock 100 MHz]
    input  clk,

    //[Reset]
    //[InitialValue 1]
    //[ResetTime 1000]
    input  reset,

    input store,
    input startReadout,
    input [spectrumWidth - 1:0] inputValue,
    output histogramValueReady,
    output [spectrumWidth - 1:0] histogramValue
);


//[Set $SpectrumWidth]
parameter spectrumWidth = 7;
parameter spectrumMax = 1 << spectrumWidth - 1;

//[RisingEdge]
wire risingStore;
//[Previous]
reg p1_risingStore;
//[Previous]
reg p2_risingStore;

//[RisingEdge]
wire risingStartReadout;

assign histogramValueReady = memoryState == readingOut;
assign histogramValue = histogramValueReady ? histogramDataOutA : 0;

//[StateMachine memoryState
// storing : risingStartReadout => readingOut
// readingOut : histogramAddressA == spectrumMax => storing]

//[$Memory histogram]
//[DualMemory -name $Memory -dataWidth 16 -addressWidth 7 -previousAddressA]

//[MemoryBlock $Memory A]
always@(posedge clk) begin
    if(reset)
        //[> 0]
    else begin
        case(memoryState)
            storing : begin
                if(risingStartReadout)
                    //[> 0]
                else if(risingStore)
                    //[> inputValue]
                else
                    //[> 0]
            end
            readingOut : begin
                //[> histogramAddressA + 1]
            end
        endcase
    end
end

//[MemoryBlock $Memory B]
always@(posedge clk) begin
    if(reset)
        //[> 0]
    else begin
        case(memoryState)
            storing : begin
                if(p2_risingStore)
                    //[> p1_histogramAddressA => histogramDataOutA + 1]
                else
                    //[> 0]
            end
            readingOut : begin
                //[> 0]
            end
        endcase
    end
end


endmodule
